using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
            foreach (var targetType in targetTypes) {
                var targetTypeQualifiedName = targetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var target = new MockTarget(targetType, targetTypeQualifiedName);

                var mockContent = _classGenerator.Generate(target);
                var mockFileName = Regex.Replace(target.FullTypeName, @"[^\w\d]", "_");
                context.AddSource(mockFileName + ".cs", SourceText.From(mockContent, Encoding.UTF8));
            }
        }

        private class TypesToMockCollectingReceiver : ISyntaxContextReceiver {
            public ISet<ITypeSymbol> TypesToMock { get; } = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context) {
                if (context.Node is not AttributeListSyntax { Target: {} target, Attributes: var attributes })
                    return;

                if (!target.Identifier.IsKind(SyntaxKind.AssemblyKeyword))
                    return;

                foreach (var attribute in attributes) {
                    ProcessAssemblyAttribute(attribute, context);
                }
            }

            private void ProcessAssemblyAttribute(AttributeSyntax attribute, GeneratorSyntaxContext context) {
                if (attribute.Name is not IdentifierNameSyntax name)
                    return;

                if (name.Identifier.ValueText is not KnownTypes.GenerateMocksForAssemblyOfAttribute.Name
                                             and not KnownTypes.GenerateMocksForAssemblyOfAttribute.NameWithoutAttribute)
                    return;

                // Double check the resolved type
                var attributeType = context.SemanticModel.GetTypeInfo(attribute).Type;
                if (attributeType is not { Name: KnownTypes.GenerateMocksForAssemblyOfAttribute.Name })
                    return;

                if (!KnownTypes.GenerateMocksForAssemblyOfAttribute.NamespaceMatches(attributeType.ContainingNamespace))
                    return;

                ProcessGenerateMocksForAssemblyOfAttribute(attribute, context);
            }

            private void ProcessGenerateMocksForAssemblyOfAttribute(AttributeSyntax attribute, GeneratorSyntaxContext context) {
                var attributeArgument = attribute.ArgumentList?.Arguments.ElementAtOrDefault(0);
                if (attributeArgument is not { Expression: TypeOfExpressionSyntax @typeof })
                    return;

                var anyTypeInAssembly = context.SemanticModel.GetTypeInfo(@typeof.Type).Type;
                if (anyTypeInAssembly == null)
                    return;

                CollectTypesRecursively(anyTypeInAssembly.ContainingAssembly.GlobalNamespace);                
            }

            private void CollectTypesRecursively(INamespaceSymbol parent) {
                foreach (var member in parent.GetMembers()) {
                    switch (member) {
                        case ITypeSymbol type:
                            if (type.TypeKind != TypeKind.Interface)
                                continue;
                            if (type.GetAttributes().Any(IsGeneratedMockAttribute))
                                continue;
                            TypesToMock.Add(type);
                            break;

                        case INamespaceSymbol nested:
                            CollectTypesRecursively(nested);
                            break;
                    }                    
                }
            }

            private bool IsGeneratedMockAttribute(AttributeData attribute) {
                if (attribute.AttributeClass is not {} attributeClass)
                    return false;

                if (attributeClass.Name != KnownTypes.GeneratedMockAttribute.Name)
                    return false;

                return KnownTypes.GeneratedMockAttribute.NamespaceMatches(attributeClass.ContainingNamespace);
            }
        }
    }
}