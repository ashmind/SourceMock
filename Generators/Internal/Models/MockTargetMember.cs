using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTargetMember {
#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public MockTargetMember(
            ISymbol symbol,
            string name,
            ITypeSymbol type,
            string typeFullName,
            ImmutableArray<ITypeParameterSymbol> genericParameters,
            string? genericParameterConstraints,
            ImmutableArray<MockTargetParameter> parameters,
            string handlerFieldName,
            MockTargetMethodRunDelegateType? methodRunDelegateType
        ) {
            Symbol = symbol;
            Name = name;
            Type = type;
            TypeFullName = typeFullName;
            GenericParameters = genericParameters;
            GenericParameterConstraints = genericParameterConstraints;
            Parameters = parameters;
            HandlerFieldName = handlerFieldName;
            MethodRunDelegateType = methodRunDelegateType;
        }

        public ISymbol Symbol { get; }
        public string Name { get; }
        public ITypeSymbol Type { get; }
        public string TypeFullName { get; }
        public ImmutableArray<ITypeParameterSymbol> GenericParameters { get; }
        public string? GenericParameterConstraints { get; }
        public ImmutableArray<MockTargetParameter> Parameters { get; }
        public string HandlerFieldName { get; }
        public MockTargetMethodRunDelegateType? MethodRunDelegateType { get; }

#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public bool IsVoidMethod => Symbol is IMethodSymbol && Type.SpecialType == SpecialType.System_Void;
#pragma warning disable HAA0502
        [PerformanceSensitive("")]
#pragma warning restore HAA0502
        public string HandlerGenericParameterFullName => !IsVoidMethod ? TypeFullName : KnownTypes.VoidReturn.FullName;
    }
}
