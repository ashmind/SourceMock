using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Internal;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class KnownTypeNames {
            public static readonly string MockMethodSetup = $"global::{typeof(MockMethodSetup<>).Namespace}.{nameof(MockMethodSetup)}";
            public static readonly string MockArgument = $"global::{typeof(MockArgument<>).Namespace}.{nameof(MockArgument)}";
            public static readonly string MockHandler = $"global::{typeof(MockHandler).FullName}";
            public static readonly string NoArguments = $"global::{typeof(NoArguments).FullName}";

            public static readonly string IReadOnlyList = $"global::{typeof(IReadOnlyList<>).Namespace}.{nameof(IReadOnlyList)}";
        }

        private static class Indents {
            public const string Member = "    ";
        }

        public string Generate(in MockInfo mock) {
            var setupInterfaceName = "I_" + mock.MockTypeName + "_Setup";
            var callsInterfaceName = "I_" + mock.MockTypeName + "_Calls";

            var mockBuilder = new StringBuilder("public class ")
                .Append(mock.MockTypeName)
                .Append(" : ")
                .Append(mock.TargetTypeQualifiedName)
                .Append(", ")
                .Append(setupInterfaceName)
                .Append(", ")
                .Append(callsInterfaceName)
                .AppendLine(" {")
                .Append(Indents.Member)
                .Append("public ")
                .Append(setupInterfaceName)
                .AppendLine(" Setup => this;")
                .Append(Indents.Member)
                .Append("public ")
                .Append(callsInterfaceName)
                .AppendLine(" Calls => this;")
                .AppendLine();

            var setupInterfaceBuilder = new StringBuilder("public interface ")
                .Append(setupInterfaceName)
                .AppendLine(" {");

            var callsInterfaceBuilder = new StringBuilder("public interface ")
                .Append(callsInterfaceName)
                .AppendLine(" {");

            foreach (var member in mock.TargetType.GetMembers())
            {
                switch (member) {
                    case IMethodSymbol method:
                        AppendMethodMocks(mockBuilder, (setupInterfaceName, setupInterfaceBuilder), (callsInterfaceName, callsInterfaceBuilder), method);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }

                mockBuilder.AppendLine();
            }

            mockBuilder.Append("}");
            setupInterfaceBuilder.Append("}");
            callsInterfaceBuilder.Append("}");

            return new StringBuilder(
                mockBuilder.Length + (Environment.NewLine.Length * 2) + setupInterfaceBuilder.Length + (Environment.NewLine.Length * 2) + callsInterfaceBuilder.Length
            ).Append(mockBuilder)
             .AppendLine()
             .AppendLine()
             .Append(setupInterfaceBuilder)
             .AppendLine()
             .AppendLine()
             .Append(callsInterfaceBuilder)
             .ToString();
        }

        private void AppendMethodMocks(
            StringBuilder mockBuilder,
            (string name, StringBuilder builder) setupInterface,
            (string name, StringBuilder builder) callsInterface,
            IMethodSymbol method
        ) {
            var handlerFieldName = "_" + char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
            var methodReturnTypeName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);
            var context = new MethodContext(method, methodReturnTypeName, handlerFieldName);

            AppendHandlerField(mockBuilder, context);
            AppendSetupMethodInterface(setupInterface.builder, context);
            AppendSetupMethod(mockBuilder, context, setupInterface.name);
            AppendImplementation(mockBuilder, context);
            AppendCallsPropertyInterface(callsInterface.builder, context);
            AppendCallsProperty(mockBuilder, context, callsInterface.name);
        }

        private void AppendHandlerField(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append("private readonly ")
                .Append(KnownTypeNames.MockHandler)
                .Append(" ")
                .Append(context.HandlerFieldName)
                .AppendLine(" = new();");
        }

        private void AppendSetupMethodInterface(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append(KnownTypeNames.MockMethodSetup)
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
                .Append(KnownTypeNames.MockMethodSetup)
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
                    .Append(KnownTypeNames.MockArgument)
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

        private void AppendCallsPropertyInterface(StringBuilder builder, in MethodContext context) {
            builder.Append(Indents.Member);
            AppendCallsReturnValue(builder, context);
            builder
                .Append(" ")
                .Append(context.Method.Name)
                .AppendLine(" { get; }");
        }

        private void AppendCallsProperty(StringBuilder builder, in MethodContext context, string callsInterfaceName) {
            builder.Append(Indents.Member);
            AppendCallsReturnValue(builder, context);
            builder
                .Append(" ")
                .Append(callsInterfaceName)
                .Append(".")
                .Append(context.Method.Name)
                .Append(" => ")
                .Append(context.HandlerFieldName)
                .Append(".Calls(");
            if (context.Method.Parameters.Length > 0) {
                builder.Append("args => (");
                var index = 0;
                foreach (var parameter in context.Method.Parameters) {
                    if (index != 0)
                        builder.Append(", ");
                    builder
                        .Append("(")
                        .Append(GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                        .Append(")args[")
                        .Append(index)
                        .Append("]!");
                    index += 1;
                }
                builder.Append(")");
            }
            else {
                builder
                    .Append("_ => ")
                    .Append(KnownTypeNames.NoArguments)
                    .Append(".Value");
            }
            builder.AppendLine(");");
        }

        private void AppendCallsReturnValue(StringBuilder builder, in MethodContext context) {
            builder
                .Append(KnownTypeNames.IReadOnlyList)
                .Append("<");

            var parameters = context.Method.Parameters;
            if (parameters.Length > 1) {
                builder.Append("(");
                var first = true;
                foreach (var parameter in parameters) {
                    if (!first)
                        builder.Append(", ");
                    builder
                        .Append(GetFullTypeName(parameter.Type, parameter.NullableAnnotation))
                        .Append(" ")
                        .Append(parameter.Name);
                    first = false;
                }
                builder.Append(")");
            }
            else if (parameters.Length == 1) {
                builder.Append(GetFullTypeName(parameters[0].Type, parameters[0].NullableAnnotation));
            }
            else {
                builder.Append(KnownTypeNames.NoArguments);
            }

            builder.Append(">");
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
