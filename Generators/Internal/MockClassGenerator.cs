using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using SourceMock.Generators.Internal.Models;

namespace SourceMock.Generators.Internal {
    internal class MockClassGenerator {
        private static readonly SymbolDisplayFormat TargetTypeDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);
        private static readonly SymbolDisplayFormat TargetTypeNamespaceDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
            SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining
        );

        private readonly MockMemberGenerator _mockMemberGenerator = new();

        [PerformanceSensitive("")]
        public string Generate(MockTarget target) {
            var targetTypeNamespace = target.Type.ContainingNamespace.ToDisplayString(TargetTypeNamespaceDisplayFormat);
            var mockBaseName = GenerateMockBaseName(target.Type.Name);
            var typeParameters = GenerateTypeParametersAsString(target);
            var mockClassName = mockBaseName + "Mock" + typeParameters;
            var setupInterfaceName = "I" + mockBaseName + "Setup" + typeParameters;
            var callsInterfaceName = "I" + mockBaseName + "Calls" + typeParameters;

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var mainWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine("#nullable enable")
                .WriteLine("namespace ", targetTypeNamespace, ".Mocks {");

            mainWriter
                .Write(Indents.Type, "public class ", mockClassName, " : ")
                    .Write(target.FullTypeName, ", ", setupInterfaceName, ", ", callsInterfaceName, ", ")
                    .WriteGeneric(KnownTypes.IMock.FullName, target.FullTypeName)
                    .WriteLine(" {")
                .WriteLine(Indents.Member, "public ", setupInterfaceName, " Setup => this;")
                .WriteLine(Indents.Member, "public ", callsInterfaceName, " Calls => this;");

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var setupInterfaceWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine(Indents.Type, "public interface ", setupInterfaceName, " {");

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var callsInterfaceWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine(Indents.Type, "public interface ", callsInterfaceName, " {");

            var memberId = 1;
            #pragma warning disable HAA0401 // Possible allocation of reference type enumerator - TODO
            foreach (var memberSymbol in GetAllMembers(target)) {
            #pragma warning restore HAA0401
                if (GetMockTargetMember(memberSymbol, memberId) is not {} member)
                    continue;

                mainWriter.WriteLine();
                _mockMemberGenerator.WriteMemberMocks(
                    mainWriter,
                    setupInterfaceWriter,
                    setupInterfaceName,
                    callsInterfaceWriter,
                    callsInterfaceName,
                    member
                ).WriteLine();
                memberId += 1;
            }

            mainWriter.WriteLine(Indents.Type, "}");

            setupInterfaceWriter.Write(Indents.Type, "}");
            mainWriter
                .WriteLine()
                .Append(setupInterfaceWriter)
                .WriteLine();

            callsInterfaceWriter.Write(Indents.Type, "}");
            mainWriter
                .WriteLine()
                .Append(callsInterfaceWriter)
                .WriteLine();

            mainWriter.Write("}");
            return mainWriter.ToString();
        }

        [PerformanceSensitive("")]
        private IEnumerable<ISymbol> GetAllMembers(MockTarget target) {
            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later (or removed if we handle them differently)
            var seen = new HashSet<string>();
            #pragma warning restore HAA0502
            foreach (var member in target.PotentiallyLoadedMembers ?? target.Type.GetMembers()) {
                seen.Add(member.Name);
                yield return member;
            }

            foreach (var @interface in target.Type.AllInterfaces) {
                foreach (var member in @interface.GetMembers()) {
                    if (!seen.Add(member.Name))
                        throw NotSupported($"Type member {@interface.Name}.{member.Name} is hidden or overloaded by another type member. This is not yet supported.");
                    yield return member;
                }
            }
        }

        [PerformanceSensitive("")]
        private string GenerateMockBaseName(string targetName) {
            if (targetName.Length < 3)
                return targetName;

            var canRemoveI = targetName[0] == 'I'
                          && char.IsUpper(targetName[1])
                          && char.IsLower(targetName[2]);

            return canRemoveI ? targetName.Substring(1) : targetName;
        }

        [PerformanceSensitive("")]
        private string GenerateTypeParametersAsString(MockTarget target) {
            if (target.Type.TypeParameters is not { Length: > 0 } parameters)
                return "";

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var writer = new CodeWriter();
            #pragma warning restore HAA0502
            writer.Write("<");
            var index = 0;
            foreach (var parameter in parameters) {
                EnsureNoUnsupportedConstraints(parameter);
                if (index > 0)
                    writer.Write(", ");
                writer.Write(parameter.Name);
                index += 1;
            }
            writer.Write(">");
            return writer.ToString();
        }

        [PerformanceSensitive("")]
        private MockTargetMember? GetMockTargetMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol method => GetTargetMethod(method, uniqueMemberId),

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

            _ => throw MemberNotSupported(member)
        };

        private MockTargetMember? GetTargetMethod(IMethodSymbol method, int uniqueMemberId) {
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
        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            #pragma warning disable HAA0601 // Boxing - unavoidable for now, will revisit later
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
            #pragma warning restore HAA0601
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
        private void EnsureNoUnsupportedConstraints(ITypeParameterSymbol parameter) {
            var hasConstraints = parameter.HasConstructorConstraint
                              || parameter.HasReferenceTypeConstraint
                              || parameter.HasValueTypeConstraint
                              || parameter.HasConstructorConstraint
                              || parameter.HasNotNullConstraint
                              || parameter.HasUnmanagedTypeConstraint
                              || parameter.ConstraintTypes.Length > 0;

            if (hasConstraints) {
                #pragma warning disable HAA0502 // Explicit allocation -- OK in exceptional case
                throw new NotSupportedException("Generic constraints are not yet supported.");
                #pragma warning restore HAA0502
            }
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

        // Having this as a separate method removes need to suppress allocation warnings each time in exceptional situations
        private NotSupportedException NotSupported(string message) => new(message);

        private NotSupportedException MemberNotSupported(ISymbol symbol) => NotSupported(
            $"{symbol.Name} has an unsupported member symbol type ({symbol.GetType()})"
        );
    }
}
