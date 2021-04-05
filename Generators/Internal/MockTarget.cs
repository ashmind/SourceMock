using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal readonly struct MockTarget {
        public MockTarget(ITypeSymbol targetType, string targetTypeQualifiedName)
        {
            Type = targetType;
            FullTypeName = targetTypeQualifiedName;
        }

        public ITypeSymbol Type { get; }
        public string FullTypeName { get; }
    }
}
