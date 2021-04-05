using System;
using System.Collections.Generic;

namespace SourceMock.Internal {
    internal class MockMethodSetup<TReturn> : IMockMethodSetup<TReturn> {
        private bool _hasReturnValue;
        private TReturn? _returnValue;
        private Exception? _exception;

        public MockMethodSetup(IReadOnlyList<IMockArgumentMatcher> arguments) {
            Arguments = arguments;
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

        public IReadOnlyList<IMockArgumentMatcher> Arguments { get; }

        public TReturn? Execute() {
            if (_exception != null)
                throw _exception;

            if (!_hasReturnValue)
                return DefaultValue.Get<TReturn>();

            return _returnValue;
        }
    }
}
