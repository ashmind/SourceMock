using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using SourceMock.Generators.Internal.Models;

namespace SourceMock.Generators.Internal {
    internal class MockMemberGenerator {
        private readonly DefaultConstraintCandidateCollector _defaultConstraintCollector = new();

        [PerformanceSensitive("")]
        public CodeWriter WriteMemberMocks(
            CodeWriter mockWriter,
            CodeWriter customDelegatesClassWriter,
            CodeWriter setupInterfaceWriter,
            string setupInterfaceName,
            CodeWriter callsInterfaceWriter,
            string callsInterfaceName,
            in MockTargetMember member
        ) {
            WriteHandlerField(mockWriter, member).WriteLine();
            if (member.MethodRunDelegateType is { IsCustom: true } runDelegate)
                WriteCustomRunDelegate(customDelegatesClassWriter, member, runDelegate).WriteLine();
            WriteSetupInterfaceMember(setupInterfaceWriter, member).WriteLine();
            WriteSetupMemberImplementation(mockWriter, setupInterfaceName, member).WriteLine();
            WriteMemberImplementation(mockWriter, member).WriteLine();
            WriteCallsInterfaceMember(callsInterfaceWriter, member).WriteLine();
            WriteCallsMemberImplementation(mockWriter, callsInterfaceName, member);
            return mockWriter;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteHandlerField(CodeWriter writer, in MockTargetMember member) {
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
                    throw Exceptions.MemberNotSupported(member.Symbol);
            }
            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCustomRunDelegate(CodeWriter writer, in MockTargetMember member, MockTargetMethodRunDelegateType runDelegate) {
            writer.Write(Indents.Member, "public delegate ", member.TypeFullName, " ", runDelegate.CustomNameWithGenericParameters!, "(");
            WriteMethodImplementationParametersIfAny(writer, member, out _);
            return writer.Write(");");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupInterfaceMember(CodeWriter writer, in MockTargetMember member) {
            writer.Write(Indents.Member);
            WriteSetupMemberType(writer, member);
            writer.Write(" ");
            WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupMemberImplementation(CodeWriter writer, string setupInterfaceName, in MockTargetMember member) {
            writer.Write(Indents.Member);

            WriteSetupMemberType(writer, member);
            writer.Write(" ", setupInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMethodGenericParametersIfAny(writer, member);
                writer.Write("(");
                WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: false);
                writer.Write(")");
                WriteExplicitImplementationDefaultConstraintsIfAny(writer, member);
            }

            writer.Write(" => ");

            writer.Write(member.HandlerFieldName, ".Setup");
            if (member.Symbol is IMethodSymbol) {
                writer.Write("<", member.MethodRunDelegateType!.Value.FullName, ", ", member.HandlerGenericParameterFullName, ">(");
                WriteCommonMethodHandlerArguments(writer, member, KnownTypes.IMockArgumentMatcher.FullName);
                writer.Write(")");
            }
            else {
                writer.Write("()");
            }

            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupMemberType(CodeWriter writer, in MockTargetMember member) {
            var setupTypeFullName = member.Symbol switch {
                IMethodSymbol => KnownTypes.IMockMethodSetup.FullName,
                IPropertySymbol property => property.SetMethod != null
                    ? KnownTypes.IMockSettablePropertySetup.FullName
                    : KnownTypes.IMockPropertySetup.FullName,
                var s => throw Exceptions.MemberNotSupported(s)
            };

            if (member.Symbol is IPropertySymbol)
                return writer.WriteGeneric(setupTypeFullName, member.TypeFullName);

            if (member.IsVoidMethod)
                return writer.WriteGeneric(setupTypeFullName, member.MethodRunDelegateType!.Value.FullName);

            return writer.WriteGeneric(setupTypeFullName, member.MethodRunDelegateType!.Value.FullName, member.TypeFullName);
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMemberImplementation(CodeWriter writer, in MockTargetMember member) {
            writer.Write(Indents.Member);
            if (member.Symbol.ContainingType.TypeKind == TypeKind.Interface) {
                writer.Write("public ");
            }
            else {
                writer.Write(GetAccessibility(member.Symbol.DeclaredAccessibility), " override ");
            }
            writer.Write(member.TypeFullName, " ", member.Name);

            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMethodGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    WriteMethodImplementationParametersIfAny(writer, member, out var hasOutParameters);
                    writer.Write(")");
                    writer.WriteIfNotNull(member.GenericParameterConstraints, (Environment.NewLine, Indents.Member), " ");
                    WriteMethodImplementationBody(writer, hasOutParameters, member);
                    break;

                case IPropertySymbol property:
                    if (property.SetMethod != null) {
                        writer.WriteLine(" {");
                        writer.Write(Indents.MemberBody, "get => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(
                            writer, member,
                            (KnownTypes.Func.FullName, "<", member.TypeFullName, ">"),
                            parametersOverride: ImmutableArray<MockTargetParameter>.Empty
                        );
                        writer.WriteLine(";");
                        writer.Write(Indents.MemberBody, "set => ", member.HandlerFieldName, ".SetterHandler");
                        WriteMemberImplementationHandlerCall(
                            writer, member,
                            (KnownTypes.Action.FullName, "<", member.TypeFullName, ">")
                        );
                        writer.WriteLine(";");
                        writer.Write(Indents.Member, "}");
                    }
                    else {
                        writer.Write(" => ", member.HandlerFieldName, ".GetterHandler");
                        WriteMemberImplementationHandlerCall(
                            writer, member,
                            (KnownTypes.Func.FullName, "<", member.TypeFullName, ">")
                        );
                        writer.Write(";");
                    }
                    break;

                default:
                    throw Exceptions.MemberNotSupported(member.Symbol);
            }

            return writer;
        }

        [PerformanceSensitive("")]
        private string GetAccessibility(Accessibility accessibility) => accessibility switch {
            Accessibility.Public => "public",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedOrInternal => "protected internal",
            Accessibility.ProtectedAndInternal => "private protected",
            #pragma warning disable HAA0601 // Boxing -- OK in exceptional case
            _ => throw Exceptions.NotSupported($"Unexpected accessibility: {accessibility}")
            #pragma warning restore HAA0601
        };

        [PerformanceSensitive("")]
        private CodeWriter WriteMethodImplementationParametersIfAny(CodeWriter writer, MockTargetMember member, out bool hasOutParameters) {
            hasOutParameters = false;
            foreach (var parameter in member.Parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                if (GetRefModifier(parameter.RefKind) is {} modifier)
                    writer.Write(modifier, " ");
                writer.Write(parameter.FullTypeName, " ", parameter.Name);
                hasOutParameters = hasOutParameters || (parameter.RefKind == RefKind.Out);
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
            _ => throw Exceptions.NotSupported($"Unsupported parameter ref kind: {refKind}")
            #pragma warning restore HAA0601
        };

        [PerformanceSensitive("")]
        private CodeWriter WriteMethodImplementationBody(CodeWriter writer, bool hasOutParameters, in MockTargetMember member) {
            if (hasOutParameters) {
                writer
                    .WriteLine("{")
                    .Write(Indents.MemberBody, "var arguments = ");
                WriteCommonArgumentsArray(writer, "object?", member.Parameters).WriteLine(";");
                writer
                    .Write(Indents.MemberBody, "var result = ", member.HandlerFieldName);
                WriteMemberImplementationHandlerCall(writer, member, argumentsArrayOverride: "arguments")
                    .WriteLine(";");
                foreach (var parameter in member.Parameters) {
                    if (parameter.RefKind is not RefKind.Out or RefKind.Ref)
                        continue;
                    writer.Write(Indents.MemberBody, parameter.Name, " = (", parameter.FullTypeName, ")arguments[", parameter.Index.ToString(), "]");
                    if (!parameter.FullTypeName.EndsWith("?"))
                        writer.Write("!");
                    writer.WriteLine(";");
                }
                writer.WriteLine(Indents.MemberBody, "return result;");
                return writer.Write(Indents.Member, "}");
            }

            writer.Write("=> ", member.HandlerFieldName);
            WriteMemberImplementationHandlerCall(writer, member);
            return writer.Write(";");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMemberImplementationHandlerCall(
            CodeWriter writer,
            in MockTargetMember member,
            (string part1, string part2, string part3, string part4)? runDelegateFullNameOverride = null,
            string? argumentsArrayOverride = null,
            ImmutableArray<MockTargetParameter>? parametersOverride = null
        ) {
            writer.Write(".Call<");
            if (runDelegateFullNameOverride != null) {
                writer.Write(runDelegateFullNameOverride.Value);
            }
            else {
                writer.Write(member.MethodRunDelegateType!.Value.FullName);
            }
            writer.Write(", ", member.HandlerGenericParameterFullName, ">(");
            WriteCommonMethodHandlerArguments(writer, member, "object?", argumentsArrayOverride, parametersOverride ?? member.Parameters);
            return writer.Write(")");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCallsInterfaceMember(CodeWriter writer, in MockTargetMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ");
            return WriteSetupOrCallsInterfaceMemberNameAndParameters(writer, member);
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCallsMemberImplementation(CodeWriter writer, string callsInterfaceName, in MockTargetMember member) {
            writer.Write(Indents.Member);
            WriteCallsMemberType(writer, member);
            writer.Write(" ", callsInterfaceName, ".", member.Name);
            if (member.Symbol is IMethodSymbol) {
                WriteMethodGenericParametersIfAny(writer, member);
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
                        writer.Write("(", parameter.FullTypeName, ")args[", parameter.Index.ToString(), "]");
                        if (!parameter.FullTypeName.EndsWith("?"))
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
        private CodeWriter WriteCallsMemberType(CodeWriter writer, in MockTargetMember member) {
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
                    writer.Write(parameter.FullTypeName, " ", parameter.Name);
                }
                writer.Write(")");
            }
            else if (parameters.Length == 1) {
                writer.Write(parameters[0].FullTypeName);
            }
            else {
                writer.Write(KnownTypes.NoArguments.FullName);
            }

            return writer.Write(">");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupOrCallsInterfaceMemberNameAndParameters(CodeWriter writer, in MockTargetMember member) {
            writer.Write(member.Name);
            switch (member.Symbol) {
                case IMethodSymbol:
                    WriteMethodGenericParametersIfAny(writer, member);
                    writer.Write("(");
                    WriteSetupOrCallsMemberParameters(writer, member, appendDefaultValue: true);
                    writer.Write(")");
                    writer.WriteIfNotNull(member.GenericParameterConstraints);
                    writer.Write(";");
                    break;
                case IPropertySymbol:
                    writer.Write(" { get; }");
                    break;
                default:
                    throw Exceptions.MemberNotSupported(member.Symbol);
            }
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteSetupOrCallsMemberParameters(CodeWriter writer, in MockTargetMember member, bool appendDefaultValue) {
            foreach (var parameter in member.Parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");
                writer
                    .WriteGeneric(KnownTypes.MockArgumentMatcher.FullName, parameter.FullTypeName)
                    .Write(" ", parameter.Name);
                if (appendDefaultValue)
                    writer.Write(" = default");
            }
            return writer;
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCommonMethodHandlerArguments(
            CodeWriter writer,
            in MockTargetMember member,
            string argumentTypeFullName,
            string? argumentsArrayOverride = null,
            ImmutableArray<MockTargetParameter>? parametersOverride = null
        ) {
            var parameters = parametersOverride ?? member.Parameters;

            var genericParameters = member.GenericParameters;
            if (!genericParameters.IsEmpty) {
                writer.Write("new[] { ");
                for (var i = 0; i < genericParameters.Length; i++) {
                    if (i > 0)
                        writer.Write(", ");
                    writer.Write("typeof(", genericParameters[i].Name, ")");
                }
                writer.Write(" }");
            }
            else {
                writer.Write("null");
            }
            writer.Write(", ");

            if (argumentsArrayOverride != null)
                return writer.Write(argumentsArrayOverride);

            return WriteCommonArgumentsArray(writer, argumentTypeFullName, parameters);
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteCommonArgumentsArray(CodeWriter writer, string argumentTypeFullName, ImmutableArray<MockTargetParameter> parameters) {
            if (parameters.IsEmpty)
                return writer.Write("null");

            writer.Write("new ", argumentTypeFullName, "[] { ");
            foreach (var parameter in parameters) {
                if (parameter.Index > 0)
                    writer.Write(", ");

                if (parameter.RefKind == RefKind.Out && argumentTypeFullName == "object?") {
                    writer.Write("default(", parameter.FullTypeName, ")");
                    continue;
                }

                writer.Write(parameter.Name);
            }
            return writer.Write(" }");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteMethodGenericParametersIfAny(CodeWriter writer, in MockTargetMember member) {
            var genericParameters = member.GenericParameters;
            if (genericParameters.IsEmpty)
                return writer;
            writer.Write("<");
            for (var i = 0; i < genericParameters.Length; i++) {
                if (i > 0)
                    writer.Write(", ");
                writer.Write(genericParameters[i].Name);
            }
            return writer.Write(">");
        }

        [PerformanceSensitive("")]
        private CodeWriter WriteExplicitImplementationDefaultConstraintsIfAny(CodeWriter writer, in MockTargetMember member) {
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
    }
}
