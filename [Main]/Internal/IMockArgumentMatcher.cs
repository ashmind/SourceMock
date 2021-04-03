namespace SourceMock.Internal {
    public interface IMockArgumentMatcher {
        bool Matches(object? argument);
    }
}
