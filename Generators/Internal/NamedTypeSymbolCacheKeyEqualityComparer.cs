using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal class NamedTypeSymbolCacheKeyEqualityComparer : IEqualityComparer<INamedTypeSymbol> {
        public static NamedTypeSymbolCacheKeyEqualityComparer Default { get; } = new NamedTypeSymbolCacheKeyEqualityComparer();

        public bool Equals(INamedTypeSymbol x, INamedTypeSymbol y) {
            if (ReferenceEquals(x, y))
                return true;
            if (SymbolEqualityComparer.Default.Equals(x, y))
                return true;
            if (x.DeclaringSyntaxReferences.Length == 0)
                return false; // non-syntax types will only be cached on assembly level

            return x.DeclaringSyntaxReferences.SequenceEqual(
                y.DeclaringSyntaxReferences, static (x, y) => x == y
            );
        }

        public int GetHashCode(INamedTypeSymbol obj) {
            if (obj.DeclaringSyntaxReferences.Length == 0)
                return SymbolEqualityComparer.Default.GetHashCode(obj);

            var hashCode = 0;
            foreach (var reference in obj.DeclaringSyntaxReferences) {
                hashCode ^= reference.GetHashCode();
            }
            return hashCode;
        }
    }
}