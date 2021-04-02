using System;
using System.IO;
using System.Linq;
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
        private const string LogPath = @"d:\Development\VS 2019\SourceMock\[Main]\generator.log";

        public void Initialize(GeneratorInitializationContext context) {
        }

        public void Execute(GeneratorExecutionContext context) {
            Log("Execute Start");
            var mocks = context.Compilation.GetMethodBodyDiagnostics()
                .Select(d => GetMockInfoFromDiagnostic(d, context))
                .OfType<MockInfo>()
                .GroupBy(m => m.TargetType)
                .Select(g => g.First())
                .OrderBy(m => m.TargetTypeQualifiedName);

            var mocksClassGenerator = new MocksClassGenerator();
            var mocksClassBuilder = mocksClassGenerator.Start();

            var individualMockGenerator = new IndividualMockGenerator();
            foreach (var mock in mocks)
            {
                mocksClassGenerator.Append(mocksClassBuilder, mock);
                context.AddSource(mock.MockTypeName + ".cs", SourceText.From(individualMockGenerator.Generate(mock), Encoding.UTF8));
            }

            context.AddSource("Mocks.cs", SourceText.From(mocksClassGenerator.End(mocksClassBuilder), Encoding.UTF8));

            Log("Execute End");
        }

        private MockInfo? GetMockInfoFromDiagnostic(Diagnostic diagnostic, GeneratorExecutionContext context) {
            if (diagnostic.Id != "CS0103")
                return null;

            if (diagnostic.Location.SourceTree is not {} sourceTree)
                return null;

            if (!sourceTree.TryGetRoot(out var root))
                throw new Exception("Hmm");

            var missing = root.FindNode(diagnostic.Location.SourceSpan);
            Log($"Missing: {missing} ({missing.GetType()})");

            if (missing is not IdentifierNameSyntax identifier || identifier.Identifier.ValueText != "Mocks")
                return null;

            Log($"Parent: {missing.Parent} ({missing.Parent?.GetType()})");
            if (missing.Parent is not MemberAccessExpressionSyntax memberAccess)
                return null;

            Log($"Parent: {memberAccess.Parent} ({memberAccess.Parent?.GetType()})");
            if (memberAccess.Parent is not InvocationExpressionSyntax call)
                return null;

            var model = context.Compilation.GetSemanticModel(sourceTree);
            var targetType = model.GetTypeInfo(call.ArgumentList.Arguments[0].Expression).Type;
            if (targetType == null)
                throw new Exception("Hmm2");

            Log($"Type: {targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}");
            var targetTypeQualifiedName = targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var mockTypeName = Regex.Replace(targetTypeQualifiedName, @"[^\w\d]", "_") + "_Mock";

            return new MockInfo(mockTypeName, targetType, targetTypeQualifiedName);
        }

        private void Log(string message) {
            //File.AppendAllText(LogPath, $"[{DateTime.Now.ToString("s")}] {message}{Environment.NewLine}");
        }
    }
}
