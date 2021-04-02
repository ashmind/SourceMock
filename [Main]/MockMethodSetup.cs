using System.Collections.Generic;
using SourceMock.Internal;

namespace SourceMock {
    public class MockMethodSetup<TReturn> : IMockMethodSetup {
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

        IReadOnlyList<IMockArgument> IMockMethodSetup.Arguments => Arguments;
        bool IMockMethodSetup.HasReturnValue => HasReturnValue;
        object? IMockMethodSetup.ReturnValue => ReturnValue;
    }
}
