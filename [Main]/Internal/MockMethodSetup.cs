using System;
using System.Collections.Generic;

namespace SourceMock.Internal {
    internal class MockMethodSetup<TReturn> : IMockMethodSetup<TReturn>, IMockMethodSetupInternal {
        private readonly IReadOnlyList<Type> _genericArguments;
        private readonly IReadOnlyList<IMockArgumentMatcher> _arguments;
        private bool _hasReturnValue;
        private TReturn? _returnValue;
        private Exception? _exception;

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

        public bool Matches(MockCall call) => call.Matches(_genericArguments, _arguments);

        public TReturn? Execute() {
            if (_exception != null)
                throw _exception;

            if (!_hasReturnValue)
                return DefaultValue.Get<TReturn>();

            return _returnValue;
        }
    }
}
