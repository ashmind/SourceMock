namespace SourceMock.Internal {
    internal interface IMockCallMatcher
    {
        bool Matches(MockCall call);
    }
}
