using System;
using Microsoft.CodeAnalysis;

namespace SourceMock.Generators.Internal {
    internal static class Exceptions {

        // Having this as a separate method removes need to suppress allocation warnings each time in exceptional situations
        public static NotSupportedException NotSupported(string message) => new(message);

        public static NotSupportedException MemberNotSupported(ISymbol symbol) => Exceptions.NotSupported(
            $"{symbol.Name} has an unsupported member symbol type ({symbol.GetType()})"
        );
    }
}
