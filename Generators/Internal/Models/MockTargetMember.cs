using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTargetMember {
        [PerformanceSensitive("")]
        public MockTargetMember(
            ISymbol symbol,
            string name,
            ITypeSymbol type,
            string typeFullName,
            ImmutableArray<ITypeParameterSymbol> genericParameters,
            ImmutableArray<MockTargetParameter> parameters,
            string handlerFieldName,
            string? methodRunDelegateTypeFullName
        ) {
            Symbol = symbol;
            Name = name;
            Type = type;
            TypeFullName = typeFullName;
            GenericParameters = genericParameters;
            Parameters = parameters;
            HandlerFieldName = handlerFieldName;
            MethodRunDelegateTypeFullName = methodRunDelegateTypeFullName;
        }

        public ISymbol Symbol { get; }
        public string Name { get; }
        public ITypeSymbol Type { get; }
        public string TypeFullName { get; }
        public ImmutableArray<ITypeParameterSymbol> GenericParameters { get; }
        public ImmutableArray<MockTargetParameter> Parameters { get; }
        public string HandlerFieldName { get; }
        public string? MethodRunDelegateTypeFullName { get; }
        [PerformanceSensitive("")]
        public bool IsVoidMethod => Symbol is IMethodSymbol && Type.SpecialType == SpecialType.System_Void;
        [PerformanceSensitive("")]
        public string HandlerGenericParameterFullName => !IsVoidMethod ? TypeFullName : KnownTypes.VoidReturn.FullName;
    }
}
