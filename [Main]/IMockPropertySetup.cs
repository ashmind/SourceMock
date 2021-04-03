namespace SourceMock {
    public interface IMockPropertySetup<T> {
        public IMockMethodSetup<T> get { get; }
        void Returns(T value);
    }
}
