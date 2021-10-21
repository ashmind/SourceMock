using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using SourceMock.Generators.Internal;
using SourceMock.Generators.Internal.Models;

namespace SourceMock.Generators {
    [Generator]
    internal class MockGenerator : ISourceGenerator, IDisposable {
        private static class DiagnosticDescriptors {
            #pragma warning disable RS2008 // Enable analyzer release tracking
            public static readonly DiagnosticDescriptor SingleMockFailedToGenerate = new(
                "SourceMock001", "Failed to generate a single mock", "Failed to generate mock for {0}: {1}",
                "Generation", DiagnosticSeverity.Warning, isEnabledByDefault: true
            );
            public static readonly DiagnosticDescriptor RegexPatternFailedToParse = new(
                "SourceMock002", "Regex pattern is not a valid regex", "Regex pattern \"{0}\" cannot be parsed: {1}",
                "Generation", DiagnosticSeverity.Error, isEnabledByDefault: true
            );
            #pragma warning restore RS2008 // Enable analyzer release tracking
        }

        private readonly GeneratorCache<(IAssemblySymbol assembly, string? excludePattern), ImmutableArray<(string name, SourceText source)>> _mockedAssemblyCache = new("MockedAssemblyCache");
        private readonly GeneratorCache<INamedTypeSymbol, (string name, SourceText source)> _mockedTypeCache = new("MockedTypeCache", NamedTypeSymbolCacheKeyEqualityComparer.Default);
        private readonly MockTargetModelFactory _modelFactory = new();
        private readonly MockClassGenerator _classGenerator;

        public MockGenerator() {
            GeneratorLog.Log("MockGenerator constructor");
            _classGenerator = new(_modelFactory);
        }

        public void Initialize(GeneratorInitializationContext context) {
        }

        [PerformanceSensitive("")]
        public void Execute(GeneratorExecutionContext context) {
            GeneratorLog.Log("MockGenerator.Execute started");
            try {
                GeneratorLog.Log("Get attributes started");
                var attributes = context.Compilation.Assembly.GetAttributes();
                GeneratorLog.Log("Get attributes finished");
                foreach (var attribute in attributes) {
                    ProcessAssemblyAttribute(attribute, context);
                }
            }
            catch (Exception ex) {
                GeneratorLog.Log("MockGenerator.Execute failed: " + ex);
                throw;
            }
            GeneratorLog.Log("MockGenerator.Execute finished");
        }

        private void ProcessAssemblyAttribute(AttributeData attribute, in GeneratorExecutionContext context) {
            if (attribute.AttributeClass is not {} attributeClass)
                return;

            switch (attributeClass.Name) {
                case KnownTypes.GenerateMocksForAssemblyOfAttribute.Name:
                    if (!KnownTypes.GenerateMocksForAssemblyOfAttribute.NamespaceMatches(attributeClass.ContainingNamespace))
                        return;
                    ProcessGenerateMocksForAssemblyAttribute(attribute, context);
                    break;

                case KnownTypes.GenerateMocksForTypesAttribute.Name:
                    if (!KnownTypes.GenerateMocksForTypesAttribute.NamespaceMatches(attributeClass.ContainingNamespace))
                        return;
                    ProcessGenerateMocksForTypesAttribute(attribute, context);
                    break;
            }
        }

        [PerformanceSensitive("")]
        private void ProcessGenerateMocksForAssemblyAttribute(AttributeData attribute, GeneratorExecutionContext context) {
            // intermediate code state? just in case
            if (attribute.ConstructorArguments.ElementAtOrDefault(0).Value is not INamedTypeSymbol anyTypeInAssembly)
                return;

            string? excludePattern = null;
            foreach (var named in attribute.NamedArguments) {
                if (named.Key == KnownTypes.GenerateMocksForAssemblyOfAttribute.NamedParameters.ExcludeRegex)
                    excludePattern = named.Value.Value as string;
            }

            GenerateMocksForAssembly(anyTypeInAssembly.ContainingAssembly, excludePattern, attribute.ApplicationSyntaxReference, context);
        }

        [PerformanceSensitive("")]
        private void ProcessGenerateMocksForTypesAttribute(AttributeData attribute, GeneratorExecutionContext context) {
            if (attribute.ConstructorArguments.ElementAtOrDefault(0) is not { Kind: TypedConstantKind.Array, Values: var typeConstants })
                return;

            foreach (var typeConstant in typeConstants) {
                if (typeConstant is not { Value: INamedTypeSymbol type })
                    continue;
                if (type.IsUnboundGenericType)
                    type = type.ConstructedFrom;

                GenerateMockForType(_modelFactory.GetMockTarget(type), assemblyCacheBuilder: null, context);
            }
        }

        [PerformanceSensitive("")]
        private void GenerateMocksForAssembly(IAssemblySymbol assembly, string? excludePattern, SyntaxReference? errorSyntaxReference, in GeneratorExecutionContext context) {
            if (_mockedAssemblyCache.TryGetValue((assembly, excludePattern), out var sources)) {
                GeneratorLog.Log("Using cached mocks for assembly " + assembly.Name);
                foreach (var (name, source) in sources) {
                    context.AddSource(name, source);
                }
                return;
            }

            GeneratorLog.Log("Generating mocks for assembly " + assembly.Name);

            Regex? excludeRegex;
            try {
                #pragma warning disable HAA0502 // Explicit new reference type allocation -- No alternative at the moment (cache/pool later?)
                excludeRegex = excludePattern != null ? new Regex(excludePattern) : null;
                #pragma warning restore HAA0502
            }
            catch (ArgumentException ex) {
                #pragma warning disable HAA0101 // Array allocation for params parameter -- Exceptional case: OK to allocate
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.RegexPatternFailedToParse,
                    errorSyntaxReference?.SyntaxTree.GetLocation(errorSyntaxReference.Span),
                    excludePattern, ex.Message
                ));
                #pragma warning restore HAA0101 // Array allocation for params parameter
                return;
            }

            var assemblyCacheBuilder = ImmutableArray.CreateBuilder<(string, SourceText)>();
            GenerateMocksForNamespace(assembly.GlobalNamespace, excludeRegex, assemblyCacheBuilder, context);
            _mockedAssemblyCache.TryAdd((assembly, excludePattern), assemblyCacheBuilder.ToImmutable());
        }

        [PerformanceSensitive("")]
        private void GenerateMocksForNamespace(
            INamespaceSymbol parent,
            Regex? excludeRegex,
            ImmutableArray<(string, SourceText)>.Builder assemblyCacheBuilder,
            in GeneratorExecutionContext context
        ) {
            #pragma warning disable HAA0401 // Possible allocation of reference type enumerator -- TODO to revisit later
            foreach (var member in parent.GetMembers()) {
            #pragma warning restore HAA0401
                switch (member) {
                    case INamedTypeSymbol type:
                        var target = _modelFactory.GetMockTarget(type);
                        if (!ShouldIncludeInMocksForAssembly(target, excludeRegex, context))
                            continue;
                        GenerateMockForType(target, assemblyCacheBuilder, context);
                        break;

                    case INamespaceSymbol nested:
                        GenerateMocksForNamespace(nested, excludeRegex, assemblyCacheBuilder, context);
                        break;
                }
            }
        }

        [PerformanceSensitive("")]
        private bool ShouldIncludeInMocksForAssembly(MockTarget target, Regex? excludeRegex, in GeneratorExecutionContext context) {
            var type = target.Type;
            if (type.TypeKind != TypeKind.Interface)
                return false;

            if (type.DeclaredAccessibility != Accessibility.Public) {
                var isVisibleInternal = type.DeclaredAccessibility == Accessibility.Internal
                                     && type.ContainingAssembly.GivesAccessTo(context.Compilation.Assembly);
                if (!isVisibleInternal)
                    return false;
            }

            if (excludeRegex != null && excludeRegex.IsMatch(target.FullTypeName))
                return false;

            return true;
        }

        [PerformanceSensitive("")]
        private void GenerateMockForType(
            MockTarget target,
            ImmutableArray<(string, SourceText)>.Builder? assemblyCacheBuilder,
            in GeneratorExecutionContext context
        ) {
            if (_mockedTypeCache.TryGetValue(target.Type, out var cached)) {
                GeneratorLog.Log("Using cached mock for type " + target.FullTypeName);
                context.AddSource(cached.name, cached.source);
                assemblyCacheBuilder?.Add(cached);
                return;
            }

            GeneratorLog.Log("Generating mock for type " + target.FullTypeName);
            string mockContent;
            try {
                mockContent = _classGenerator.Generate(target);
            }
            catch (Exception ex) {
                #pragma warning disable HAA0101 // Array allocation for params parameter
                // Exceptional case: OK to allocate
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.SingleMockFailedToGenerate, location: null, target.FullTypeName, ex.ToString()
                ));
                #pragma warning restore HAA0101 // Array allocation for params parameter
                return;
            }

            var mockFileName = Regex.Replace(target.FullTypeName, @"[^\w\d]", "_") + ".cs";
            var mockSource = SourceText.From(mockContent, Encoding.UTF8);
            context.AddSource(mockFileName, mockSource);
            _mockedTypeCache.TryAdd(target.Type, (mockFileName, mockSource));
            assemblyCacheBuilder?.Add((mockFileName, mockSource));
        }

        public void Dispose() {
            _mockedAssemblyCache.Dispose();
            _mockedTypeCache.Dispose();
        }
    }
}