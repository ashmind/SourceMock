using System;

namespace SourceMock.Internal {
    public class MockPropertyHandler<T> {
        private readonly MockMethodHandler? _setterHandler;

        public MockPropertyHandler(bool isSettable) {
            GetterHandler = new MockMethodHandler();
            _setterHandler = isSettable ? new MockMethodHandler() : null;
        }

        public MockMethodHandler GetterHandler { get; }
        public MockMethodHandler SetterHandler => _setterHandler
            ?? throw new InvalidOperationException("Attempted to set up setter for property with no setter.");

        public IMockSettablePropertySetup<T> Setup() => new MockPropertySetup<T>(this);
        public IMockSettablePropertyCalls<T> Calls() => new MockPropertyCalls<T>(this);
    }
}
