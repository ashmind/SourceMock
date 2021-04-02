using System;
using SourceMock.Internal;

namespace SourceMock {
    public readonly struct MockArgument<T>: IMockArgument {
        private readonly Func<T, bool>? _matches;

        private MockArgument(Func<T, bool> matches) {
            _matches = matches;
        }

        bool IMockArgument.Matches(object? argument) => _matches == null || (argument switch {
            T typed => _matches(typed),
            null => _matches((T)((object?)null)!),
            _ => false
        });

        public static implicit operator MockArgument<T>(T value) => new(v => Equals(v, value));
    }
}
