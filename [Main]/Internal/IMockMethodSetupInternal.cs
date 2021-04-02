using System.Collections.Generic;

namespace SourceMock.Internal {
    internal interface IMockMethodSetupInternal {
        IReadOnlyList<IMockArgument> Arguments { get; }
        bool HasReturnValue { get; }
        object? ReturnValue { get; }
    }
}
