using System;

namespace SourceMock.Internal {
    /// <summary>This type supports generated code and is not intended to be used directly.</summary>
    public readonly struct MockArgumentMatcher<T>: IMockArgumentMatcher {
        private readonly Func<T, bool>? _matches;

        private MockArgumentMatcher(Func<T, bool> matches) {
            _matches = matches;
        }

        bool IMockArgumentMatcher.Matches(object? argument) => _matches == null || (argument switch {
            T typed => _matches(typed),
            null => _matches((T)((object?)null)!),
            _ => false
        });

        /// <summary>This operator supports generated code and is not intended to be used directly.</summary>
        public static implicit operator MockArgumentMatcher<T>(T value) => new(v => Equals(v, value));
    }
}
