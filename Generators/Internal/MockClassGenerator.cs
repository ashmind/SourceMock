using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using SourceMock.Generators.Internal.Models;

namespace SourceMock.Generators.Internal {
    internal class MockClassGenerator {
        private static readonly SymbolDisplayFormat TargetTypeNamespaceDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
            SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining
        );

        private readonly MockTargetModelFactory _modelFactory;
        private readonly MockMemberGenerator _mockMemberGenerator = new();

        public MockClassGenerator(MockTargetModelFactory modelFactory) {
            _modelFactory = modelFactory;
        }

        [PerformanceSensitive("")]
        public string Generate(MockTarget target) {
            var targetTypeNamespace = target.Type.ContainingNamespace.ToDisplayString(TargetTypeNamespaceDisplayFormat);
            var mockBaseName = GenerateMockBaseName(target.Type.Name);
            var typeParameters = GenerateTypeParametersAsString(target);
            var mockClassName = mockBaseName + "Mock" + typeParameters;
            var setupInterfaceName = "I" + mockBaseName + "Setup" + typeParameters;
            var callsInterfaceName = "I" + mockBaseName + "Calls" + typeParameters;
            var customDelegatesClassName = mockBaseName + "Delegates" + typeParameters;

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
            var customDelegatesClassWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine(Indents.Type, "public static class ", customDelegatesClassName, " {");
            var customDelegatesEmptyLength = customDelegatesClassWriter.CurrentLength;

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var setupInterfaceWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine(Indents.Type, "public interface ", setupInterfaceName, " {");

            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            var callsInterfaceWriter = new CodeWriter()
            #pragma warning restore HAA0502
                .WriteLine(Indents.Type, "public interface ", callsInterfaceName, " {");

            #pragma warning disable HAA0401 // Possible allocation of reference type enumerator - TODO
            foreach (var member in _modelFactory.GetMockTargetMembers(target, customDelegatesClassName)) {
            #pragma warning restore HAA0401
                mainWriter.WriteLine();
                _mockMemberGenerator.WriteMemberMocks(
                    mainWriter,
                    customDelegatesClassWriter,
                    setupInterfaceWriter,
                    setupInterfaceName,
                    callsInterfaceWriter,
                    callsInterfaceName,
                    member
                ).WriteLine();
            }

            mainWriter.WriteLine(Indents.Type, "}");

            if (customDelegatesClassWriter.CurrentLength != customDelegatesEmptyLength) {
                customDelegatesClassWriter.Write(Indents.Type, "}");
                mainWriter
                    .WriteLine()
                    .Append(customDelegatesClassWriter)
                    .WriteLine();
            }

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
                if (index > 0)
                    writer.Write(", ");
                writer.Write(parameter.Name);
                index += 1;
            }
            writer.Write(">");
            return writer.ToString();
        }
    }
}
