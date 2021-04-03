using System;
using System.Collections.Generic;
using System.Linq;
//using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SourceMock.Generators.Internal;

namespace SourceMock.Generators {
    [Generator]
    internal class MockGenerator : ISourceGenerator {
        //private const string LogPath = @"d:\Development\VS 2019\SourceMock\Generators\generator.log";

        public void Initialize(GeneratorInitializationContext context) {
            context.RegisterForSyntaxNotifications(() => new TypesToMockCollectingReceiver());
        }

        public void Execute(GeneratorExecutionContext context) {
            try {
                ExecuteSafe(context);
            }
            catch (Exception ex) {
                Log(ex.ToString());
                throw;
            }
        }

        private void ExecuteSafe(GeneratorExecutionContext context) {
            Log("Execute Start");
            var targetTypes = ((TypesToMockCollectingReceiver)context.SyntaxContextReceiver!).TypesToMock;
            var individualMockGenerator = new MockClassGenerator();

            var mocks = targetTypes.Select(GetMockInfo).ToList();

            for (var i = 0; i < mocks.Count; i++) {
                var mock = mocks[i];
                Log("Mocking type " + mock.TargetType.Name);

                var mockContent = individualMockGenerator.Generate(
                    mock,
                    newTargetType => RequestMock(mocks, newTargetType)
                );
                context.AddSource(mock.MockTypeName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
            }
            Log("Execute End");
        }

        private MockInfo RequestMock(IList<MockInfo> mocks, ITypeSymbol targetType) {
            var existing = mocks.FirstOrDefault(m => SymbolEqualityComparer.Default.Equals(m.TargetType, targetType));
            if (existing.TargetType != null)
                return existing;

            var newMock = GetMockInfo(targetType);
            mocks.Add(newMock);
            return newMock;
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

                if (callerType is not { IsGenericType: true, Name: KnownTypes.Mock.Name, ContainingNamespace: { Name: KnownTypes.Mock.Namespace } })
                    return;

                TypesToMock.Add(callerType.TypeArguments[0]);
            }
        }
    }
}