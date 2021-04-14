using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTarget {
        [PerformanceSensitive("")]
        public MockTarget(
            INamedTypeSymbol targetType,
            string targetTypeQualifiedName,
            ImmutableArray<ISymbol>? potentiallyLoadedMembers
        )
        {
            Type = targetType;
            FullTypeName = targetTypeQualifiedName;
            PotentiallyLoadedMembers = potentiallyLoadedMembers;
        }

        public INamedTypeSymbol Type { get; }
        public string FullTypeName { get; }
        public ImmutableArray<ISymbol>? PotentiallyLoadedMembers { get; }
    }
}
