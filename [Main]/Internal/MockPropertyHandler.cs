using System;

namespace SourceMock.Internal {
    /// <summary>This type supports generated code and is not intended to be used directly.</summary>
    public class MockPropertyHandler<T> {
        private readonly MockMethodHandler? _setterHandler;

        /// <summary>This constructor supports generated code and is not intended to be used directly.</summary>
        public MockPropertyHandler(bool isSettable) {
            GetterHandler = new MockMethodHandler();
            _setterHandler = isSettable ? new MockMethodHandler() : null;
        }

        /// <summary>This property supports generated code and is not intended to be used directly.</summary>
        public MockMethodHandler GetterHandler { get; }
        /// <summary>This property supports generated code and is not intended to be used directly.</summary>
        public MockMethodHandler SetterHandler => _setterHandler
            ?? throw new InvalidOperationException("Attempted to set up setter for property with no setter.");

        /// <summary>This methpd supports generated code and is not intended to be used directly.</summary>
        public IMockSettablePropertySetup<T> Setup() => new MockPropertySetup<T>(this);
        /// <summary>This methpd supports generated code and is not intended to be used directly.</summary>
        public IMockSettablePropertyCalls<T> Calls() => new MockPropertyCalls<T>(this);
    }
}
