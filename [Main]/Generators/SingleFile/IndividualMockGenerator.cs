using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Internal;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class KnownTypeNames {
            public static readonly string Mock = $"global::{typeof(Mock<>).Namespace}.{nameof(Mock)}";
            public static readonly string IMockMethodSetup = $"global::{typeof(IMockMethodSetup<>).Namespace}.{nameof(IMockMethodSetup)}";
            public static readonly string MockArgument = $"global::{typeof(MockArgument<>).Namespace}.{nameof(MockArgument)}";
            public static readonly string IMockArgument = $"global::{typeof(IMockArgument).FullName}";
            public static readonly string MockMemberHandler = $"global::{typeof(MockMemberHandler).FullName}";
            public static readonly string NoArguments = $"global::{typeof(NoArguments).FullName}";

            public static readonly string IReadOnlyList = $"global::{typeof(IReadOnlyList<>).Namespace}.{nameof(IReadOnlyList)}";
        }

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

            foreach (var member in mock.TargetType.GetMembers())
            {
                switch (member) {
                    case IMethodSymbol method:
                        WriteMethodMocks(mainWriter, setupInterfaceWriter, callsInterfaceWriter, method, requestOtherMock);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }

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
                .WriteGeneric(KnownTypeNames.IMockMethodSetup, mock.TargetTypeQualifiedName)
                .WriteLine(" {}")
                .WriteLine()
                .Write(Indents.Type, "public static ", mock.MockTypeName, ".Instance Get(this ")
                .WriteGeneric(KnownTypeNames.Mock, mock.TargetTypeQualifiedName)
                .WriteLine(" _) => new();")
                .Write("}");

            return mainWriter.ToString();
        }

        private void WriteMethodMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            CodeWriter callsInterfaceWriter,
            IMethodSymbol method,
            Func<ITypeSymbol, MockInfo> requestOtherMock
        ) {
            var handlerFieldName = "_" + char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
            var methodReturnTypeName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);
            var context = new MethodContext(method, methodReturnTypeName, handlerFieldName);

            WriteHandlerField(mockWriter, context);
            WriteSetupMethodInterface(setupInterfaceWriter, context, requestOtherMock);
            WriteSetupMethod(mockWriter, context, requestOtherMock);
            WriteImplementation(mockWriter, context);
            WriteCallsPropertyInterface(callsInterfaceWriter, context);
            WriteCallsProperty(mockWriter, context);
        }

        private void WriteHandlerField(CodeWriter writer, in MethodContext context) {
            writer.WriteLine(
                Indents.Member, "private readonly ", KnownTypeNames.MockMemberHandler, " ", context.HandlerFieldName, " = new();"
            );
        }

        private void WriteSetupMethodInterface(CodeWriter writer, in MethodContext context, Func<ITypeSymbol, MockInfo> requestOtherMock) {
            writer.Write(Indents.Member);
            WriteSetupMethodReturnType(writer, context, requestOtherMock);
            writer.Write(" ", context.Method.Name, "(");

            WriteSetupMethodParameters(writer, context, appendDefaultValue: true);

            writer.WriteLine(");");
        }

        private void WriteSetupMethod(CodeWriter writer, in MethodContext context, Func<ITypeSymbol, MockInfo> requestOtherMockName) {
            writer.Write(Indents.Member);
            var returnMock = WriteSetupMethodReturnType(writer, context, requestOtherMockName);
            writer.Write(" ISetup.", context.Method.Name, "(");
            WriteSetupMethodParameters(writer, context, appendDefaultValue: false);
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

            writer.WriteGeneric(KnownTypeNames.IMockMethodSetup, context.MethodReturnTypeName);
            return null;
        }

        private void WriteSetupMethodParameters(CodeWriter writer, in MethodContext context, bool appendDefaultValue) {
            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypeNames.MockArgument, GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
                first = false;
            }
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

        private void WriteCallsPropertyInterface(CodeWriter writer, in MethodContext context) {
            writer.Write(Indents.Member);
            WriteCallsPropertyReturnType(writer, context);
            writer.WriteLine(" ", context.Method.Name, " { get; }");
        }

        private void WriteCallsProperty(CodeWriter writer, in MethodContext context) {
            writer.Write(Indents.Member);
            WriteCallsPropertyReturnType(writer, context);
            writer.Write(" ICalls.", context.Method.Name, " => ", context.HandlerFieldName, ".Calls(");
            if (context.Method.Parameters.Length > 0) {
                writer.Write("args => (");
                var index = 0;
                foreach (var parameter in context.Method.Parameters) {
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
                writer.Write("_ => ", KnownTypeNames.NoArguments, ".Value");
            }
            writer.WriteLine(");");
        }

        private void WriteCallsPropertyReturnType(CodeWriter writer, in MethodContext context) {
            writer.Write(KnownTypeNames.IReadOnlyList, "<");

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
                writer.Write(KnownTypeNames.NoArguments);
            }

            writer.Write(">");
        }

        private void WriteReturnedInstanceType(CodeWriter writer, in MockInfo mock) {
            writer
                .WriteLine(Indents.Type, "public class ReturnedInstance : Instance, IReturnedSetup {")
                .WriteLine(Indents.Member, "private readonly ", KnownTypeNames.IMockMethodSetup, "<", mock.TargetTypeQualifiedName, "> _setup;")
                .WriteLine(Indents.Member, "public ReturnedInstance(", KnownTypeNames.IMockMethodSetup, "<", mock.TargetTypeQualifiedName, "> setup) {")
                .WriteLine(Indents.MemberBody, "_setup = setup;")
                .WriteLine(Indents.MemberBody, "_setup.Returns(this);")
                .WriteLine(Indents.Member, "}")
                .WriteLine()
                .Write(Indents.Member, "void ")
                .WriteGeneric(KnownTypeNames.IMockMethodSetup, mock.TargetTypeQualifiedName)
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
