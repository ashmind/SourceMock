using System.Collections.Generic;
using SourceMock.Interfaces;

namespace SourceMock.Internal {
    internal class MockPropertyCalls<T> : IMockSettablePropertyCalls<T> {
        private readonly MockPropertyHandler<T> _handler;

        public MockPropertyCalls(MockPropertyHandler<T> handler) {
            _handler = handler;
        }

        public IReadOnlyList<NoArguments> get => _handler.GetterHandler.Calls(null, null, _ => NoArguments.Value);

        public IReadOnlyList<T> set(MockArgumentMatcher<T> value = default) => _handler.SetterHandler.Calls(null, new IMockArgumentMatcher[] { value }, args => (T)args[0]!);
    }
}
