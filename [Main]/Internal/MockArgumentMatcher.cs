using System;

namespace SourceMock.Internal {
    public readonly struct MockArgumentMatcher<T>: IMockArgumentMatcher {
        private readonly Func<T, bool>? _matches;

        internal MockArgumentMatcher(Func<T, bool> matches) {
            _matches = matches;
        }

        bool IMockArgumentMatcher.Matches(object? argument) => _matches == null || (argument switch {
            T typed => _matches(typed),
            null => _matches((T)((object?)null)!),
            _ => false
        });

        public static implicit operator MockArgumentMatcher<T>(T value) => new(v => Equals(v, value));
    }
}
