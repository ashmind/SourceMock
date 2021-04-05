using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using SourceMock.Generators.Internal;

namespace SourceMock.Generators {
    [Generator]
    internal class MockGenerator : ISourceGenerator {
        private static class DiagnosticDescriptors {
            #pragma warning disable RS2008 // Enable analyzer release tracking
            public static readonly DiagnosticDescriptor SingleMockFailedToGenerate = new DiagnosticDescriptor(
                "SM0001", "Failed to generate a single mock", "Failed to generate mock for {0}: {1}",
                "Generation", DiagnosticSeverity.Warning, isEnabledByDefault: true
            );
            public static readonly DiagnosticDescriptor RegexPatternFailedToParse = new DiagnosticDescriptor(
                "SM0011", "Regex pattern is not a valid regex", "Regex pattern \"{0}\" cannot be parsed: {1}",
                "Generation", DiagnosticSeverity.Error, isEnabledByDefault: true
            );
            #pragma warning restore RS2008 // Enable analyzer release tracking
        }

        private readonly MockClassGenerator _classGenerator = new();

        public void Initialize(GeneratorInitializationContext context) {
        }

        public void Execute(GeneratorExecutionContext context) {
            foreach (var attribute in context.Compilation.Assembly.GetAttributes()) {
                if (attribute.AttributeClass is not { Name: KnownTypes.GenerateMocksForAssemblyOfAttribute.Name } attributeClass)
                    continue;

                if (!KnownTypes.GenerateMocksForAssemblyOfAttribute.NamespaceMatches(attributeClass.ContainingNamespace))
                    continue;

                GenerateMocksForAttributeTargetAssembly(attribute, context);
            }
        }

        private void GenerateMocksForAttributeTargetAssembly(AttributeData attribute, in GeneratorExecutionContext context) {
            // intermediate code state? just in case
            if (attribute.ConstructorArguments[0].Value is not INamedTypeSymbol anyTypeInAssembly)
                return;

            string? excludePattern = null;
            foreach (var named in attribute.NamedArguments) {
                if (named.Key == KnownTypes.GenerateMocksForAssemblyOfAttribute.NamedParameters.ExcludeRegex)
                    excludePattern = named.Value.Value as string;
            }

            Regex? excludeRegex;
            try {
                excludeRegex = excludePattern != null ? new Regex(excludePattern) : null;
            }
            catch (ArgumentException ex) {
                var attributeSyntax = attribute.ApplicationSyntaxReference;
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.RegexPatternFailedToParse,
                    attributeSyntax?.SyntaxTree.GetLocation(attributeSyntax.Span),
                    excludePattern, ex.Message
                ));
                return;
            }

            GenerateMocksForNamespace(anyTypeInAssembly.ContainingAssembly.GlobalNamespace, excludeRegex, context);
        }

        private void GenerateMocksForNamespace(INamespaceSymbol parent, Regex? excludeRegex, in GeneratorExecutionContext context) {
            foreach (var member in parent.GetMembers()) {
                switch (member) {
                    case INamedTypeSymbol type:
                        GenerateMockForTypeIfApplicable(type, excludeRegex, context);
                        break;

                    case INamespaceSymbol nested:
                        GenerateMocksForNamespace(nested, excludeRegex, context);
                        break;
                }
            }
        }

        private void GenerateMockForTypeIfApplicable(INamedTypeSymbol type, Regex? excludeRegex, in GeneratorExecutionContext context) {
            if (type.TypeKind != TypeKind.Interface)
                return;

            if (type.DeclaredAccessibility != Accessibility.Public) {
                var isVisibleInternal = type.DeclaredAccessibility == Accessibility.Internal
                                     && type.ContainingAssembly.GivesAccessTo(context.Compilation.Assembly);
                if (!isVisibleInternal)
                    return;
            }

            var fullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (excludeRegex != null && excludeRegex.IsMatch(fullName))
                return;

            GenerateMockForType(new MockTarget(type, fullName), context);
        }

        private void GenerateMockForType(MockTarget target, in GeneratorExecutionContext context) {
            string mockContent;
            try {
                mockContent = _classGenerator.Generate(target);
            }
            catch (Exception ex) {
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.SingleMockFailedToGenerate, location: null, target.FullTypeName, ex.ToString()
                ));
                return;
            }

            var mockFileName = Regex.Replace(target.FullTypeName, @"[^\w\d]", "_");
            context.AddSource(mockFileName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
        }
    }
}