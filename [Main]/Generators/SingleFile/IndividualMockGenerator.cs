using System;
using System.Text;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Handlers;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class SourceMockTypeNames {
            public static readonly string MockMethodSetup = $"global::{typeof(MockMethodSetup<>).Namespace}.{nameof(MockMethodSetup)}";
            public static readonly string MockFuncHandler = $"global::{typeof(MockFuncHandler<>).Namespace}.{nameof(MockFuncHandler)}";
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
                        var handlerFieldName = "_" + char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
                        var methodReturnTypeName = method.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        AppendHandlerField(mockBuilder, handlerFieldName, methodReturnTypeName);
                        AppendSetupMethodInterface(setupInterfaceBuilder, method, methodReturnTypeName);
                        AppendSetupMethod(mockBuilder, method, methodReturnTypeName, setupInterfaceName, handlerFieldName);
                        AppendImplementation(mockBuilder, method, handlerFieldName);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }
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

        private void AppendHandlerField(StringBuilder builder, string handlerFieldName, string methodReturnTypeName) {
            builder
                .Append(Indents.Member)
                .Append("private readonly ")
                .Append(SourceMockTypeNames.MockFuncHandler)
                .Append("<")
                .Append(methodReturnTypeName)
                .Append(">")
                .Append(" ")
                .Append(handlerFieldName)
                .AppendLine(" = new();");
        }

        private void AppendSetupMethodInterface(StringBuilder builder, IMethodSymbol method, string methodReturnTypeName) {
            builder
                .Append(Indents.Member)
                .Append(SourceMockTypeNames.MockMethodSetup)
                .Append("<")
                .Append(methodReturnTypeName)
                .Append(">")
                .Append(" ")
                .Append(method.Name)
                .Append("();")
                .AppendLine();
        }

        private void AppendSetupMethod(StringBuilder builder, IMethodSymbol method, string methodReturnTypeName, string setupInterfaceName, string handlerFieldName) {
            builder
                .Append(Indents.Member)
                .Append(SourceMockTypeNames.MockMethodSetup)
                .Append("<")
                .Append(methodReturnTypeName)
                .Append(">")
                .Append(" ")
                .Append(setupInterfaceName)
                .Append(".")
                .Append(method.Name)
                .Append("() => ")
                .Append(handlerFieldName)
                .Append(".Setup;")
                .AppendLine();
        }

        private void AppendImplementation(StringBuilder builder, IMethodSymbol method, string handlerFieldName) {
            builder
                .Append(Indents.Member)
                .Append("public ")
                .Append(method.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
                .Append(" ")
                .Append(method.Name)
                .Append("() => ")
                .Append(handlerFieldName)
                .AppendLine(".Call();");
        }
    }
}
