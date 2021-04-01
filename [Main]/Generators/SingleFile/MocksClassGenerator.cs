using System.Text;
using SourceMock.Generators.Models;

namespace SourceMock.Generators.SingleFile {
    internal class MocksClassGenerator {
        public StringBuilder Start() {
            return new StringBuilder("public static class Mocks {").AppendLine();
        }

        public void Append(StringBuilder builder, in MockInfo mock) {
            builder
                .Append("    public static ")
                .Append(mock.MockTypeName)
                .Append(" Get(")
                .Append(mock.TargetTypeQualifiedName)
                .AppendLine("? _) => new();");
        }

        public string End(StringBuilder builder) {
            return builder.Append("}").ToString();
        }
    }
}
