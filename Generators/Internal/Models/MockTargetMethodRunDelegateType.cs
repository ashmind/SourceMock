namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTargetMethodRunDelegateType {
        public MockTargetMethodRunDelegateType(string fullName) {
            FullName = fullName;
            CustomNameWithGenericParameters = null;
        }

        private MockTargetMethodRunDelegateType(string fullName, string customNameWithGenericParameters) {
            FullName = fullName;
            CustomNameWithGenericParameters = customNameWithGenericParameters;
        }

        public static MockTargetMethodRunDelegateType Custom(string customNameWithGenericParameters, string fullName) {
            return new(fullName, customNameWithGenericParameters);
        }

        public string FullName { get; }
        public string? CustomNameWithGenericParameters { get; }
        public bool IsCustom => CustomNameWithGenericParameters != null;
    }
}
