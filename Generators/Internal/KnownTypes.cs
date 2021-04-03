namespace SourceMock.Generators.Internal {
    internal static class KnownTypes {
        public static class Mock {
            public const string Namespace = "SourceMock";
            public const string Name = "Mock";
            public const string FullName = Namespace + "." + Name;
        }

        public static class IMockMethodSetup {
            public const string Namespace = "SourceMock";
            public const string Name = "IMockMethodSetup";
            public const string FullName = Namespace + "." + Name;
        }

        public static class MockMemberHandler {
            public const string Namespace = "SourceMock.Internal";
            public const string Name = "MockMemberHandler";
            public const string FullName = Namespace + "." + Name;
        }

        public static class MockArgumentMatcher {
            public const string Namespace = "SourceMock.Internal";
            public const string Name = "MockArgumentMatcher";
            public const string FullName = Namespace + "." + Name;
        }

        public static class NoArguments {
            public const string Namespace = "SourceMock";
            public const string Name = "NoArguments";
            public const string FullName = Namespace + "." + Name;
        }

        public static class IReadOnlyList {
            public const string Namespace = "System.Collections.Generic";
            public const string Name = "IReadOnlyList";
            public const string FullName = Namespace + "." + Name;
        }
    }
}
