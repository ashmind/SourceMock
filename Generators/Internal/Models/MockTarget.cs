using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTarget {
        [PerformanceSensitive("")]
        public MockTarget(
            INamedTypeSymbol targetType,
            string targetTypeQualifiedName,
            string? genericParameterConstraints
        )
        {
            Type = targetType;
            FullTypeName = targetTypeQualifiedName;
            GenericParameterConstraints = genericParameterConstraints;
        }

        public INamedTypeSymbol Type { get; }
        public string FullTypeName { get; }
        public string? GenericParameterConstraints { get; }
    }
}
