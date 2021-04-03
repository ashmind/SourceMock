using System;
using SourceMock.Internal;

namespace SourceMock {
    public static class It {
        public static MockArgumentMatcher<T> Is<T>(Func<T, bool> match) {
            return new MockArgumentMatcher<T>(match);
        }
    }
}
