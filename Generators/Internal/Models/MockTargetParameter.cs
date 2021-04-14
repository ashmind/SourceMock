using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal.Models {
    internal readonly struct MockTargetParameter {
        public MockTargetParameter(string name, string typeFullName, RefKind refKind, int index) {
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
