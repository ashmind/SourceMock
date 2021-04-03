using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal class MockClassGenerator {
        private static class Indents {
            public const string Type = "    ";
            public const string Member = "        ";
            public const string MemberBody = "            ";
        }        

        public string Generate(in MockInfo mock, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            var mainWriter = new CodeWriter()
                .WriteLine("#nullable enable")
                .WriteLine("public static class ", mock.MockTypeName, " {")
                .WriteLine(Indents.Type, "public class Instance : ", mock.TargetTypeQualifiedName, ", ISetup, ICalls {")
                .WriteLine(Indents.Member, "public ISetup Setup => this;")
                .WriteLine(Indents.Member, "public ICalls Calls => this;")
                .WriteLine();

            var setupInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.Type, "public interface ISetup {");

            var callsInterfaceWriter = new CodeWriter()
                .WriteLine(Indents.Type, "public interface ICalls {");

            var memberId = 1;
            foreach (var member in mock.TargetType.GetMembers()) {
                var context = GetMockedMember(member, memberId);
                if (context == null)
                    continue;
                WriteMemberMocks(mainWriter, setupInterfaceWriter, callsInterfaceWriter, context!.Value, requestOtherMock);
                memberId += 1;
                mainWriter.WriteLine();
            }

            setupInterfaceWriter.Write(Indents.Type, "}");
            callsInterfaceWriter.Write(Indents.Type, "}");

            mainWriter
                .WriteLine("    }")
                .WriteLine();

            WriteReturnedInstanceType(mainWriter, mock);

            mainWriter
                .WriteLine()
                .WriteLine()
                .Append(setupInterfaceWriter)
                .WriteLine()
                .WriteLine()
                .Append(callsInterfaceWriter)
                .WriteLine()
                .WriteLine()
                .Write(Indents.Type, "public interface IReturnedSetup : ISetup, ")
                .WriteGeneric(KnownTypes.IMockMethodSetup.FullName, mock.TargetTypeQualifiedName)
                .WriteLine(" {}")
                .WriteLine()
                .Write(Indents.Type, "public static ", mock.MockTypeName, ".Instance Get(this ")
                .WriteGeneric(KnownTypes.Mock.FullName, mock.TargetTypeQualifiedName)
                .WriteLine(" _) => new();")
                .Write("}");

            return mainWriter.ToString();
        }

        private MockedMember? GetMockedMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol { MethodKind: MethodKind.Ordinary } method => new(
                MockedMemberKind.Method, method.Name, method.ReturnType,
                GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation),
                ImmutableArray.CreateRange(
                    method.Parameters.Select((p, index) => (p.Name, GetFullTypeName(p.Type, p.NullableAnnotation), index))
                ),
                GetHandlerFieldName(method.Name, uniqueMemberId)
            ),
            IPropertySymbol property => new(
                MockedMemberKind.Property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                ImmutableArray<(string, string, int)>.Empty,
                GetHandlerFieldName(property.Name, uniqueMemberId)
            ),
            _ => null
        };

        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
        }

        private void WriteMemberMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            CodeWriter callsInterfaceWriter,
            in MockedMember context,
            Func<ITypeSymbol, MockInfo> requestOtherMock
        ) {
            WriteHandlerField(mockWriter, context.HandlerFieldName);
            WriteSetupInterfaceMember(setupInterfaceWriter, context, requestOtherMock);
            WriteSetupMemberImplementation(mockWriter, context, requestOtherMock);
            WriteMemberImplementation(mockWriter, context);
            WriteCallsInterfaceMember(callsInterfaceWriter, context);
            WriteCallsMemberImplementation(mockWriter, context);
        }

        private void WriteHandlerField(CodeWriter writer, string handlerFieldName)
        {
            writer.WriteLine(
                Indents.Member, "private readonly ", KnownTypes.MockMemberHandler.FullName, " ", handlerFieldName, " = new();"
            );
        }

        private void WriteSetupInterfaceMember(CodeWriter writer, in MockedMember member, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            writer.Write(Indents.Member);
            WriteSetupMemberType(writer, member, requestOtherMock);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            writer.WriteLine();
        }

        private void WriteSetupMemberImplementation(CodeWriter writer, in MockedMember member, Func<ITypeSymbol, MockInfo> requestOtherMockName) {
            writer.Write(Indents.Member);
            var returnMock = WriteSetupMemberType(writer, member, requestOtherMockName);
            writer.Write(" ISetup.", member.Name);
            if (member.Kind == MockedMemberKind.Method) {
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }
            writer.Write(" => ");
            if (returnMock != null)
                writer.Write("new ", returnMock.Value.MockTypeName, ".ReturnedInstance(");

            writer.Write(member.HandlerFieldName, ".Setup");
            if (!member.IsVoidMethod)
                writer.Write("<", member.TypeFullName, ">");

            writer.Write("(");
            foreach (var parameter in member.Parameters) {
                if (parameter.index > 0)
                    writer.Write(", ");
                writer.Write(parameter.name);
            }
            writer.Write(")");

            if (returnMock != null)
                writer.Write(")");
            writer.WriteLine(";");
        }

        private MockInfo? WriteSetupMemberType(CodeWriter writer, in MockedMember member, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            if (member.Type is { TypeKind: TypeKind.Interface } interfaceType) {
                var other = requestOtherMock(interfaceType);
                writer.Write(other.MockTypeName, ".IReturnedSetup");
                return other;
            }
            else if (member.IsVoidMethod) {
                writer.Write(KnownTypes.IMockMethodSetup.FullName);
                return null;
            }

            writer.WriteGeneric(KnownTypes.IMockMethodSetup.FullName, member.TypeFullName);
            return null;
        }

        private void WriteMemberImplementation(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member, "public ", member.TypeFullName, " ", member.Name);

            if (member.Kind == MockedMemberKind.Method) {
                writer.Write("(");
                foreach (var parameter in member.Parameters) {
                    if (parameter.index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.typeFullName, " ", parameter.name);
                }
                writer.Write(")");
            }

            writer.Write(" => ", member.HandlerFieldName, ".Call");
            if (!member.IsVoidMethod)
                writer.Write("<", member.TypeFullName, ">");

            writer.Write("(");
            foreach (var parameter in member.Parameters) {
                if (parameter.index > 0)
                    writer.Write(", ");
                writer.Write(parameter.name);
            }
            writer.WriteLine(");");
        }

        private void WriteCallsInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            writer.WriteLine();
        }

        private void WriteCallsMemberImplementation(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ICalls.", member.Name);
            if (member.Kind == MockedMemberKind.Method) {
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
            }
            writer.Write(" => ", member.HandlerFieldName, ".Calls(");
            var parameters = member.Parameters;
            if (parameters.Length > 0) {
                writer.Write("args => (");
                foreach (var parameter in parameters) {
                    if (parameter.index > 0)
                        writer.Write(", ");
                    writer.Write("(", parameter.typeFullName, ")args[", parameter.index.ToString(), "]!");
                }
                writer.Write(")");
            }
            else {
                writer.Write("_ => ", KnownTypes.NoArguments.FullName, ".Value");
            }
            foreach (var parameter in parameters) {
                writer.Write(", ", parameter.name);
            }
            writer.WriteLine(");");
        }

        private void WriteSetupOrCallsInterfaceMemberNameAndParameters(CodeWriter writer, MockedMember member) {
            writer.Write(member.Name);
            switch (member.Kind) {
                case MockedMemberKind.Method:
                    writer.Write("(");
                    WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: true);
                    writer.Write(");");
                    break;
                case MockedMemberKind.Property:
                    writer.Write(" { get; }");
                    break;
                default:
                    throw new NotSupportedException($"Unknwon member kind: {member.Kind}");
            }
        }

        private void WriteSetupOrCallsMemberParameters(CodeWriter writer, in MockedMember member, bool appendDefaultValue) {
            foreach (var parameter in member.Parameters) {
                if (parameter.index > 0)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgumentMatcher.FullName, parameter.typeFullName)
                    .Write(" ", parameter.name);
                if (appendDefaultValue)
                    writer.Write(" = default");
            }
        }

        private void WriteCallsMemberType(CodeWriter writer, in MockedMember member) {
            writer.Write(KnownTypes.IReadOnlyList.FullName, "<");

            var parameters = member.Parameters;
            if (parameters.Length > 1) {
                writer.Write("(");
                foreach (var parameter in parameters) {
                    if (parameter.index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.typeFullName, " ", parameter.name);
                }
                writer.Write(")");
            }
            else if (parameters.Length == 1) {
                writer.Write(parameters[0].typeFullName);
            }
            else {
                writer.Write(KnownTypes.NoArguments.FullName);
            }

            writer.Write(">");
        }

        private void WriteReturnedInstanceType(CodeWriter writer, in MockInfo mock) {
            writer
                .WriteLine(Indents.Type, "public class ReturnedInstance : Instance, IReturnedSetup {")
                .WriteLine(Indents.Member, "private readonly ", KnownTypes.IMockMethodSetup.FullName, "<", mock.TargetTypeQualifiedName, "> _setup;")
                .WriteLine(Indents.Member, "public ReturnedInstance(", KnownTypes.IMockMethodSetup.FullName, "<", mock.TargetTypeQualifiedName, "> setup) {")
                .WriteLine(Indents.MemberBody, "_setup = setup;")
                .WriteLine(Indents.MemberBody, "_setup.Returns(this);")
                .WriteLine(Indents.Member, "}")
                .WriteLine()
                .Write(Indents.Member, "void ")
                .WriteGeneric(KnownTypes.IMockMethodSetup.FullName, mock.TargetTypeQualifiedName)
                .WriteLine(".Returns(", mock.TargetTypeQualifiedName, " value) => _setup.Returns(value);")
                .Write(Indents.Type, "}");
        }

        private string GetFullTypeName(ITypeSymbol type, NullableAnnotation nullableAnnotation) {
            var name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (nullableAnnotation == NullableAnnotation.Annotated && !type.IsValueType)
                name += "?";
            return name;
        }

        private readonly struct MockedMember
        {
            public MockedMember(
                MockedMemberKind kind,
                string name,
                ITypeSymbol type,
                string typeFullName,
                ImmutableArray<(string, string, int)> parameters,
                string handlerFieldName
            ) {
                Kind = kind;
                Name = name;
                Type = type;
                TypeFullName = typeFullName;
                Parameters = parameters;
                HandlerFieldName = handlerFieldName;
            }

            public MockedMemberKind Kind { get; }
            public string Name { get; }
            public ITypeSymbol Type { get; }
            public string TypeFullName { get; }
            public ImmutableArray<(string name, string typeFullName, int index)> Parameters { get; }
            public string HandlerFieldName { get; }
            public bool IsVoidMethod => Kind == MockedMemberKind.Method && Type.SpecialType == SpecialType.System_Void;
        }

        private enum MockedMemberKind {
            Method,
            Property
        }
    }
}
