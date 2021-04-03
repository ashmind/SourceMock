using System.Collections.Generic;

namespace SourceMock {
    public interface IMockPropertyCalls<T> {
        IReadOnlyList<NoArguments> get { get; }
    }
}
