using System;
using SourceMock.Internal;

namespace SourceMock {
    public class MockArgument<T>: IMockArgument {
        private MockArgument(Func<T, bool> matches) {
            Matches = matches;
        }

        public Func<T, bool> Matches { get; }

        bool IMockArgument.Matches(object? argument) =>
            argument is T typed && Matches(typed);

        public static implicit operator MockArgument<T>(T value) {
            return new MockArgument<T>(v => Equals(v, value));
        }
    }
}
