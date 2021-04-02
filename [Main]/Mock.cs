namespace SourceMock {
    public class Mock<T> {
        internal Mock() {
        }
    }

    public static class Mock {
        public static Mock<T> Of<T>() {
            return new Mock<T>();
        }
    }
}
