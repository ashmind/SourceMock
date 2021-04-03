using System;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;

namespace SourceMock.Generators.SingleFile {
    internal partial class MockClassGenerator {
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
                switch (member) {
                    case IMethodSymbol method:
                        WriteMethodMocks(mainWriter, setupInterfaceWriter, callsInterfaceWriter, method, memberId, requestOtherMock);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }
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

        private void WriteMethodMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            CodeWriter callsInterfaceWriter,
            IMethodSymbol method,
            int uniqueMemberId,
            Func<ITypeSymbol, MockInfo> requestOtherMock
        ) {            
            var handlerFieldName = $"_{char.ToLowerInvariant(method.Name[0])}{method.Name.Substring(1)}{uniqueMemberId}Handler";
            var methodReturnTypeName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);
            var context = new MethodContext(method, methodReturnTypeName, handlerFieldName);

            WriteHandlerField(mockWriter, context);
            WriteSetupMethodInterface(setupInterfaceWriter, context, requestOtherMock);
            WriteSetupMethod(mockWriter, context, requestOtherMock);
            WriteImplementation(mockWriter, context);
            WriteCallsMethodInterface(callsInterfaceWriter, context);
            WriteCallsMethod(mockWriter, context);
        }

        private void WriteHandlerField(CodeWriter writer, in MethodContext context) {
            writer.WriteLine(
                Indents.Member, "private readonly ", KnownTypes.MockMemberHandler.FullName, " ", context.HandlerFieldName, " = new();"
            );
        }

        private void WriteSetupMethodInterface(CodeWriter writer, in MethodContext context, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            writer.Write(Indents.Member);
            WriteSetupMethodReturnType(writer, context, requestOtherMock);
            writer.Write(" ", context.Method.Name, "(");
            WriteSetupOrCallsMethodParameters(writer, context, appendDefaultValue: true);
            writer.WriteLine(");");
        }

        private void WriteSetupMethod(CodeWriter writer, in MethodContext context, Func<ITypeSymbol, MockInfo> requestOtherMockName) {
            writer.Write(Indents.Member);
            var returnMock = WriteSetupMethodReturnType(writer, context, requestOtherMockName);
            writer.Write(" ISetup.", context.Method.Name, "(");
            WriteSetupOrCallsMethodParameters(writer, context, appendDefaultValue: false);
            writer.Write(") => ");
            if (returnMock != null)
                writer.Write("new ", returnMock.Value.MockTypeName, ".ReturnedInstance(");

            writer.Write(context.HandlerFieldName, ".Setup");
            if (!context.Method.ReturnsVoid)
                writer.Write("<", context.MethodReturnTypeName, ">");

            writer.Write("(");

            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    writer.Write(", ");
                writer.Write(parameter.Name);
                first = false;
            }

            writer.Write(")");

            if (returnMock != null)
                writer.Write(")");
            writer.WriteLine(";");
        }

        private MockInfo? WriteSetupMethodReturnType(CodeWriter writer, MethodContext context, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            if (context.Method.ReturnType is { TypeKind: TypeKind.Interface } interfaceType) {
                var other = requestOtherMock(interfaceType);
                writer.Write(other.MockTypeName, ".IReturnedSetup");
                return other;
            }
            else if (context.Method.ReturnsVoid) {
                writer.Write(KnownTypes.IMockMethodSetup.FullName);
                return null;
            }

            writer.WriteGeneric(KnownTypes.IMockMethodSetup.FullName, context.MethodReturnTypeName);
            return null;
        }

        private void WriteImplementation(CodeWriter writer, in MethodContext context) {
            writer.Write(
                Indents.Member, "public ", context.MethodReturnTypeName, " ", context.Method.Name, "("
            );

            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    writer.Write(", ");

                writer.Write(GetFullTypeName(parameter.Type, parameter.NullableAnnotation), " ", parameter.Name);
                first = false;
            }

            writer.Write(") => ", context.HandlerFieldName, ".Call");
            if (!context.Method.ReturnsVoid)
                writer.Write("<", context.MethodReturnTypeName, ">");

            writer.Write("(");
            first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    writer.Write(", ");
                writer.Write(parameter.Name);
                first = false;
            }
            writer.WriteLine(");");
        }

        private void WriteCallsMethodInterface(CodeWriter writer, in MethodContext context) {
            writer.Write(Indents.Member);
            WriteCallsMethodReturnType(writer, context);
            writer.Write(" ", context.Method.Name, "(");
            WriteSetupOrCallsMethodParameters(writer, context, appendDefaultValue: true);
            writer.WriteLine(");");
        }

        private void WriteCallsMethod(CodeWriter writer, in MethodContext context) {
            writer.Write(Indents.Member);
            WriteCallsMethodReturnType(writer, context);
            writer.Write(" ICalls.", context.Method.Name, "(");
            WriteSetupOrCallsMethodParameters(writer, context, appendDefaultValue: false);
            writer.Write(") => ", context.HandlerFieldName, ".Calls(");
            var parameters = context.Method.Parameters;
            if (parameters.Length > 0) {
                writer.Write("args => (");
                var index = 0;
                foreach (var parameter in parameters) {
                    if (index != 0)
                        writer.Write(", ");
                    writer.Write(
                        "(", GetFullTypeName(parameter.Type, parameter.NullableAnnotation), ")args[", index.ToString(), "]!"
                    );
                    index += 1;
                }
                writer.Write(")");
            }
            else {
                writer.Write("_ => ", KnownTypes.NoArguments.FullName, ".Value");
            }
            foreach (var parameter in parameters) {
                writer.Write(", ", parameter.Name);
            }
            writer.WriteLine(");");
        }

        private void WriteSetupOrCallsMethodParameters(CodeWriter writer, in MethodContext context, bool appendDefaultValue) {
            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgument.FullName, GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
                first = false;
            }
        }

        private void WriteCallsMethodReturnType(CodeWriter writer, in MethodContext context) {
            writer.Write(KnownTypes.IReadOnlyList.FullName, "<");

            var parameters = context.Method.Parameters;
            if (parameters.Length > 1) {
                writer.Write("(");
                var first = true;
                foreach (var parameter in parameters) {
                    if (!first)
                        writer.Write(", ");
                    writer.Write(GetFullTypeName(parameter.Type, parameter.NullableAnnotation), " ", parameter.Name);
                    first = false;
                }
                writer.Write(")");
            }
            else if (parameters.Length == 1) {
                writer.Write(GetFullTypeName(parameters[0].Type, parameters[0].NullableAnnotation));
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

        private readonly struct MethodContext {
            public MethodContext(IMethodSymbol method, string methodReturnTypeName, string handlerFieldName) {
                Method = method;
                MethodReturnTypeName = methodReturnTypeName;
                HandlerFieldName = handlerFieldName;
            }

            public IMethodSymbol Method { get; }
            public string MethodReturnTypeName { get; }
            public string HandlerFieldName { get; }
        }
    }
}
