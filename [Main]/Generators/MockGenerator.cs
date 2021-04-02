using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SourceMock.Generators.Models;
using SourceMock.Generators.SingleFile;

namespace SourceMock.Generators {
    [Generator]
    internal class MockGenerator : ISourceGenerator {
        //private const string LogPath = @"d:\Development\VS 2019\SourceMock\[Main]\generator.log";

        public void Initialize(GeneratorInitializationContext context) {
            context.RegisterForSyntaxNotifications(() => new TypesToMockCollectingReceiver());
        }

        public void Execute(GeneratorExecutionContext context) {
            Log("Execute Start");
            var targetTypes = ((TypesToMockCollectingReceiver)context.SyntaxContextReceiver!).TypesToMock;
            var individualMockGenerator = new IndividualMockGenerator();
            foreach (var targetType in targetTypes) {
                var targetTypeQualifiedName = targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var mockTypeName = Regex.Replace(targetTypeQualifiedName, @"[^\w\d]", "_") + "_Mock";

                var mockContent = individualMockGenerator.Generate(new MockInfo(mockTypeName, targetType, targetTypeQualifiedName));
                context.AddSource(mockTypeName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
            }
            Log("Execute End");
        }

        private void Log(string message) {
            //File.AppendAllText(LogPath, $"[{DateTime.Now.ToString("s")}] {message}{Environment.NewLine}");
        }

        private class TypesToMockCollectingReceiver : ISyntaxContextReceiver {
            public ISet<ITypeSymbol> TypesToMock { get; } = new HashSet<ITypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context) {
                if (context.Node is not InvocationExpressionSyntax invocation)
                    return;

                if (invocation.Expression is not MemberAccessExpressionSyntax member)
                    return;

                if (member.Name is not IdentifierNameSyntax { Identifier: { ValueText: "Get" } })
                    return;

                if (context.SemanticModel.GetTypeInfo(member.Expression).Type is not INamedTypeSymbol callerType)
                    return;

                if (callerType is not { IsGenericType: true, Name: nameof(Mock), ContainingNamespace: { Name: var @namespace } } || @namespace != typeof(Mock).Namespace)
                    return;

                TypesToMock.Add(callerType.TypeArguments[0]);
            }
        }
    }
}