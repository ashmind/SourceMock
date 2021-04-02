using System;
using SourceMock.Internal;

namespace SourceMock {
    public static class It {
        public static MockArgument<T> Is<T>(Func<T, bool> match) {
            return new MockArgument<T>(match);
        }
    }
}
