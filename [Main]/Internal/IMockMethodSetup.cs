using System.Collections.Generic;

namespace SourceMock.Internal {
    internal interface IMockMethodSetup {
        IReadOnlyList<IMockArgument> Arguments { get; }
        bool HasReturnValue { get; }
        object? ReturnValue { get; }
    }
}
