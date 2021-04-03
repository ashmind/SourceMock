using System;

namespace SourceMock.Internal {
    public class MockPropertyHandler<T> {
        private readonly MockMethodHandler<VoidReturn>? _setterHandler;

        public MockPropertyHandler(bool isSettable) {
            GetterHandler = new MockMethodHandler<T>();
            _setterHandler = isSettable ? new MockMethodHandler<VoidReturn>() : null;
        }

        public MockMethodHandler<T> GetterHandler { get; }
        public MockMethodHandler<VoidReturn> SetterHandler => _setterHandler
            ?? throw new InvalidOperationException("Attempted to set up setter for property with no setter.");

        public IMockSettablePropertySetup<T> Setup() => new MockPropertySetup<T>(this);
        public IMockSettablePropertyCalls<T> Calls() => new MockPropertyCalls<T>(this);
    }
}
