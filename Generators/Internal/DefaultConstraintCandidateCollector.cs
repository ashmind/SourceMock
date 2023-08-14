using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal {
    // https://github.com/dotnet/csharplang/discussions/4638#discussioncomment-594388
    public class DefaultConstraintCandidateCollector : SymbolVisitor {
        private NullableAnnotation? _lastAnnotation;
        private readonly HashSet<ITypeParameterSymbol> _results = new(SymbolEqualityComparer.Default);

        public ISet<ITypeParameterSymbol> Collect(ISymbol symbol) {
            _results.Clear();
            Visit(symbol);
            return _results;
        }

        public override void VisitMethod(IMethodSymbol symbol) {
            _lastAnnotation = symbol.ReturnNullableAnnotation;
            Visit(symbol.ReturnType);

            foreach (var parameter in symbol.Parameters) {
                _lastAnnotation = parameter.NullableAnnotation;
                Visit(parameter.Type);
            }
        }

#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public override void VisitArrayType(IArrayTypeSymbol symbol) {
            _lastAnnotation = symbol.ElementNullableAnnotation;
            Visit(symbol.ElementType);
        }

#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public override void VisitNamedType(INamedTypeSymbol symbol) {
            if (!symbol.IsGenericType)
                return;

            var arguments = symbol.TypeArguments;
            for (var i = 0; i < arguments.Length; i++) {
                _lastAnnotation = symbol.TypeArgumentNullableAnnotations[0];
                Visit(arguments[i]);
            }
        }

#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public override void VisitTypeParameter(ITypeParameterSymbol symbol) {
            if (_lastAnnotation != NullableAnnotation.Annotated)
                return;

            if (symbol.HasValueTypeConstraint || symbol.HasReferenceTypeConstraint)
                return; // we'll copy existing constraints anyways

            _results.Add(symbol);
        }
    }
}
