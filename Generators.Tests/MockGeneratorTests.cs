using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Azure.Storage.Blobs;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace SourceMock.Generators.Tests {
    public class MockGeneratorTests {
        private readonly ITestOutputHelper _output;

        public MockGeneratorTests(ITestOutputHelper output) {
            _output = output;
        }

        [Theory]
        [InlineData(typeof(ILogger<>), "LoggerMock.cs")]
        [InlineData(typeof(BlobContainerClient), "BlobContainerClientMock.cs")]
        public void Generator_GeneratesExpectedMock(Type targetType, string expectedTextFileName) {
            // Arrange
            var targetTypeFullName = targetType.IsGenericType
                // Remove `1 at the end. Would not work with more than 9 generic arguments.
                ? targetType.FullName!.Remove(targetType.FullName.Length - 2) + "<>"
                : targetType.FullName!;
            var originalCompilation = CreateCompilationFromText(
                $@"[assembly: SourceMock.GenerateMocksForTypes(new[] {{
                    typeof({targetTypeFullName})
                }})]",
                targetType.Assembly
            );

            var generator = new MockGenerator();
            var driver = (GeneratorDriver)CSharpGeneratorDriver.Create(generator);

            // Act
            driver = driver.RunGeneratorsAndUpdateCompilation(originalCompilation, out _, out var diagnostics);

            // Assert
            Assert.Empty(diagnostics);
            var tree = Assert.Single(driver.GetRunResult().GeneratedTrees);
            var text = tree.ToString();
            _output.WriteLine(text);
            Assert.Equal(GetExpectedText(expectedTextFileName), text);
        }

        private CSharpCompilation CreateCompilationFromText(string source, Assembly referenceAssembly) {
            var initialReferenceAssemblies = new[] {
                typeof(GenerateMocksForTypesAttribute).Assembly,
                referenceAssembly
            };
            var referenceAssemblies = initialReferenceAssemblies.Concat(
                initialReferenceAssemblies.SelectMany(GetReferencesRecursive)
            ).Distinct();

            var compilation = CSharpCompilation.Create("_",
                new[] { CSharpSyntaxTree.ParseText(source) },
                referenceAssemblies.Select(a => MetadataReference.CreateFromFile(a.Location)),
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            Assert.Empty(compilation.GetDiagnostics());

            return compilation;
        }

        private IEnumerable<Assembly> GetReferencesRecursive(Assembly assembly) {
            foreach (var reference in assembly.GetReferencedAssemblies()) {
                var referencedAssembly = Assembly.Load(reference);
                yield return referencedAssembly;
                foreach (var nestedReference in GetReferencesRecursive(referencedAssembly)) {
                    yield return nestedReference;
                }
            }
        }

        private string GetExpectedText(string name) {
            return File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Expected", name));
        }
    }
}
