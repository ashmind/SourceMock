namespace SourceMock.Tests.Interfaces {
    public interface INeedsGenerics {
        T Parse<T>(string value);
    }
}
