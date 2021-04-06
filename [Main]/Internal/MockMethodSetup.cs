using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceMock.Internal {
    internal class MockMethodSetup<TCallback, TReturn> : IMockMethodSetup<TCallback, TReturn>, IMockMethodSetupInternal where TCallback : Delegate  {
        private readonly IReadOnlyList<Type> _genericArguments;
        private readonly IReadOnlyList<IMockArgumentMatcher> _arguments;
        private bool _hasReturnValue;
        private TReturn? _returnValue;
        private Exception? _exception;
        private TCallback? _callback;

        public MockMethodSetup(IReadOnlyList<Type> genericArguments, IReadOnlyList<IMockArgumentMatcher> arguments) {
            _genericArguments = genericArguments;
            _arguments = arguments;
        }

        public void Returns(TReturn value) {
            _hasReturnValue = true;
            _returnValue = value;
        }

        public void Throws(Exception exception) {
            _exception = exception;
        }

        public void Throws<TException>()
            where TException : Exception, new() {
            Throws(new TException());
        }

        public void Callback(TCallback callback) {
            _callback = callback;
        }

        public bool Matches(MockCall call) => call.Matches(_genericArguments, _arguments);

        public TReturn? Execute(IReadOnlyList<object?>? arguments) {
            if (_callback != null)
                _callback.DynamicInvoke(arguments.ToArray());

            if (_exception != null)
                throw _exception;

            if (!_hasReturnValue)
                return DefaultValue.Get<TReturn>();

            return _returnValue;
        }
    }
}
