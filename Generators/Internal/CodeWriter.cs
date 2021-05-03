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
        public CodeWriter Write(string part1, string part2, string part3, string part4, string part5, string part6, string part7) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5)
                .Append(part6)
                .Append(part7);
            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Write((string part1, string part2, string part3, string part4) parts) {
            _builder
                .Append(parts.part1)
                .Append(parts.part2)
                .Append(parts.part3)
                .Append(parts.part4);
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
        public CodeWriter WriteLine(string part1, string part2, string part3, string part4, string part5, string part6, string part7) {
            _builder
                .Append(part1)
                .Append(part2)
                .Append(part3)
                .Append(part4)
                .Append(part5)
                .Append(part6)
                .AppendLine(part7);
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
        public CodeWriter WriteGeneric(string genericTypeName, string genericArgumentName) {
            _builder
                .Append(genericTypeName)
                .Append("<")
                .Append(genericArgumentName)
                .Append(">");

            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter WriteGeneric(string genericTypeName, string genericArgumentName1, string genericArgumentName2) {
            _builder
                .Append(genericTypeName)
                .Append("<")
                .Append(genericArgumentName1)
                .Append(", ")
                .Append(genericArgumentName2)
                .Append(">");

            return this;
        }

        public CodeWriter WriteIfNotNull(string? value) {
            _builder.Append(value);
            return this;
        }

        public CodeWriter WriteIfNotNull(string? value, (string part1, string part2) ifNotNull, string ifNull) {
            if (value != null) {
                _builder
                    .Append(value)
                    .Append(ifNotNull.part1)
                    .Append(ifNotNull.part2);
            }
            else {
                _builder.Append(ifNull);
            }

            return this;
        }

        public CodeWriter WriteIfNotNull(string? value, (string part1, string part2, string part3) ifNotNull, string ifNull) {
            if (value != null) {
                _builder
                    .Append(value)
                    .Append(ifNotNull.part1)
                    .Append(ifNotNull.part2)
                    .Append(ifNotNull.part3);
            }
            else {
                _builder.Append(ifNull);
            }

            return this;
        }

        [PerformanceSensitive("")]
        public CodeWriter Append(CodeWriter other) {
            _builder.Append(other._builder);
            return this;
        }

        public int CurrentLength => _builder.Length;

        public override string ToString() => _builder.ToString();
    }
}
