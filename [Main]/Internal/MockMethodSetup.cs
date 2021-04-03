using System.Collections.Generic;
using SourceMock.Internal;

namespace SourceMock {
    internal class MockMethodSetup : IMockMethodSetupInternal, IMockMethodSetup {
        public MockMethodSetup(IReadOnlyList<IMockArgumentMatcher> arguments) {
            Arguments = arguments;
        }

        public IReadOnlyList<IMockArgumentMatcher> Arguments { get; }
        bool IMockMethodSetupInternal.HasReturnValue => false;
        object? IMockMethodSetupInternal.ReturnValue => null;
    }

    internal class MockMethodSetup<TReturn> : MockMethodSetup, IMockMethodSetup<TReturn>, IMockMethodSetupInternal {
        public MockMethodSetup(IReadOnlyList<IMockArgumentMatcher> arguments) : base(arguments) {
        }

        public void Returns(TReturn value) {
            HasReturnValue = true;
            ReturnValue = value;
        }

        public bool HasReturnValue { get; private set; }
        public TReturn? ReturnValue { get; private set; }
        object? IMockMethodSetupInternal.ReturnValue => ReturnValue;
    }
}
