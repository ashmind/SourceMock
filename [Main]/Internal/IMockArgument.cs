namespace SourceMock.Internal {
    public interface IMockArgument {
        bool Matches(object? argument);
    }
}