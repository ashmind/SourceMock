using System.Collections.Generic;

namespace SourceMock.Internal {
    public class MockPropertyCalls<T> : IMockSettablePropertyCalls<T> {
        private readonly MockPropertyHandler<T> _handler;

        public MockPropertyCalls(MockPropertyHandler<T> handler) {
            _handler = handler;
        }

        public IReadOnlyList<NoArguments> get => _handler.GetterHandler.Calls(_ => NoArguments.Value);

        public IReadOnlyList<T> set(MockArgumentMatcher<T> value = default) => _handler.SetterHandler.Calls(args => (T)args[0]!, value);
    }
}
