using SourceMock.Internal;

namespace SourceMock {
    public interface IMockSettablePropertySetup<T> : IMockPropertySetup<T> {
        IMockMethodSetup set(MockArgumentMatcher<T> value = default);
    }
}
