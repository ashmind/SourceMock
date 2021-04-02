using System;
using System.Text;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Internal;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class SourceMockTypeNames {
            public static readonly string MockMethodSetup = $"global::{typeof(MockMethodSetup<>).Namespace}.{nameof(MockMethodSetup)}";
            public static readonly string MockArgument = $"global::{typeof(MockArgument<>).Namespace}.{nameof(MockArgument)}";
            public static readonly string MockHandler = $"global::{typeof(MockHandler).FullName}";
        }

        private static class Indents {
            public const string Member = "    ";
        }

        public string Generate(in MockInfo mock) {
            var setupInterfaceName = "I_" + mock.MockTypeName + "_Setup";
            var mockBuilder = new StringBuilder("public class ")
                .Append(mock.MockTypeName)
                .Append(" : ")
                .Append(mock.TargetTypeQualifiedName)
                .Append(", ")
                .Append(setupInterfaceName)
                .AppendLine(" {")
                .Append(Indents.Member)
                .Append("public ")
                .Append(setupInterfaceName)
                .AppendLine(" Setup => this;")
                .AppendLine();

            var setupInterfaceBuilder = new StringBuilder("public interface ")
                .Append(setupInterfaceName)
                .AppendLine(" {");

            foreach (var member in mock.TargetType.GetMembers())
            {
                switch (member) {
                    case IMethodSymbol method:
                        AppendMethodMocks(mockBuilder, setupInterfaceBuilder, method, setupInterfaceName);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }

                mockBuilder.AppendLine();
            }

            mockBuilder.Append("}");
            setupInterfaceBuilder.Append("}");

            return new StringBuilder(mockBuilder.Length + (Environment.NewLine.Length * 2) + setupInterfaceBuilder.Length)
                .Append(mockBuilder)
                .AppendLine()
                .AppendLine()
                .Append(setupInterfaceBuilder)
                .ToString();
        }

        private void AppendMethodMocks(StringBuilder mockBuilder, StringBuilder setupInterfaceBuilder, IMethodSymbol method, string setupInterfaceName) {
            var handlerFieldName = "_" + char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
            var methodReturnTypeName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);
            var context = new MethodContext(method, methodReturnTypeName, handlerFieldName);

            AppendHandlerField(mockBuilder, context);
            AppendSetupMethodInterface(setupInterfaceBuilder, context);
            AppendSetupMethod(mockBuilder, context, setupInterfaceName);
            AppendImplementation(mockBuilder, context);
        }

        private void AppendHandlerField(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append("private readonly ")
                .Append(SourceMockTypeNames.MockHandler)
                .Append(" ")
                .Append(context.HandlerFieldName)
                .AppendLine(" = new();");
        }

        private void AppendSetupMethodInterface(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append(SourceMockTypeNames.MockMethodSetup)
                .Append("<")
                .Append(context.MethodReturnTypeName)
                .Append(">")
                .Append(" ")
                .Append(context.Method.Name)
                .Append("(");
            AppendSetupMethodParameters(builder, context, appendDefaultValue: true);
            builder
                .Append(");")
                .AppendLine();
        }

        private void AppendSetupMethod(StringBuilder builder, in MethodContext context, string setupInterfaceName) {
            builder
                .Append(Indents.Member)
                .Append(SourceMockTypeNames.MockMethodSetup)
                .Append("<")
                .Append(context.MethodReturnTypeName)
                .Append(">")
                .Append(" ")
                .Append(setupInterfaceName)
                .Append(".")
                .Append(context.Method.Name)
                .Append("(");
            AppendSetupMethodParameters(builder, context, appendDefaultValue: false);
            builder
                .Append(") => ")
                .Append(context.HandlerFieldName)
                .Append(".Setup");
            if (!context.Method.ReturnsVoid) {
                builder
                    .Append("<")
                    .Append(context.MethodReturnTypeName)
                    .Append(">");
            }
            builder.Append("(");

            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    builder.Append(", ");
                builder.Append(parameter.Name);
                first = false;
            }

            builder.AppendLine(");");
        }

        private void AppendSetupMethodParameters(StringBuilder builder, in MethodContext context, bool appendDefaultValue) {
            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    builder.Append(", ");

                builder
                    .Append(SourceMockTypeNames.MockArgument)
                    .Append("<")
                    .Append(GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                    .Append("> ")
                    .Append(parameter.Name);
                if (appendDefaultValue)
                    builder.Append(" = default");
                first = false;
            }
        }

        private void AppendImplementation(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append("public ")
                .Append(context.MethodReturnTypeName)
                .Append(" ")
                .Append(context.Method.Name)
                .Append("(");

            var first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    builder.Append(", ");

                builder
                    .Append(GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                    .Append(" ")
                    .Append(parameter.Name);
                first = false;
            }

            builder
                .Append(") => ")
                .Append(context.HandlerFieldName)
                .Append(".Call");
            if (!context.Method.ReturnsVoid) {
                builder
                    .Append("<")
                    .Append(context.MethodReturnTypeName)
                    .Append(">");
            }
            builder.Append("(");

            first = true;
            foreach (var parameter in context.Method.Parameters) {
                if (!first)
                    builder.Append(", ");
                builder.Append(parameter.Name);
                first = false;
            }
            builder.AppendLine(");");
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
