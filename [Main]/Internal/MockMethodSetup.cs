using System.Collections.Generic;

namespace SourceMock.Internal {
    internal class MockMethodSetup<TReturn> : IMockMethodSetup<TReturn>, IMockMethodSetupInternal {
        public MockMethodSetup(IReadOnlyList<IMockArgumentMatcher> arguments) {
            Arguments = arguments;
        }

        public void Returns(TReturn value) {
            HasReturnValue = true;
            ReturnValue = value;
        }

        public IReadOnlyList<IMockArgumentMatcher> Arguments { get; }
        public bool HasReturnValue { get; private set; }
        public TReturn? ReturnValue { get; private set; }
        object? IMockMethodSetupInternal.ReturnValue => ReturnValue;
    }
}
