namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTargetGenericParameter {
        public MockTargetGenericParameter(string name, string? constraintsCode) {
            Name = name;
            ConstraintsCode = constraintsCode;
        }

        public string Name { get; }
        public string? ConstraintsCode { get; }
    }
}
