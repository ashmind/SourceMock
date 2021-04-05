using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal class MockClassGenerator {
        private static readonly SymbolDisplayFormat TargetTypeNamespaceDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
            SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining
        );

        private static class Indents {
            public const string TopLevelType = "    ";
            public const string TopLevelTypeMember = TopLevelType + "    ";
            public const string TopLevelMemberBody = TopLevelTypeMember + "    ";
            public const string Type = "        ";
        }        

        public string Generate(in MockTarget target) {
            var targetTypeNamespace = target.Type.ContainingNamespace.ToDisplayString(TargetTypeNamespaceDisplayFormat);
            var mockClassName = "Mock" + target.Type.Name;
            var setupInterfaceName = "ISetup" + target.Type.Name;
            var callsInterfaceName = "ICalls" + target.Type.Name;

            var mainWriter = new CodeWriter()
                .WriteLine("#nullable enable")
                .WriteLine("namespace ", targetTypeNamespace, ".Mocks {")
                .WriteLine(Indents.TopLevelType, "[", KnownTypes.GeneratedMockAttribute.FullNameWithoutAttribute, "]")
                .Write(Indents.TopLevelType, "public class ", mockClassName, " : ")
                    .WriteLine(target.FullTypeName, ", ", setupInterfaceName, ", ", callsInterfaceName, " {")
                .WriteLine(Indents.TopLevelTypeMember, "public ", setupInterfaceName, " Setup => this;")
                .WriteLine(Indents.TopLevelTypeMember, "public ", callsInterfaceName, " Calls => this;");

            var setupInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.TopLevelType, "[", KnownTypes.GeneratedMockAttribute.FullNameWithoutAttribute, "]")
                .WriteLine(Indents.TopLevelType, "public interface ", setupInterfaceName, " {");

            var callsInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.TopLevelType, "[", KnownTypes.GeneratedMockAttribute.FullNameWithoutAttribute, "]")
                .WriteLine(Indents.TopLevelType, "public interface ", callsInterfaceName, " {");

            var memberId = 1;
            foreach (var memberSymbol in target.Type.GetMembers()) {
                if (GetMockedMember(memberSymbol, memberId) is not {} member)
                    continue;
                mainWriter.WriteLine();
                WriteMemberMocks(
                    mainWriter,
                    setupInterfaceWriter,
                    setupInterfaceName,
                    callsInterfaceWriter,
                    callsInterfaceName,
                    member
                );
                memberId += 1;
            }

            mainWriter.WriteLine(Indents.TopLevelType, "}");

            setupInterfaceWriter.Write(Indents.TopLevelType, "}");
            mainWriter
                .WriteLine()
                .Append(setupInterfaceWriter)
                .WriteLine();

            callsInterfaceWriter.Write(Indents.TopLevelType, "}");
            mainWriter
                .WriteLine()
                .Append(callsInterfaceWriter)
                .WriteLine();

            mainWriter.Write("}");
            return mainWriter.ToString();
        }

        private MockedMember? GetMockedMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol { MethodKind: MethodKind.Ordinary } method => new(
                method, method.Name, method.ReturnType,
                GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation),
                ConvertParametersFromSymbols(method.Parameters),
                GetHandlerFieldName(method.Name, uniqueMemberId)
            ),

            IPropertySymbol property => new(
                property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                property.SetMethod == null
                    ? ImmutableArray<Parameter>.Empty
                    : ConvertParametersFromSymbols(property.SetMethod.Parameters),
                GetHandlerFieldName(property.Name, uniqueMemberId)
            ),

            _ => null
        };

        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
        }

        private ImmutableArray<Parameter> ConvertParametersFromSymbols(ImmutableArray<IParameterSymbol> parameters) {
            return ImmutableArray.CreateRange(
                parameters.Select((p, index) => new Parameter(p.Name, GetFullTypeName(p.Type, p.NullableAnnotation), index))
            );
        }

        private string GetFullTypeName(ITypeSymbol type, NullableAnnotation nullableAnnotation) {
            var name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (nullableAnnotation == NullableAnnotation.Annotated && !type.IsValueType)
                name += "?";
            return name;
        }

        private void WriteMemberMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            string setupInterfaceName,
            CodeWriter callsInterfaceWriter,
            string callsInterfaceName,
            in MockedMember member
        ) {
            WriteHandlerField(mockWriter, member);
            WriteSetupInterfaceMember(setupInterfaceWriter, member);
            WriteSetupMemberImplementation(mockWriter, setupInterfaceName, member);
            WriteMemberImplementation(mockWriter, member);
            WriteCallsInterfaceMember(callsInterfaceWriter, member);
            WriteCallsMemberImplementation(mockWriter, callsInterfaceName, member);
        }

        private void WriteHandlerField(CodeWriter writer, in MockedMember member)
        {
            var (handlerTypeFullName, handlerValue) = member.Symbol switch {
                IMethodSymbol => (KnownTypes.MockMethodHandler.FullName, "new()"),
                IPropertySymbol property => (KnownTypes.MockPropertyHandler.FullName, property.SetMethod != null ? "new(true)" : "new(false)"),
                _ => throw MemberNotSupported(member)
            };
            var handlerGenericParameterFullName = !member.IsVoidMethod ? member.TypeFullName : KnownTypes.VoidReturn.FullName;
            writer
                .Write(Indents.TopLevelTypeMember, "private readonly ")
                .WriteGeneric(handlerTypeFullName, handlerGenericParameterFullName)
                .WriteLine(" ", member.HandlerFieldName, " = ", handlerValue, ";");
        }

        private void WriteSetupInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.TopLevelTypeMember);
            WriteSetupMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            writer.WriteLine();
        }

        private void WriteSetupMemberImplementation(CodeWriter writer, string setupInterfaceName, in MockedMember member) {
            writer.Write(Indents.TopLevelTypeMember);
            WriteSetupMemberType(writer, member);
            writer.Write(" ", setupInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }
            writer.Write(" => ");

            writer.Write(member.HandlerFieldName, ".Setup(");
            if (member.Symbol is IMethodSymbol) {
                foreach (var parameter in member.Parameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.Name);
                }
            }
            writer.WriteLine(");");
        }

        private void WriteSetupMemberType(CodeWriter writer, in MockedMember member) {
            if (member.IsVoidMethod) {
                writer.Write(KnownTypes.IMockMethodSetup.FullName);
                return;
            }

            var setupTypeFullName = member.Symbol switch {
                IMethodSymbol => KnownTypes.IMockMethodSetup.FullName,
                IPropertySymbol property => property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertySetup.FullName
                    : KnownTypes.IMockPropertySetup.FullName,
                _ => throw MemberNotSupported(member)
            };
            writer.WriteGeneric(setupTypeFullName, member.TypeFullName);
        }

        private void WriteMemberImplementation(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.TopLevelTypeMember, "public ", member.TypeFullName, " ", member.Name);

            switch (member.Symbol) {
                case IMethodSymbol:
                    writer.Write("(");
                    foreach (var parameter in member.Parameters) {
                        if (parameter.Index > 0)
                            writer.Write(", ");
                        writer.Write(parameter.TypeFullName, " ", parameter.Name);
                    }
                    writer.Write(") => ", member.HandlerFieldName);
                    WriteMemberImplementationHandlerCall(writer, member.Parameters);
                    break;

                case IPropertySymbol property:
                    if (property.SetMethod != null) {
                        writer.WriteLine(" {");
                        writer.Write(Indents.TopLevelMemberBody, "get => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, ImmutableArray<Parameter>.Empty);
                        writer.Write(Indents.TopLevelMemberBody, "set => ", member.HandlerFieldName, ".SetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member.Parameters);
                        writer.WriteLine(Indents.TopLevelTypeMember, "}");
                    }
                    else {
                        writer.Write(" => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member.Parameters);
                    }
                    break;
            }
        }

        private void WriteMemberImplementationHandlerCall(CodeWriter writer, ImmutableArray<Parameter> parameters) {
            writer.Write(".Call(");
            foreach (var parameter in parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer.Write(parameter.Name);
            }
            writer.WriteLine(");");
        }

        private void WriteCallsInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.TopLevelTypeMember);            
            WriteCallsMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            writer.WriteLine();
        }

        private void WriteCallsMemberImplementation(CodeWriter writer, string callsInterfaceName, in MockedMember member) {
            writer.Write(Indents.TopLevelTypeMember);
            WriteCallsMemberType(writer, member);
            writer.Write(" ", callsInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }
            writer.Write(" => ", member.HandlerFieldName, ".Calls(");
            if (member.Symbol is IMethodSymbol) {
                var parameters = member.Parameters;
                if (parameters.Length > 0) {
                    writer.Write("args => (");
                    foreach (var parameter in parameters) {
                        if (parameter.Index > 0)
                            writer.Write(", ");
                        writer.Write("(", parameter.TypeFullName, ")args[", parameter.Index.ToString(), "]");
                        if (!parameter.TypeFullName.EndsWith("?"))
                            writer.Write("!");
                    }
                    writer.Write(")");
                }
                else {
                    writer.Write("_ => ", KnownTypes.NoArguments.FullName, ".Value");
                }
                foreach (var parameter in parameters) {
                    writer.Write(", ", parameter.Name);
                }
            }
            writer.WriteLine(");");
        }

        private void WriteSetupOrCallsInterfaceMemberNameAndParameters(CodeWriter writer, MockedMember member) {
            writer.Write(member.Name);
            switch (member.Symbol) {
                case IMethodSymbol:
                    writer.Write("(");
                    WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: true);
                    writer.Write(");");
                    break;
                case IPropertySymbol:
                    writer.Write(" { get; }");
                    break;
                default:
                    throw MemberNotSupported(member);
            }
        }

        private void WriteSetupOrCallsMemberParameters(CodeWriter writer, in MockedMember member, bool appendDefaultValue) {
            foreach (var parameter in member.Parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgumentMatcher.FullName, parameter.TypeFullName)
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
            }
        }

        private void WriteCallsMemberType(CodeWriter writer, in MockedMember member) {
            if (member.Symbol is IPropertySymbol property) {
                var callsTypeFullName = property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertyCalls.FullName
                    : KnownTypes.IMockPropertyCalls.FullName;
                writer.WriteGeneric(callsTypeFullName, member.TypeFullName);
                return;
            }

            writer.Write(KnownTypes.IReadOnlyList.FullName, "<");

            var parameters = member.Parameters;
            if (parameters.Length > 1) {
                writer.Write("(");
                foreach (var parameter in parameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.TypeFullName, " ", parameter.Name);
                }
                writer.Write(")");
            }
            else if (parameters.Length == 1) {
                writer.Write(parameters[0].TypeFullName);
            }
            else {
                writer.Write(KnownTypes.NoArguments.FullName);
            }

            writer.Write(">");
        }

        private NotSupportedException MemberNotSupported(in MockedMember member) => new NotSupportedException($"Unknown member symbol type: {member.Symbol.GetType()}");

        private readonly struct MockedMember
        {
            public MockedMember(
                ISymbol symbol,
                string name,
                ITypeSymbol type,
                string typeFullName,
                ImmutableArray<Parameter> parameters,
                string handlerFieldName
            ) {
                Symbol = symbol;
                Name = name;
                Type = type;
                TypeFullName = typeFullName;
                Parameters = parameters;
                HandlerFieldName = handlerFieldName;
            }

            public ISymbol Symbol { get; }
            public string Name { get; }
            public ITypeSymbol Type { get; }
            public string TypeFullName { get; }
            public ImmutableArray<Parameter> Parameters { get; }
            public string HandlerFieldName { get; }
            public bool IsVoidMethod => Symbol is IMethodSymbol && Type.SpecialType == SpecialType.System_Void;
        }

        private readonly struct Parameter {
            public Parameter(string name, string typeFullName, int index) {
                Name = name;
                TypeFullName = typeFullName;
                Index = index;
            }

            public string Name { get; }
            public string TypeFullName { get; }
            public int Index { get; }
        }
    }
}
