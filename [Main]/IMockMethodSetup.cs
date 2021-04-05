using System;

namespace SourceMock {
    public interface IMockMethodSetup {
        void Throws(Exception exception);
        void Throws<TException>()
            where TException: Exception, new();
    }

    public interface IMockMethodSetup<TReturn> : IMockMethodSetup {
        void Returns(TReturn value);
    }
}
