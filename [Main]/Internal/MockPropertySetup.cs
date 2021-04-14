using System;
using SourceMock.Interfaces;

namespace SourceMock.Internal {
    internal class MockPropertySetup<T> : IMockSettablePropertySetup<T> {
        private readonly MockPropertyHandler<T> _handler;

        public MockPropertySetup(MockPropertyHandler<T> handler) {
            _handler = handler;
        }

        public IMockMethodSetup<Func<T>, T> get => _handler.GetterHandler.Setup<Func<T>, T>(null, null);

        public IMockMethodSetup<Action<T>> set(MockArgumentMatcher<T> value = default) => (
            _handler.SetterHandler ?? throw new InvalidOperationException("Attempted to set up setter for property with no setter.")
        ).Setup<Action<T>, VoidReturn>(null, new IMockArgumentMatcher[] { value });

        public void Returns(T value) => get.Returns(value);
    }
}
