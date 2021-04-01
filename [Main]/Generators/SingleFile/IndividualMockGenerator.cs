using System;
using System.Text;
using Microsoft.CodeAnalysis;
using SourceMock.Generators.Models;
using SourceMock.Handlers;

namespace SourceMock.Generators.SingleFile {
    internal class IndividualMockGenerator {
        private static class SourceMockTypeNames {
            public static readonly string MockSetup = "global::" + typeof(MockSetup).FullName;
            public static readonly string MockHandler = "global::" + typeof(MockFuncHandler).FullName;
        }

        private static class Indents {
            public const string Member = "    ";
        }

        public string Generate(in MockInfo mock) {
            var builder = new StringBuilder("public class ")
                .Append(mock.MockTypeName)
                .Append(" : ")
                .Append(mock.TargetTypeQualifiedName)
                .AppendLine(" {");

            foreach (var member in mock.TargetType.GetMembers())
            {
                switch (member) {
                    case IMethodSymbol method:
                        var handlerFieldName = "_" + Char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1) + "Handler";
                        AppendHandlerField(builder, handlerFieldName);
                        AppendSetupMethod(builder, method, handlerFieldName);
                        AppendExplicitImplementation(builder, method, mock, handlerFieldName);
                        break;

                    default:
                        throw new NotSupportedException($"Member {mock.TargetTypeQualifiedName}.{member.Name} is {member.GetType()} which is not yet supported.");
                }
            }

            return builder.Append("}").ToString();
        }

        private void AppendHandlerField(StringBuilder builder, string handlerFieldName) {
            builder
                .Append(Indents.Member)
                .Append("private readonly ")
                .Append(SourceMockTypeNames.MockHandler)
                .Append(" ")
                .Append(handlerFieldName)
                .AppendLine(" = new();");
        }

        private void AppendSetupMethod(StringBuilder builder, IMethodSymbol method, string handlerFieldName) {
            builder
                .Append(Indents.Member)
                .Append("public ")
                .Append(SourceMockTypeNames.MockSetup)
                .Append(" Setup")
                .Append(method.Name)
                .Append("() => ")
                .Append(handlerFieldName)
                .Append(".Setup;")
                .AppendLine();
        }

        private void AppendExplicitImplementation(StringBuilder builder, IMethodSymbol method, in MockInfo mock, string handlerFieldName) {
            builder
                .Append(Indents.Member)
                .Append(method.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
                .Append(" ")
                .Append(mock.TargetTypeQualifiedName)
                .Append(".")
                .Append(method.Name)
                .Append("() => ")
                .Append(handlerFieldName)
                .AppendLine(".Call();");
        }
    }
}
