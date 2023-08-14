using System;

namespace SourceMock.Generators.Internal {
    internal static class Indents {
#pragma warning disable RS1035 // Do not use APIs banned for analyzers
        public static readonly string NewLine = Environment.NewLine;
#pragma warning restore RS1035 // Do not use APIs banned for analyzers
        public const string Type = "    ";
        public const string Member = Type + "    ";
        public const string MemberBody = Member + "    ";
    }
}
