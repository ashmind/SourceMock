using System;
using System.Collections.Generic;
using System.Linq;
using SourceMock.Interfaces;

namespace SourceMock.Internal {
    internal class MockMethodSetup<TRun, TReturn> : IMockMethodSetup<TRun, TReturn>, IMockCallMatcher
        where TRun : Delegate
    {
        private readonly IReadOnlyList<Type> _genericArguments;
        private readonly IReadOnlyList<IMockArgumentMatcher> _arguments;
        private bool _hasReturnValue;
        private TReturn? _returnValue;
        private Exception? _exception;
        private TRun? _callback;

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

        public void Runs(TRun callback) {
            _callback = callback;
        }

        public bool Matches(MockCall call) => call.Matches(_genericArguments, _arguments);

        public TReturn? Execute(object?[] arguments) {
            if (_callback != null)
                return (TReturn?)_callback.DynamicInvoke(arguments);

            if (_exception != null)
                throw _exception;

            if (!_hasReturnValue)
                return DefaultValue.Get<TReturn>();

            return _returnValue;
        }
    }
}
