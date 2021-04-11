using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal {
    internal class MockClassGenerator {
        private static readonly SymbolDisplayFormat TargetTypeDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(SymbolDisplayFormat.FullyQualifiedFormat.MiscellaneousOptions | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);
        private static readonly SymbolDisplayFormat TargetTypeNamespaceDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
            SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining
        );

        private readonly DefaultConstraintCandidateCollector _defaultConstraintCollector = new();

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
                if (GetMockedMember(memberSymbol, memberId) is not { } member)
                    continue;

                mainWriter.WriteLine();
                WriteMemberMocks(
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
            foreach (var member in target.Type.GetMembers()) {
                seen.Add(member.Name);
                yield return member;
            }

            foreach (var @interface in target.Type.AllInterfaces) {
                foreach (var member in @interface.GetMembers()) {
                    if (!seen.Add(member.Name))
                        throw NotSupported($"Interface member {@interface.Name}.{member.Name} is hidden or overloaded by another interface member. This is not yet supported.");
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
        private MockedMember? GetMockedMember(ISymbol member, int uniqueMemberId) => member switch {
            IMethodSymbol { MethodKind: MethodKind.Ordinary } method => new(
                method, method.Name, method.ReturnType,
                GetFullTypeName(method.ReturnType, method.ReturnNullableAnnotation),
                ConvertGenericParametersFromSymbols(method.TypeParameters),
                ConvertParametersFromSymbols(method.Parameters),
                GetHandlerFieldName(method.Name, uniqueMemberId)
            ),

            IPropertySymbol property => new(
                property, property.Name, property.Type,
                GetFullTypeName(property.Type, property.NullableAnnotation),
                ImmutableArray<GenericParameter>.Empty,
                property.SetMethod == null
                    ? ImmutableArray<Parameter>.Empty
                    : ConvertParametersFromSymbols(property.SetMethod.Parameters),
                GetHandlerFieldName(property.Name, uniqueMemberId)
            ),

            IMethodSymbol { MethodKind: not MethodKind.Ordinary } => null,

            _ => throw MemberNotSupported(member)
        };

        [PerformanceSensitive("")]
        private string GetHandlerFieldName(string memberName, int uniqueMemberId) {
            #pragma warning disable HAA0601 // Boxing - unavoidable for now, will revisit later
            return $"_{char.ToLowerInvariant(memberName[0])}{memberName.Substring(1)}{uniqueMemberId}Handler";
            #pragma warning restore HAA0601
        }

        [PerformanceSensitive("")]
        private ImmutableArray<Parameter> ConvertParametersFromSymbols(ImmutableArray<IParameterSymbol> parameters) {
            if (parameters.Length == 0)
                return ImmutableArray<Parameter>.Empty;

            var builder = ImmutableArray.CreateBuilder<Parameter>(parameters.Length);
            for (var i = 0; i < parameters.Length; i++) {
                var parameter = parameters[i];
                builder.Add(new Parameter(
                    parameter.Name, GetFullTypeName(parameter.Type, parameter.NullableAnnotation), parameter.RefKind, i
                ));
            }
            return builder.MoveToImmutable();
        }

        [PerformanceSensitive("")]
        private ImmutableArray<GenericParameter> ConvertGenericParametersFromSymbols(ImmutableArray<ITypeParameterSymbol> typeParameters) {
            if (typeParameters.Length == 0)
                return ImmutableArray<GenericParameter>.Empty;

            var builder = ImmutableArray.CreateBuilder<GenericParameter>(typeParameters.Length);
            for (var i = 0; i < typeParameters.Length; i++) {
                var typeParameter = typeParameters[i];
                EnsureNoUnsupportedConstraints(typeParameter);
                builder.Add(new GenericParameter(typeParameter.Name, i));
            }
            return builder.MoveToImmutable();
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
        private CodeWriter WriteMemberMocks(
            CodeWriter mockWriter,
            CodeWriter setupInterfaceWriter,
            string setupInterfaceName,
            CodeWriter callsInterfaceWriter,
            string callsInterfaceName,
            in MockedMember member
        ) {
            WriteHandlerField(mockWriter, member).WriteLine();
            WriteSetupInterfaceMember(setupInterfaceWriter, member).WriteLine();
            WriteSetupMemberImplementation(mockWriter, setupInterfaceName, member).WriteLine();
            WriteMemberImplementation(mockWriter, member).WriteLine();
            WriteCallsInterfaceMember(callsInterfaceWriter, member).WriteLine();
            WriteCallsMemberImplementation(mockWriter, callsInterfaceName, member);
            return mockWriter;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteHandlerField(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member, "private readonly ");
            switch (member.Symbol) {
                case IMethodSymbol:
                    writer.Write(KnownTypes.MockMethodHandler.FullName, " ", member.HandlerFieldName, " = new()");
                    break;

                case IPropertySymbol property:
                    writer
                        .WriteGeneric(KnownTypes.MockPropertyHandler.FullName, member.HandlerGenericParameterFullName)
                        .Write(" ", member.HandlerFieldName)
                        .Write(" = new(", property.SetMethod != null ? "true" : "false", ")");
                    break;

                default:
                    throw MemberNotSupported(member.Symbol);
            }
            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteSetupMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupMemberImplementation(CodeWriter writer, string setupInterfaceName, in MockedMember member) {
            writer.Write(Indents.Member);

            WriteSetupMemberType(writer, member);
            writer.Write(" ", setupInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMemberGenericParametersIfAny(writer, member);
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
                WriteExplicitImplementationDefaultConstraintsIfAny(writer, member);
            }

            writer.Write(" => ");

            writer.Write(member.HandlerFieldName, ".Setup");
            if (member.Symbol is IMethodSymbol) {
                var callbackType = GetCallbackType(member);

                writer.Write("<", callbackType + ", " + member.HandlerGenericParameterFullName, ">(");
                WriteCommonMethodHandlerArguments(writer, member, KnownTypes.IMockArgumentMatcher.FullName);
                writer.Write(")");
            }
            else {
                writer.Write("()");
            }

            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupMemberType(CodeWriter writer, in MockedMember member) {
            var setupTypeFullName = member.Symbol switch {
                IMethodSymbol => KnownTypes.IMockMethodSetup.FullName,
                IPropertySymbol property => property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertySetup.FullName
                    : KnownTypes.IMockPropertySetup.FullName,
                var s => throw MemberNotSupported(s)
            };

            var callbackType = GetCallbackType(member);

            if (member.Symbol is IPropertySymbol)
                return writer.WriteGeneric(setupTypeFullName, member.TypeFullName);

            if (member.IsVoidMethod)
                return writer.WriteGeneric(setupTypeFullName, callbackType);

            return writer.WriteGeneric(setupTypeFullName, callbackType, member.TypeFullName);
        }

        private string GetCallbackType(in MockedMember member) {
            if (member.IsVoidMethod) {
                if (member.Parameters.Length > 0) {
                    return $"System.Action<{string.Join(",", member.Parameters.Select(x => x.TypeFullName))}>";
                }

                return "System.Action";
            }

            var returnType = member.Symbol switch {
                IMethodSymbol method => method.ReturnType,
                IPropertySymbol property => property.Type,
                _ => throw MemberNotSupported(member.Symbol)
            };

            if (member.Parameters.Length > 0)
                return $"System.Func<{string.Join(",", member.Parameters.Select(x => x.TypeFullName))},{returnType}>";

            return $"System.Func<{returnType}>";
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMemberImplementation(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member, "public ", member.TypeFullName, " ", member.Name);

            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMemberGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    var hasOutParameters = false;
                    foreach (var parameter in member.Parameters) {
                        if (parameter.Index > 0)
                            writer.Write(", ");
                        if (GetRefModifier(parameter.RefKind) is { } modifier)
                            writer.Write(modifier, " ");
                        writer.Write(parameter.TypeFullName, " ", parameter.Name);
                        hasOutParameters = hasOutParameters || (parameter.RefKind == RefKind.Out);
                    }
                    writer.Write(") ");
                    WriteMethodImplementationBody(writer, hasOutParameters, member);
                    break;

                case IPropertySymbol property:
                    if (property.SetMethod != null) {
                        writer.WriteLine(" {");
                        writer.Write(Indents.MemberBody, "get => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member, ImmutableArray<Parameter>.Empty);
                        writer.WriteLine(";");
                        writer.Write(Indents.MemberBody, "set => ", member.HandlerFieldName, ".SetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member);
                        writer.WriteLine(";");
                        writer.Write(Indents.Member, "}");
                    }
                    else {
                        writer.Write(" => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(writer, member);
                        writer.Write(";");
                    }
                    break;

                default:
                    throw MemberNotSupported(member.Symbol);
            }

            return writer;
        }

        [PerformanceSensitive("")]
        private string? GetRefModifier(RefKind refKind) => refKind switch {
            RefKind.None => null,
            RefKind.Ref => "ref",
            RefKind.In => "in",
            RefKind.Out => "out",
            #pragma warning disable HAA0601 // Boxing -- OK in exceptional case
            _ => throw NotSupported($"Unsupported parameter ref kind: {refKind}")
            #pragma warning restore HAA0601
        };

        [PerformanceSensitive("")]
        private CodeWriter WriteMethodImplementationBody(CodeWriter writer, bool hasOutParameters, in MockedMember member) {
            if (hasOutParameters) {
                writer.WriteLine("{");
                foreach (var parameter in member.Parameters) {
                    if (parameter.RefKind != RefKind.Out)
                        continue;
                    writer.WriteLine(Indents.MemberBody, parameter.Name, " = default;");
                }
                writer.Write(Indents.MemberBody, "return ", member.HandlerFieldName);
                WriteMemberImplementationHandlerCall(writer, member, member.Parameters);
                writer.WriteLine(";");
                return writer.Write(Indents.Member, "}");
            }

            writer.Write("=> ", member.HandlerFieldName);
            WriteMemberImplementationHandlerCall(writer, member, member.Parameters);
            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMemberImplementationHandlerCall(CodeWriter writer, in MockedMember member, ImmutableArray<Parameter>? parametersOverride = null) {
            var callbackType = GetCallbackType(member);
            writer.Write(".Call<", callbackType + ", " + member.HandlerGenericParameterFullName, ">(");
            WriteCommonMethodHandlerArguments(writer, member, "object?", parametersOverride);
            return writer.Write(")");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCallsInterfaceMember(CodeWriter writer, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ");
            return WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCallsMemberImplementation(CodeWriter writer, string callsInterfaceName, in MockedMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ", callsInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMemberGenericParametersIfAny(writer, member);
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
                WriteExplicitImplementationDefaultConstraintsIfAny(writer, member);
            }
            writer.Write(" => ", member.HandlerFieldName, ".Calls(");
            if (member.Symbol is IMethodSymbol) {
                WriteCommonMethodHandlerArguments(writer, member, KnownTypes.IMockArgumentMatcher.FullName).Write(", ");
                var parameters = member.Parameters;
                if (!parameters.IsEmpty) {
                    writer.Write("args => (");
                    foreach (var parameter in parameters) {
                        if (parameter.Index > 0)
                            writer.Write(", ");
                        writer.Write("(", parameter.TypeFullName, ")args[", parameter.Index.ToString(), "]");
                        if (!parameter.TypeFullName.EndsWith("?"))
                            writer.Write("!");
                    }
                    writer.Write(")");
                }
                else {
                    writer.Write("_ => ", KnownTypes.NoArguments.FullName, ".Value");
                }
            }
            return writer.Write(");");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCallsMemberType(CodeWriter writer, in MockedMember member) {
            if (member.Symbol is IPropertySymbol property) {
                var callsTypeFullName = property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertyCalls.FullName
                    : KnownTypes.IMockPropertyCalls.FullName;
                return writer.WriteGeneric(callsTypeFullName, member.TypeFullName);
            }

            writer.Write(KnownTypes.IReadOnlyList.FullName, "<");

            var parameters = member.Parameters;
            if (parameters.Length > 1) {
                writer.Write("(");
                foreach (var parameter in parameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.TypeFullName, " ", parameter.Name);
                }
                writer.Write(")");
            }
            else if (parameters.Length == 1) {
                writer.Write(parameters[0].TypeFullName);
            }
            else {
                writer.Write(KnownTypes.NoArguments.FullName);
            }

            return writer.Write(">");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupOrCallsInterfaceMemberNameAndParameters(CodeWriter writer, in MockedMember member) {
            writer.Write(member.Name);
            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMemberGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: true);
                    writer.Write(");");
                    break;
                case IPropertySymbol:
                    writer.Write(" { get; }");
                    break;
                default:
                    throw MemberNotSupported(member.Symbol);
            }
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupOrCallsMemberParameters(CodeWriter writer, in MockedMember member, bool appendDefaultValue) {
            foreach (var parameter in member.Parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgumentMatcher.FullName, parameter.TypeFullName)
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
            }
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCommonMethodHandlerArguments(
            CodeWriter writer,
            in MockedMember member,
            string argumentTypeFullName,
            ImmutableArray<Parameter>? parametersOverride = null
        ) {
            var parameters = parametersOverride ?? member.Parameters;

            if (!member.GenericParameters.IsEmpty) {
                writer.Write("new[] { ");
                foreach (var parameter in member.GenericParameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write("typeof(", parameter.Name, ")");
                }
                writer.Write(" }");
            }
            else {
                writer.Write("null");
            }
            writer.Write(", ");

            if (!parameters.IsEmpty) {
                writer.Write("new ", argumentTypeFullName, "[] { ");
                foreach (var parameter in parameters) {
                    if (parameter.Index > 0)
                        writer.Write(", ");
                    writer.Write(parameter.Name);
                }
                writer.Write(" }");
            }
            else {
                writer.Write("null");
            }
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMemberGenericParametersIfAny(CodeWriter writer, in MockedMember member) {
            if (member.GenericParameters.Length == 0)
                return writer;
            writer.Write("<");
            foreach (var parameter in member.GenericParameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer.Write(parameter.Name);
            }
            return writer.Write(">");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteExplicitImplementationDefaultConstraintsIfAny(CodeWriter writer, in MockedMember member) {
            if (member.GenericParameters.Length == 0)
                return writer;

            var parametersNeedingDefault = _defaultConstraintCollector.Collect(member.Symbol);
            if (parametersNeedingDefault.Count == 0)
                return writer;

            #pragma warning disable HAA0401 // Possible allocation of reference type enumerator - TODO
            foreach (var parameter in parametersNeedingDefault) {
            #pragma warning restore HAA0401
                writer.Write(" where ", parameter.Name, ": default");
            }
            return writer;
        }

        // Having this as a separate method removes need to suppress allocation warnings each time in exceptional situations
        private NotSupportedException NotSupported(string message) => new(message);

        private NotSupportedException MemberNotSupported(ISymbol symbol) => NotSupported(
            $"{symbol.Name} has an unsupported member symbol type ({symbol.GetType()})"
        );

        private readonly struct MockedMember {
            [PerformanceSensitive("")]
            public MockedMember(
                ISymbol symbol,
                string name,
                ITypeSymbol type,
                string typeFullName,
                ImmutableArray<GenericParameter> genericParameters,
                ImmutableArray<Parameter> parameters,
                string handlerFieldName
            ) {
                Symbol = symbol;
                Name = name;
                Type = type;
                TypeFullName = typeFullName;
                GenericParameters = genericParameters;
                Parameters = parameters;
                HandlerFieldName = handlerFieldName;
            }

            public ISymbol Symbol { get; }
            public string Name { get; }
            public ITypeSymbol Type { get; }
            public string TypeFullName { get; }
            public ImmutableArray<GenericParameter> GenericParameters { get; }
            public ImmutableArray<Parameter> Parameters { get; }
            public string HandlerFieldName { get; }
            [PerformanceSensitive("")]
            public bool IsVoidMethod => Symbol is IMethodSymbol && Type.SpecialType == SpecialType.System_Void;
            [PerformanceSensitive("")]
            public string HandlerGenericParameterFullName => !IsVoidMethod ? TypeFullName : KnownTypes.VoidReturn.FullName;
        }

        private readonly struct GenericParameter {
            public GenericParameter(string name, int index) {
                Name = name;
                Index = index;
            }

            public string Name { get; }
            public int Index { get; }
        }

        private readonly struct Parameter {
            public Parameter(string name, string typeFullName, RefKind refKind, int index) {
                Name = name;
                TypeFullName = typeFullName;
                RefKind = refKind;
                Index = index;
            }

            public string Name { get; }
            public string TypeFullName { get; }
            public RefKind RefKind { get; }
            public int Index { get; }
        }
    }
}
