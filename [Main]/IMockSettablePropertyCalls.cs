using System.Collections.Generic;
using SourceMock.Internal;

namespace SourceMock {
    public interface IMockSettablePropertyCalls<T> : IMockPropertyCalls<T> {
        IReadOnlyList<T> set(MockArgumentMatcher<T> value = default);
    }
}
