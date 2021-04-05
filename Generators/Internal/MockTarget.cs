using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal readonly struct MockTarget {
        public MockTarget(INamedTypeSymbol targetType, string targetTypeQualifiedName)
        {
            Type = targetType;
            FullTypeName = targetTypeQualifiedName;
        }

        public INamedTypeSymbol Type { get; }
        public string FullTypeName { get; }
    }
}
