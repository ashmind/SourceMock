using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Models {
    internal readonly struct MockInfo {
        public MockInfo(string mockTypeName, ITypeSymbol targetType, string targetTypeQualifiedName)
        {
            MockTypeName = mockTypeName;
            TargetType = targetType;
            TargetTypeQualifiedName = targetTypeQualifiedName;
        }

        public string MockTypeName { get; }
        public ITypeSymbol TargetType { get; }
        public string TargetTypeQualifiedName { get; }
    }
}
