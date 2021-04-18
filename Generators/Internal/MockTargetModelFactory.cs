using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using SourceMock.Generators.Internal.Models;

namespace SourceMock.Generators.Internal {
    internal class MockTargetModelFactory {
        private static readonly SymbolDisplayFormat TargetTypeDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

        public MockTarget GetMockTarget(INamedTypeSymbol type) {
            var fullName = GetFullTypeName(type, NullableAnnotation.None);
            return new MockTarget(type, fullName);
        }

        [PerformanceSensitive("")]
        public IEnumerable<MockTargetMember> GetMockTargetMembers(MockTarget target) {
            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later (or removed if we handle them differently)
            var seen = new HashSet<string>();
            #pragma warning restore HAA0502

            var memberId = 1;
            foreach (var member in target.Type.GetMembers()) {
                seen.Add(member.Name);

                if (GetMockTargetMember(member, memberId) is not {} discovered)
                    continue;

                yield return discovered;
                memberId += 1;
            }

            foreach (var @interface in target.Type.AllInterfaces) {
                foreach (var member in @interface.GetMembers()) {
                    if (!seen.Add(member.Name))
                        throw Exceptions.NotSupported($"Type member {@interface.Name}.{member.Name} is hidden or overloaded by another type member. This is not yet supported.");
                    if (GetMockTargetMember(member, memberId) is not { } discovered)
                        continue;

                    yield return discovered;
                    memberId += 1;
                }
            }
        }

        [PerformanceSensitive("")]
        private MockTargetMember? GetMockTargetMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol method => GetMockTargetMethod(method, uniqueMemberId),

            IPropertySymbol property => new(
                property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                ImmutableArray<ITypeParameterSymbol>.Empty,
                property.SetMethod == null
                    ? ImmutableArray<MockTargetParameter>.Empty
                    : ConvertParametersFromSymbols(property.SetMethod.Parameters),
                GetHandlerFieldName(property.Name, uniqueMemberId),
                methodRunDelegateTypeFullName: null
            ),

            _ => throw Exceptions.MemberNotSupported(member)
        };

        private MockTargetMember? GetMockTargetMethod(IMethodSymbol method, int uniqueMemberId) {
            if (method.MethodKind != MethodKind.Ordinary)
                return null;

            if (method.ContainingType.TypeKind != TypeKind.Interface && !method.IsAbstract && !method.IsVirtual)
                return null;

            var parameters = ConvertParametersFromSymbols(method.Parameters);

            return new(
                method, method.Name, method.ReturnType,
                GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation),
                ValidateGenericParameters(method.TypeParameters),
                parameters,
                GetHandlerFieldName(method.Name, uniqueMemberId),
                GetRunDelegateFullTypeName(parameters, method.ReturnType)
            );
        }

        [PerformanceSensitive("")]
        private string GetFullTypeName(ITypeSymbol type, NullableAnnotation nullableAnnotation) {
            var nullableFlowState = nullableAnnotation switch {
                NullableAnnotation.Annotated => NullableFlowState.MaybeNull,
                NullableAnnotation.NotAnnotated => NullableFlowState.NotNull,
                _ => NullableFlowState.None
            };
            return type.ToDisplayString(nullableFlowState, TargetTypeDisplayFormat);
        }

        [PerformanceSensitive("")]
        private ImmutableArray<MockTargetParameter> ConvertParametersFromSymbols(ImmutableArray<IParameterSymbol> parameters) {
            if (parameters.Length == 0)
                return ImmutableArray<MockTargetParameter>.Empty;

            var builder = ImmutableArray.CreateBuilder<MockTargetParameter>(parameters.Length);
            for (var i = 0; i < parameters.Length; i++) {
                var parameter = parameters[i];
                builder.Add(new MockTargetParameter(
                    parameter.Name, GetFullTypeName(parameter.Type, parameter.NullableAnnotation), parameter.RefKind, i
                ));
            }
            return builder.MoveToImmutable();
        }

        [PerformanceSensitive("")]
        private ImmutableArray<ITypeParameterSymbol> ValidateGenericParameters(ImmutableArray<ITypeParameterSymbol> typeParameters) {
            if (typeParameters.IsEmpty)
                return typeParameters;

            foreach (var typeParameter in typeParameters) {
                EnsureNoUnsupportedConstraints(typeParameter);
            }

            return typeParameters;
        }

        [PerformanceSensitive("")]
        public void EnsureNoUnsupportedConstraints(ITypeParameterSymbol parameter) {
            var hasConstraints = parameter.HasConstructorConstraint
                              || parameter.HasReferenceTypeConstraint
                              || parameter.HasValueTypeConstraint
                              || parameter.HasConstructorConstraint
                              || parameter.HasNotNullConstraint
                              || parameter.HasUnmanagedTypeConstraint
                              || parameter.ConstraintTypes.Length > 0;

            if (hasConstraints)
                throw Exceptions.NotSupported("Generic constraints are not yet supported.");
        }

        [PerformanceSensitive("")]
        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            #pragma warning disable HAA0601 // Boxing - unavoidable for now, will revisit later
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
            #pragma warning restore HAA0601
        }

        [PerformanceSensitive("")]
        private string GetRunDelegateFullTypeName(ImmutableArray<MockTargetParameter> parameters, ITypeSymbol returnType) {
            if (returnType.SpecialType == SpecialType.System_Void) {
                if (!parameters.IsEmpty)
                    return $"{KnownTypes.Action.FullName}<{string.Join(",", parameters.Select(x => x.TypeFullName))}>";

                return KnownTypes.Action.FullName;
            }

            if (!parameters.IsEmpty)
                return $"{KnownTypes.Func.FullName}<{string.Join(",", parameters.Select(x => x.TypeFullName))}, {returnType}>";

            return $"{KnownTypes.Func.FullName}<{returnType}>";
        }
    }
}
