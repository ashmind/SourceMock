using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTarget {
        [PerformanceSensitive("")]
        public MockTarget(INamedTypeSymbol targetType, string targetTypeQualifiedName)
        {
            Type = targetType;
            FullTypeName = targetTypeQualifiedName;
        }

        public INamedTypeSymbol Type { get; }
        public string FullTypeName { get; }
    }
}
