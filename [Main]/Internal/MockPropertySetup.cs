using System;

namespace SourceMock.Internal {
    internal class MockPropertySetup<T> : IMockSettablePropertySetup<T> {
        private readonly MockPropertyHandler<T> _handler;

        public MockPropertySetup(MockPropertyHandler<T> handler) {
            _handler = handler;
        }

        public IMockMethodSetup<T> get => _handler.GetterHandler.Setup();

        public IMockMethodSetup set(MockArgumentMatcher<T> value = default) => (
            _handler.SetterHandler ?? throw new InvalidOperationException("Attempted to set up setter for property with no setter.")
        ).Setup(value);

        public void Returns(T value) => get.Returns(value);
    }
}
