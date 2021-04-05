using System;
using System.Collections.Generic;

namespace SourceMock.Internal {
    internal readonly struct MockCall {
        public MockCall(IReadOnlyList<Type> genericArguments, IReadOnlyList<object?> arguments) {
            GenericArguments = genericArguments;
            Arguments = arguments;
        }

        public IReadOnlyList<Type> GenericArguments { get; }
        public IReadOnlyList<object?> Arguments { get; }

        public bool Matches(IReadOnlyList<Type> genericArguments, IReadOnlyList<IMockArgumentMatcher> arguments) {
            for (var i = 0; i < genericArguments.Count; i++) {
                if (!Equals(genericArguments[i], GenericArguments[i]))
                    return false;
            }
            for (var i = 0; i < arguments.Count; i++) {
                if (!arguments[i].Matches(Arguments[i]))
                    return false;
            }
            return true;
        }
    }
}
