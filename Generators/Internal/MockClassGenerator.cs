using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal partial class MockClassGenerator {
        private static readonly SymbolDisplayFormat TargetTypeNamespaceDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
            SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining
        );

        public string Generate(MockTarget target) {
            var targetTypeNamespace = target.Type.ContainingNamespace.ToDisplayString(TargetTypeNamespaceDisplayFormat);
            var mockBaseName = GenerateMockBaseName(target.Type.Name);
            var mockClassName = mockBaseName + "Mock";
            var setupInterfaceName = "I" + mockBaseName + "Setup";
            var callsInterfaceName = "I" + mockBaseName + "Calls";

            var mainWriter = new CodeWriter()
                .WriteLine("#nullable enable")
                .WriteLine("namespace ", targetTypeNamespace, ".Mocks {");

            mainWriter.Write(Indents.Type, "public class ", mockClassName);
            WriteMockTypeParametersIfAny(target, mainWriter);
            mainWriter
                .Write(" : ")
                    .Write(target.FullTypeName, ", ", setupInterfaceName, ", ", callsInterfaceName, ", ")
                    .WriteGeneric(KnownTypes.IMock.FullName, target.FullTypeName)
                    .WriteLine(" {")
                .WriteLine(Indents.Member, "public ", setupInterfaceName, " Setup => this;")
                .WriteLine(Indents.Member, "public ", callsInterfaceName, " Calls => this;");

            var setupInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.Type, "public interface ", setupInterfaceName, " {");

            var callsInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.Type, "public interface ", callsInterfaceName, " {");

            var memberId = 1;
            foreach (var memberSymbol in target.Type.GetMembers()) {
                if (GetMockedMember(memberSymbol, memberId) is not { } member)
                    continue;

                mainWriter.WriteLine();
                WriteMemberMocks(
                    mainWriter,
                    setupInterfaceWriter,
                    setupInterfaceName,
                    callsInterfaceWriter,
                    callsInterfaceName,
                    member
                ).WriteLine();
                memberId += 1;
            }

            mainWriter.WriteLine(Indents.Type, "}");

            setupInterfaceWriter.Write(Indents.Type, "}");
            mainWriter
                .WriteLine()
                .Append(setupInterfaceWriter)
                .WriteLine();

            callsInterfaceWriter.Write(Indents.Type, "}");
            mainWriter
                .WriteLine()
                .Append(callsInterfaceWriter)
                .WriteLine();

            mainWriter.Write("}");
            return mainWriter.ToString();
        }

        private string GenerateMockBaseName(string targetName) {
            if (targetName.Length < 3)
                return targetName;

            var canRemoveI = targetName[0] == 'I'
                          && char.IsUpper(targetName[1])
                          && char.IsLower(targetName[2]);

            return canRemoveI ? targetName.Substring(1) : targetName;
        }

        private CodeWriter WriteMockTypeParametersIfAny(MockTarget target, CodeWriter mainWriter) {
            if (target.Type.TypeParameters is not { Length: > 0 } parameters)
                return mainWriter;

            mainWriter.Write("<");
            var index = 0;
            foreach (var parameter in parameters) {
                EnsureNoUnsupportedConstraints(parameter);
                if (index > 0)
                    mainWriter.Write(", ");
                mainWriter.Write(parameter.Name);
                index += 1;
            }
            return mainWriter.Write(">");
        }

        private MockedMember? GetMockedMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol { MethodKind: MethodKind.Ordinary } method => new(
                method, method.Name, method.ReturnType,
                GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation),
                ConvertGenericParametersFromSymbols(method.TypeParameters),
                ConvertParametersFromSymbols(method.Parameters),
                GetHandlerFieldName(method.Name, uniqueMemberId)
            ),

            IPropertySymbol property => new(
                property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                ImmutableArray<GenericParameter>.Empty,
                property.SetMethod == null
                    ? ImmutableArray<Parameter>.Empty
                    : ConvertParametersFromSymbols(property.SetMethod.Parameters),
                GetHandlerFieldName(property.Name, uniqueMemberId)
            ),

            IMethodSymbol { MethodKind: not MethodKind.Ordinary } => null,

            _ => throw MemberNotSupported(member)
        };

        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
        }

        private ImmutableArray<Parameter> ConvertParametersFromSymbols(ImmutableArray<IParameterSymbol> parameters) {
            if (parameters.Length == 0)
                return ImmutableArray<Parameter>.Empty;
            return ImmutableArray.CreateRange(parameters.Select((p, index) => new Parameter(
                p.Name, GetFullTypeName(p.Type, p.NullableAnnotation), p.RefKind, index
            )));
        }

        private ImmutableArray<GenericParameter> ConvertGenericParametersFromSymbols(ImmutableArray<ITypeParameterSymbol> typeParameters) {
            if (typeParameters.Length == 0)
                return ImmutableArray<GenericParameter>.Empty;
            return ImmutableArray.CreateRange(typeParameters.Select((p, index) => {
                EnsureNoUnsupportedConstraints(p);
                return new GenericParameter(p.Name, index);
            }));
        }

        private void EnsureNoUnsupportedConstraints(ITypeParameterSymbol parameter) {
            var hasConstraints = parameter.HasConstructorConstraint
                              || parameter.HasReferenceTypeConstraint
                              || parameter.HasValueTypeConstraint
                              || parameter.HasConstructorConstraint
                              || parameter.HasNotNullConstraint
                              || parameter.HasUnmanagedTypeConstraint
                              || parameter.ConstraintTypes.Length > 0;

            if (hasConstraints)
                throw new NotSupportedException("Generic constraints are not yet supported.");
        }

        private string GetFullTypeName(ITypeSymbol type, NullableAnnotation nullableAnnotation) {
            var name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (nullableAnnotation == NullableAnnotation.Annotated && !type.IsValueType)
                name += "?";
            return name;
        }

        private CodeWriter WriteMemberMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            string setupInterfaceName,
            CodeWriter callsInterfaceWriter,
            string callsInterfaceName,
            in MockedMember member
        ) {
            WriteHandlerField(mockWriter, member).WriteLine();
            WriteSetupInterfaceMember(setupInterfaceWriter, member).WriteLine();
            WriteSetupMemberImplementation(mockWriter, setupInterfaceName, member).WriteLine();
            WriteMemberImplementation(mockWriter, member).WriteLine();
            WriteCallsInterfaceMember(callsInterfaceWriter, member).WriteLine();
            WriteCallsMemberImplementation(mockWriter, callsInterfaceName, member);
            return mockWriter;
        }

        private CodeWriter WriteHandlerField(CodeWriter writer, in MockedMember member)
        {
            writer.Write(Indents.Member, "private readonly ");
            switch (member.Symbol) {
                case IMethodSymbol:
                    writer.Write(KnownTypes.MockMethodHandler.FullName, " ", member.HandlerFieldName, " = new()");
                    break;

                case IPropertySymbol property:
                    writer
                        .WriteGeneric(KnownTypes.MockPropertyHandler.FullName, member.HandlerGenericParameterFullName)
                        .Write(" ", member.HandlerFieldName)
                        .Write(" = new(", property.SetMethod != null ? "true" : "false", ")");
                    break;

                default:
                    throw MemberNotSupported(member.Symbol);
            }
            return writer.Write(";");
        }

        private CodeWriter WriteSetupInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteSetupMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            return writer;
        }

        private CodeWriter WriteSetupMemberImplementation(CodeWriter writer, string setupInterfaceName, in MockedMember member) {
            writer.Write(Indents.Member);

            WriteSetupMemberType(writer, member);
            writer.Write(" ", setupInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMemberGenericParametersIfAny(writer, member);
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }

            writer.Write(" => ");

            writer.Write(member.HandlerFieldName, ".Setup");
            if (member.Symbol is IMethodSymbol) {
                writer.Write("<", member.HandlerGenericParameterFullName, ">(");
                WriteCommonMethodHandlerArguments(writer, member, KnownTypes.IMockArgumentMatcher.FullName);
                writer.Write(")");
            }
            else {
                writer.Write("()");
            }

            return writer.Write(";");
        }

        private CodeWriter WriteSetupMemberType(CodeWriter writer, in MockedMember member) {
            if (member.IsVoidMethod)
                return writer.Write(KnownTypes.IMockMethodSetup.FullName);

            var setupTypeFullName = member.Symbol switch {
                IMethodSymbol => KnownTypes.IMockMethodSetup.FullName,
                IPropertySymbol property => property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertySetup.FullName
                    : KnownTypes.IMockPropertySetup.FullName,
                var s => throw MemberNotSupported(s)
            };
            return writer.WriteGeneric(setupTypeFullName, member.TypeFullName);
        }

        private CodeWriter WriteMemberImplementation(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member, "public ", member.TypeFullName, " ", member.Name);

            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMemberGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    var hasOutParameters = false;
                    foreach (var parameter in member.Parameters) {
                        if (parameter.Index > 0)
                            writer.Write(", ");
                        if (GetRefModifier(parameter.RefKind) is {} modifier)
                            writer.Write(modifier, " ");
                        writer.Write(parameter.TypeFullName, " ", parameter.Name);
                        hasOutParameters = hasOutParameters || (parameter.RefKind == RefKind.Out);
                    }
                    writer.Write(") ");
                    WriteMethodImplementationBody(writer, hasOutParameters, member);
                    break;

                case IPropertySymbol property:
                    if (property.SetMethod != null) {
                        writer.WriteLine(" {");
                        writer.Write(Indents.MemberBody, "get => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member, ImmutableArray<Parameter>.Empty);
                        writer.WriteLine(";");
                        writer.Write(Indents.MemberBody, "set => ", member.HandlerFieldName, ".SetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member);
                        writer.WriteLine(";");
                        writer.Write(Indents.Member, "}");
                    }
                    else {
                        writer.Write(" => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member);
                        writer.Write(";");
                    }
                    break;

                default:
                    throw MemberNotSupported(member.Symbol);
            }

            return writer;
        }

        private string? GetRefModifier(RefKind refKind) => refKind switch {
            RefKind.None => null,
            RefKind.Ref => "ref",
            RefKind.In => "in",
            RefKind.Out => "out",
            _ => throw new NotSupportedException($"Unsupported parameter ref kind: {refKind}")
        };

        private CodeWriter WriteMethodImplementationBody(CodeWriter writer, bool hasOutParameters, in MockedMember member) {
            if (hasOutParameters) {
                writer.WriteLine("{");
                foreach (var parameter in member.Parameters) {
                    if (parameter.RefKind != RefKind.Out)
                        continue;
                    writer.WriteLine(Indents.MemberBody, parameter.Name, " = default;");
                }
                writer.Write(Indents.MemberBody, "return ", member.HandlerFieldName);
                WriteMemberImplementationHandlerCall(writer, member, member.Parameters);
                writer.WriteLine(";");
                return writer.Write(Indents.Member, "}");
            }

            writer.Write("=> ", member.HandlerFieldName);
            WriteMemberImplementationHandlerCall(writer, member, member.Parameters);
            return writer.Write(";");
        }

        private CodeWriter WriteMemberImplementationHandlerCall(CodeWriter writer, in MockedMember member, ImmutableArray<Parameter>? parametersOverride = null) {
            writer.Write(".Call<", member.HandlerGenericParameterFullName, ">(");
            WriteCommonMethodHandlerArguments(writer, member, "object?", parametersOverride);
            return writer.Write(")");
        }

        private CodeWriter WriteCallsInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);            
            WriteCallsMemberType(writer, member);
            writer.Write(" ");
            return WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);            
        }

        private CodeWriter WriteCallsMemberImplementation(CodeWriter writer, string callsInterfaceName, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ", callsInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMemberGenericParametersIfAny(writer, member);
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }
            writer.Write(" => ", member.HandlerFieldName, ".Calls(");
            if (member.Symbol is IMethodSymbol) {
                WriteCommonMethodHandlerArguments(writer, member, KnownTypes.IMockArgumentMatcher.FullName).Write(", ");
                var parameters = member.Parameters;
                if (!parameters.IsEmpty) {
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
            }
            return writer.Write(");");
        }

        private CodeWriter WriteCallsMemberType(CodeWriter writer, in MockedMember member) {
            if (member.Symbol is IPropertySymbol property) {
                var callsTypeFullName = property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertyCalls.FullName
                    : KnownTypes.IMockPropertyCalls.FullName;
                return writer.WriteGeneric(callsTypeFullName, member.TypeFullName);
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

            return writer.Write(">");
        }

        private CodeWriter WriteSetupOrCallsInterfaceMemberNameAndParameters(CodeWriter writer, in MockedMember member) {
            writer.Write(member.Name);
            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMemberGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: true);
                    writer.Write(");");
                    break;
                case IPropertySymbol:
                    writer.Write(" { get; }");
                    break;
                default:
                    throw MemberNotSupported(member.Symbol);
            }
            return writer;
        }

        private CodeWriter WriteSetupOrCallsMemberParameters(CodeWriter writer, in MockedMember member, bool appendDefaultValue) {
            foreach (var parameter in member.Parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgumentMatcher.FullName, parameter.TypeFullName)
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
            }
            return writer;
        }

        private CodeWriter WriteCommonMethodHandlerArguments(
            CodeWriter writer,
            in MockedMember member,
            string argumentTypeFullName,
            ImmutableArray<Parameter>? parametersOverride = null
        ) {
            var parameters = parametersOverride ?? member.Parameters;

            if (!member.GenericParameters.IsEmpty) {
                writer.Write("new[] { ");
                foreach (var parameter in member.GenericParameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write("typeof(", parameter.Name, ")");
                }
                writer.Write("}");
            }
            else {
                writer.Write("null");
            }
            writer.Write(", ");

            if (!parameters.IsEmpty) {
                writer.Write("new ", argumentTypeFullName, "[] { ");
                foreach (var parameter in parameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.Name);
                }
                writer.Write("}");
            }
            else {
                writer.Write("null");
            }
            return writer;
        }

        private CodeWriter WriteMemberGenericParametersIfAny(CodeWriter writer, in MockedMember member) {
            if (member.GenericParameters.Length == 0)
                return writer;
            writer.Write("<");
            foreach (var parameter in member.GenericParameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer.Write(parameter.Name);
            }
            return writer.Write(">");
        }


        private NotSupportedException MemberNotSupported(ISymbol symbol) => new NotSupportedException(
            $"{symbol.Name} has an unsupported member symbol type ({symbol.GetType()})"
        );

        private readonly struct MockedMember
        {
            public MockedMember(
                ISymbol symbol,
                string name,
                ITypeSymbol type,
                string typeFullName,
                ImmutableArray<GenericParameter> genericParameters,
                ImmutableArray<Parameter> parameters,
                string handlerFieldName
            ) {
                Symbol = symbol;
                Name = name;
                Type = type;
                TypeFullName = typeFullName;
                GenericParameters = genericParameters;
                Parameters = parameters;
                HandlerFieldName = handlerFieldName;
            }

            public ISymbol Symbol { get; }
            public string Name { get; }
            public ITypeSymbol Type { get; }
            public string TypeFullName { get; }
            public ImmutableArray<GenericParameter> GenericParameters { get; }
            public ImmutableArray<Parameter> Parameters { get; }
            public string HandlerFieldName { get; }
            public bool IsVoidMethod => Symbol is IMethodSymbol && Type.SpecialType == SpecialType.System_Void;
            public string HandlerGenericParameterFullName => !IsVoidMethod ? TypeFullName : KnownTypes.VoidReturn.FullName;
        }

        private readonly struct GenericParameter {
            public GenericParameter(string name, int index) {
                Name = name;
                Index = index;
            }

            public string Name { get; }
            public int Index { get; }
        }

        private readonly struct Parameter {
            public Parameter(string name, string typeFullName, RefKind refKind, int index) {
                Name = name;
                TypeFullName = typeFullName;
                RefKind = refKind;
                Index = index;
            }

            public string Name { get; }
            public string TypeFullName { get; }
            public RefKind RefKind { get; }
            public int Index { get; }
        }
    }
}
