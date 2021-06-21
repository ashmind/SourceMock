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
            return new MockTarget(type, fullName, GetGenericConstraintsAsCode(Indents.Member, type.TypeParameters));
        }

        [PerformanceSensitive("")]
        public IEnumerable<MockTargetMember> GetMockTargetMembers(MockTarget target, string customDelegatesClassName) {
            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later (or removed if we handle them differently)
            var lastOverloadIds = new Dictionary<string, int>();
            #pragma warning restore HAA0502

            foreach (var member in target.Type.GetMembers()) {
                if (!lastOverloadIds.TryGetValue(member.Name, out var lastOverloadId))
                    lastOverloadId = 0;

                var overloadId = lastOverloadId + 1;
                if (GetMockTargetMember(member, overloadId, customDelegatesClassName) is not {} discovered)
                    continue;

                lastOverloadIds[member.Name] = overloadId;
                yield return discovered;
            }

            if (target.Type.TypeKind != TypeKind.Interface)
                yield break;

            foreach (var @interface in target.Type.AllInterfaces) {
                foreach (var member in @interface.GetMembers()) {
                    if (lastOverloadIds.ContainsKey(member.Name))
                        throw Exceptions.NotSupported($"Type member {@interface.Name}.{member.Name} is hidden or overloaded by another type member. This is not yet supported.");

                    if (GetMockTargetMember(member, 1, customDelegatesClassName) is not {} discovered)
                        continue;

                    lastOverloadIds[member.Name] = 1;
                    yield return discovered;
                }
            }
        }

        [PerformanceSensitive("")]
        private MockTargetMember? GetMockTargetMember(ISymbol member, int overloadId, string customDelegatesClassName) => member switch {
            IMethodSymbol method => GetMockTargetMethod(method, overloadId, customDelegatesClassName),
            IPropertySymbol property => GetMockTargetProperty(property, overloadId),
            _ => throw Exceptions.MemberNotSupported(member)
        };

        [PerformanceSensitive("")]
        private MockTargetMember? GetMockTargetMethod(IMethodSymbol method, int overloadId, string customDelegatesClassName) {
            if (method.MethodKind != MethodKind.Ordinary)
                return null;

            if (!IsVirtual(method))
                return null;

            var parameters = ConvertParametersFromSymbols(method.Parameters);
            var returnTypeFullName = GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation);

            return new(
                method, method.Name, method.ReturnType,
                returnTypeFullName,
                method.TypeParameters,
                GetGenericConstraintsAsCode(Indents.MemberBody, method.TypeParameters),
                parameters,
                GetHandlerFieldName(method.Name, overloadId),
                GetRunDelegateType(method, parameters, returnTypeFullName, overloadId, customDelegatesClassName)
            );
        }

        [PerformanceSensitive("")]
        private MockTargetMember? GetMockTargetProperty(IPropertySymbol property, int overloadId) {
            if (!IsVirtual(property))
                return null;

            return new(
                property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                ImmutableArray<ITypeParameterSymbol>.Empty,
                genericParameterConstraints: null,
                property.SetMethod == null
                    ? ImmutableArray<MockTargetParameter>.Empty
                    : ConvertParametersFromSymbols(property.SetMethod.Parameters),
                GetHandlerFieldName(property.Name, overloadId),
                methodRunDelegateType: null
            );
        }

        [PerformanceSensitive("")]
        private bool IsVirtual(ISymbol symbol) {
            return symbol.ContainingType.TypeKind == TypeKind.Interface
                || symbol.IsAbstract
                || symbol.IsVirtual;
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
        private string GetHandlerFieldName(string memberName, int overloadId) {
            #pragma warning disable HAA0601 // Boxing - unavoidable for now, will revisit later
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{(overloadId > 1 ? overloadId.ToString() : "")}Handler";
            #pragma warning restore HAA0601
        }

        [PerformanceSensitive("")]
        private MockTargetMethodRunDelegateType GetRunDelegateType(
            IMethodSymbol method,
            ImmutableArray<MockTargetParameter> parameters,
            string returnTypeFullName,
            int overloadId,
            string customDelegatesClassName
        ) {
            var needsCustomDelegate = method.IsGenericMethod
                                   || parameters.Length > 16
                                   || parameters.Any(static p => p.RefKind is not RefKind.None or RefKind.In);
            if (needsCustomDelegate) {
                var suffix = method.ReturnsVoid ? "Action" : "Func";
                var nameWithGenericParameters = method.Name + (overloadId > 1 ? overloadId.ToString() : "") + suffix;
                if (method.IsGenericMethod)
                    nameWithGenericParameters += "<" + string.Join(", ", method.TypeParameters.Select(static t => t.Name)) + ">";

                return MockTargetMethodRunDelegateType.Custom(nameWithGenericParameters, customDelegatesClassName + "." + nameWithGenericParameters);
            }

            if (method.ReturnsVoid) {
                if (!parameters.IsEmpty)
                    return new($"{KnownTypes.Action.FullName}<{string.Join(",", parameters.Select(static x => x.FullTypeName))}>");

                return new(KnownTypes.Action.FullName);
            }

            if (!parameters.IsEmpty)
                return new($"{KnownTypes.Func.FullName}<{string.Join(",", parameters.Select(static x => x.FullTypeName))}, {returnTypeFullName}>");

            return new($"{KnownTypes.Func.FullName}<{returnTypeFullName}>");
        }


        [PerformanceSensitive("")]
        private string? GetGenericConstraintsAsCode(string indent, ImmutableArray<ITypeParameterSymbol> parameters) {
            var writer = (CodeWriter?)null;
            foreach (var parameter in parameters) {
                writer = WriteGenericConstraints(writer, indent, parameter);
            }
            return writer?.ToString();
        }

        [PerformanceSensitive("")]
        private CodeWriter? WriteGenericConstraints(CodeWriter? writer, string indent, ITypeParameterSymbol parameter) {
            var parameterStarted = false;
            void WriteConstraint(string constraint) {
                if (!parameterStarted) {
                    #pragma warning disable HAA0502 // Explicit new reference type allocation - currently unavoidable
                    writer ??= new CodeWriter();
                    #pragma warning restore HAA0502 // Explicit new reference type allocation
                    writer
                        .WriteLine()
                        .Write(indent, "where ", parameter.Name, ": ");
                    parameterStarted = true;
                }
                else {
                    writer!.Write(", ");
                }
                writer.Write(constraint);
            }

            if (parameter.HasReferenceTypeConstraint) {
                WriteConstraint("class");
                if (parameter.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated)
                    writer!.Write("?");
            }
            // seems like unmanaged adds implict struct constraint
            if (parameter.HasValueTypeConstraint && !parameter.HasUnmanagedTypeConstraint)
                WriteConstraint("struct");
            if (parameter.HasNotNullConstraint)
                WriteConstraint("notnull");
            if (parameter.HasUnmanagedTypeConstraint)
                WriteConstraint("unmanaged");

            for (var i = 0; i < parameter.ConstraintTypes.Length; i++) {
                var fullTypeName = GetFullTypeName(parameter.ConstraintTypes[i], parameter.ConstraintNullableAnnotations[i]);
                WriteConstraint(fullTypeName);
            }

            if (parameter.HasConstructorConstraint)
                WriteConstraint("new()");

            return writer;
        }
    }
}
