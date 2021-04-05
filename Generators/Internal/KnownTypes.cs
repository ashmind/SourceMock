using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal static class KnownTypes {
        public static class GenerateMocksForAssemblyOfAttribute {
            public const string Name = "GenerateMocksForAssemblyOfAttribute";
            public const string NameWithoutAttribute = "GenerateMocksForAssemblyOf";

            public static bool NamespaceMatches(INamespaceSymbol? @namespace) => @namespace is {
                Name: "SourceMock", ContainingNamespace: { IsGlobalNamespace: true }
            };
        }

        public static class GeneratedMockAttribute {
            public const string Name = "GeneratedMockAttribute";
            public const string FullNameWithoutAttribute = "SourceMock.Internal.GeneratedMock";

            public static bool NamespaceMatches(INamespaceSymbol? @namespace) => @namespace is {
                Name: "Internal",
                ContainingNamespace: {
                    Name: "SourceMock",
                    ContainingNamespace: { IsGlobalNamespace: true }
                }
            };
        }

        public static class Mock {
            public const string FullName = "SourceMock.Mock";
        }

        public static class IMockMethodSetup {
            public const string FullName = "SourceMock.IMockMethodSetup";
        }

        public static class MockMethodHandler {
            public const string FullName = "SourceMock.Internal.MockMethodHandler";
        }

        public static class IMockPropertySetup {
            public const string FullName = "SourceMock.IMockPropertySetup";
        }

        public static class IMockSettablePropertySetup {
            public const string FullName = "SourceMock.IMockSettablePropertySetup";
        }

        public static class IMockPropertyCalls {
            public const string FullName = "SourceMock.IMockPropertyCalls";
        }

        public static class IMockSettablePropertyCalls {
            public const string FullName = "SourceMock.IMockSettablePropertyCalls";
        }

        public static class MockPropertyHandler {
            public const string FullName = "SourceMock.Internal.MockPropertyHandler";
        }

        public static class MockArgumentMatcher {
            public const string FullName = "SourceMock.Internal.MockArgumentMatcher";
        }

        public static class NoArguments {
            public const string FullName = "SourceMock.NoArguments";
        }

        public static class VoidReturn {
            public const string FullName = "SourceMock.Internal.VoidReturn";
        }

        public static class IReadOnlyList {
            public const string FullName = "System.Collections.Generic.IReadOnlyList";
        }
    }
}
