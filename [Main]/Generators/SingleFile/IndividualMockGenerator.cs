using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Internal;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class KnownTypeNames {
            public static readonly string Mock = $"global::{typeof(Mock<>).Namespace}.{nameof(Mock)}";
            public static readonly string MockMethodSetup = $"global::{typeof(MockMethodSetup<>).Namespace}.{nameof(MockMethodSetup)}";
            public static readonly string MockArgument = $"global::{typeof(MockArgument<>).Namespace}.{nameof(MockArgument)}";
            public static readonly string MockMemberHandler = $"global::{typeof(MockMemberHandler).FullName}";
            public static readonly string NoArguments = $"global::{typeof(NoArguments).FullName}";

            public static readonly string IReadOnlyList = $"global::{typeof(IReadOnlyList<>).Namespace}.{nameof(IReadOnlyList)}";
        }

        private static class Indents {
            public const string Member = "        ";
        }

        public string Generate(in MockInfo mock) {
            var extensionClassName = mock.MockTypeName + "_Extensions";

            var builder = new StringBuilder("public static class ")
                .Append(mock.MockTypeName)
                .AppendLine(" {");

            builder
                .Append("    public class Instance : ")
                .Append(mock.TargetTypeQualifiedName)
                .AppendLine(", ISetup, ICalls {")
                .Append(Indents.Member)
                .AppendLine("public ISetup Setup => this;")
                .Append(Indents.Member)
                .AppendLine("public ICalls Calls => this;")
                .AppendLine();

            var setupInterfaceBuilder = new StringBuilder("    public interface ISetup {")
                .AppendLine();

            var callsInterfaceBuilder = new StringBuilder("    public interface ICalls {")
                .AppendLine();

            foreach (var member in mock.TargetType.GetMembers())
            {
                switch (member) {
                    case IMethodSymbol method:
                        AppendMethodMocks(builder, setupInterfaceBuilder, callsInterfaceBuilder, method);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }

                builder.AppendLine();
            }

            builder
                .AppendLine("    }")
                .AppendLine();

            setupInterfaceBuilder.Append("    }");
            builder
                .Append(setupInterfaceBuilder)
                .AppendLine()
                .AppendLine();

            callsInterfaceBuilder.Append("    }");
            builder
                .Append(callsInterfaceBuilder)
                .AppendLine()
                .AppendLine();

            builder
                .AppendLine("}")
                .AppendLine();

            builder
                .Append("public static class ")
                .Append(extensionClassName)
                .AppendLine("{")
                .Append("    public static ")
                .Append(mock.MockTypeName)
                .Append(".Instance Get(this ")
                .Append(KnownTypeNames.Mock)
                .Append("<")
                .Append(mock.TargetTypeQualifiedName)
                .Append(">")
                .AppendLine(" _) => new();")
                .AppendLine("}");

            return builder.ToString();
        }

        private void AppendMethodMocks(
            StringBuilder mockBuilder,
            StringBuilder setupInterfaceBuilder,
            StringBuilder callsInterfaceBuilder,
            IMethodSymbol method
        ) {
            var handlerFieldName = "_" + char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
            var methodReturnTypeName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);
            var context = new MethodContext(method, methodReturnTypeName, handlerFieldName);

            AppendHandlerField(mockBuilder, context);
            AppendSetupMethodInterface(setupInterfaceBuilder, context);
            AppendSetupMethod(mockBuilder, context);
            AppendImplementation(mockBuilder, context);
            AppendCallsPropertyInterface(callsInterfaceBuilder, context);
            AppendCallsProperty(mockBuilder, context);
        }

        private void AppendHandlerField(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append("private readonly ")
                .Append(KnownTypeNames.MockMemberHandler)
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

        private void AppendSetupMethod(StringBuilder builder, in MethodContext context) {
            builder
                .Append(Indents.Member)
                .Append(KnownTypeNames.MockMethodSetup)
                .Append("<")
                .Append(context.MethodReturnTypeName)
                .Append("> ISetup.")
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

        private void AppendCallsProperty(StringBuilder builder, in MethodContext context) {
            builder.Append(Indents.Member);
            AppendCallsReturnValue(builder, context);
            builder
                .Append(" ICalls.")
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
