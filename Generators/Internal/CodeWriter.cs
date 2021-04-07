using System.Text;
using Roslyn.Utilities;

namespace SourceMock.Generators.Internal {
    internal class CodeWriter {
        private readonly StringBuilder _builder;

        [PerformanceSensitive("")]
        public CodeWriter() {
            #pragma warning disable HAA0502 // Explicit allocation -- unavoidable for now, can be pooled later
            _builder = new StringBuilder();
            #pragma warning restore HAA0502
        }

        [PerformanceSensitive("")]
        public CodeWriter Write(string value) {
            _builder.Append(value);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Write(string part1, string part2) {
            _builder
                .Append(part1)
                .Append(part2);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Write(string part1, string part2, string part3) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Write(string part1, string part2, string part3, string part4) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4);
            return this;
        }

        [PerformanceSensitive("")]
        internal CodeWriter Write(string part1, string part2, string part3, string part4, string part5) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5);
            return this;
        }

        [PerformanceSensitive("")]
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

        [PerformanceSensitive("")]
        public CodeWriter WriteLine() {
            _builder.AppendLine();
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string line) {
            _builder.AppendLine(line);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string part1, string part2) {
            _builder
                .Append(part1)
                .AppendLine(part2);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string part1, string part2, string part3) {
            _builder
                .Append(part1)
                .Append(part2)
                .AppendLine(part3);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string part1, string part2, string part3, string part4) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .AppendLine(part4);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string part1, string part2, string part3, string part4, string part5) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .AppendLine(part5);
            return this;
        }

        [PerformanceSensitive("")]
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

        [PerformanceSensitive("")]
        public CodeWriter WriteLine(string part1, string part2, string part3, string part4, string part5, string part6, string part7, string part8) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5)
                .Append(part6)
                .Append(part7)
                .AppendLine(part8);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteGeneric(string genericTypeName, params string[] genericArgumentNames) {
            _builder
                .Append(genericTypeName)
                .Append("<")
                .Append(string.Join(",", genericArgumentNames))
                .Append(">");

            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Append(CodeWriter other) {
            _builder.Append(other._builder);
            return this;
        }

        public override string ToString() => _builder.ToString();
    }
}
