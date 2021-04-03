namespace SourceMock {
    public interface IMockMethodSetup {
    }

    public interface IMockMethodSetup<TReturn> : IMockMethodSetup {
        void Returns(TReturn value);
    }
}
