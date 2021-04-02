//using System;
using System.Collections.Generic;
using System.Linq;
//using System.IO;
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

            var mocks = targetTypes.Select(GetMockInfo).ToList();

            for (var i = 0; i < mocks.Count; i++) {
                var mock = mocks[i];
                Log("Mocking type " + mock.TargetType.Name);

                var mockContent = individualMockGenerator.Generate(
                    mock,
                    newTargetType => {
                        var existing = mocks.FirstOrDefault(m => SymbolEqualityComparer.Default.Equals(m.TargetType, newTargetType));
                        if (existing.TargetType != null)
                            return existing;

                        var newMock = GetMockInfo(newTargetType);
                        mocks.Add(newMock);
                        return newMock;
                    }
                );
                context.AddSource(mock.MockTypeName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
            }
            Log("Execute End");
        }

        private MockInfo GetMockInfo(ITypeSymbol targetType) {
            var targetTypeQualifiedName = targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var mockTypeName = Regex.Replace(targetTypeQualifiedName, @"[^\w\d]", "_") + "_Mock";
            return new MockInfo(mockTypeName, targetType, targetTypeQualifiedName);
        }

        private void Log(string message) {
            //File.AppendAllText(LogPath, $"[{DateTime.Now.ToString("s")}] {message}{Environment.NewLine}");
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

                if (callerType is not { IsGenericType: true, Name: nameof(Mock), ContainingNamespace: { Name: var @namespace } } || @namespace != typeof(Mock).Namespace)
                    return;

                TypesToMock.Add(callerType.TypeArguments[0]);
            }
        }
    }
}