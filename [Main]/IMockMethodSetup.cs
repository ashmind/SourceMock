using System;

namespace SourceMock {
    public interface IMockMethodSetup {
        void Throws(Exception exception);
    }

    public interface IMockMethodSetup<TReturn> : IMockMethodSetup {
        void Returns(TReturn value);
    }
}
