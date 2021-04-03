using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SourceMock.Generators.Internal;

namespace SourceMock.Generators {
    [Generator]
    internal class MockGenerator : ISourceGenerator {
        private readonly MockClassGenerator _classGenerator = new MockClassGenerator();

        public void Initialize(GeneratorInitializationContext context) {
            context.RegisterForSyntaxNotifications(() => new TypesToMockCollectingReceiver());
        }

        public void Execute(GeneratorExecutionContext context) {
            var targetTypes = ((TypesToMockCollectingReceiver)context.SyntaxContextReceiver!).TypesToMock;
            foreach (var mock in targetTypes.Select(GetMockInfo)) {
                var mockContent = _classGenerator.Generate(mock);
                context.AddSource(mock.MockTypeName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
            }
        }

        private MockInfo GetMockInfo(ITypeSymbol targetType) {
            var targetTypeQualifiedName = targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var mockTypeName = Regex.Replace(targetTypeQualifiedName, @"[^\w\d]", "_") + "_Mock";
            return new MockInfo(mockTypeName, targetType, targetTypeQualifiedName);
        }

        private class TypesToMockCollectingReceiver : ISyntaxContextReceiver {
            public ISet<ITypeSymbol> TypesToMock { get; } = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context) {
                if (context.Node is not InvocationExpressionSyntax invocation)
                    return;

                if (invocation.Expression is not MemberAccessExpressionSyntax member)
                    return;

                if (member.Name is not IdentifierNameSyntax { Identifier: { ValueText: "Get" } })
                    return;

                if (context.SemanticModel.GetTypeInfo(member.Expression).Type is not INamedTypeSymbol callerType)
                    return;

                if (callerType is not { IsGenericType: true, Name: KnownTypes.Mock.Name, ContainingNamespace: { Name: KnownTypes.Mock.Namespace } })
                    return;

                TypesToMock.Add(callerType.TypeArguments[0]);
            }
        }
    }
}