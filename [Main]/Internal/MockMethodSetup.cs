using System.Collections.Generic;
using SourceMock.Internal;

namespace SourceMock {
    internal class MockMethodSetup<TReturn> : IMockMethodSetupInternal, IMockMethodSetup<TReturn> {
        public MockMethodSetup(IReadOnlyList<IMockArgument> arguments) {
            Arguments = arguments;
        }

        public void Returns(TReturn value) {
            HasReturnValue = true;
            ReturnValue = value;
        }

        internal IReadOnlyList<IMockArgument> Arguments { get; }
        internal bool HasReturnValue { get; private set; }
        internal TReturn? ReturnValue { get; private set; }

        IReadOnlyList<IMockArgument> IMockMethodSetupInternal.Arguments => Arguments;
        bool IMockMethodSetupInternal.HasReturnValue => HasReturnValue;
        object? IMockMethodSetupInternal.ReturnValue => ReturnValue;
    }
}
