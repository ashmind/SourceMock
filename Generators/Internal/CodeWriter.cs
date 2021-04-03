using System.Text;

namespace SourceMock.Generators.Internal {
    internal class CodeWriter {
        private readonly StringBuilder _builder;

        public CodeWriter() {
            _builder = new StringBuilder();
        }

        public CodeWriter Write(string value) {
            _builder.Append(value);
            return this;
        }

        public CodeWriter Write(string part1, string part2) {
            _builder
                .Append(part1)
                .Append(part2);
            return this;
        }

        public CodeWriter Write(string part1, string part2, string part3) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3);
            return this;
        }

        public CodeWriter Write(string part1, string part2, string part3, string part4) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4);
            return this;
        }

        internal CodeWriter Write(string part1, string part2, string part3, string part4, string part5) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5);
            return this;
        }

        public CodeWriter Write(string part1, string part2, string part3, string part4, string part5, string part6) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5)
                .Append(part6);
            return this;
        }

        public CodeWriter WriteLine() {
            _builder.AppendLine();
            return this;
        }

        public CodeWriter WriteLine(string line) {
            _builder.AppendLine(line);
            return this;
        }

        public CodeWriter WriteLine(string part1, string part2) {
            _builder
                .Append(part1)
                .AppendLine(part2);
            return this;
        }

        public CodeWriter WriteLine(string part1, string part2, string part3) {
            _builder
                .Append(part1)
                .Append(part2)
                .AppendLine(part3);
            return this;
        }

        public CodeWriter WriteLine(string part1, string part2, string part3, string part4) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .AppendLine(part4);
            return this;
        }

        public CodeWriter WriteLine(string part1, string part2, string part3, string part4, string part5, string part6) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5)
                .AppendLine(part6);
            return this;
        }

        public CodeWriter WriteGeneric(string genericTypeName, string genericArgumentName) {
            _builder
                .Append(genericTypeName)
                .Append("<")
                .Append(genericArgumentName)
                .Append(">");
            return this;
        }

        public CodeWriter Append(CodeWriter other) {
            _builder.Append(other._builder);
            return this;
        }

        public override string ToString() => _builder.ToString();
    }
}
