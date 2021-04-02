namespace SourceMock {
    public interface IMockMethodSetup<TReturn> {
        void Returns(TReturn value);
    }
}
