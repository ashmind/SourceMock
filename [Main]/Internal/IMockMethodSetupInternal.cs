namespace SourceMock.Internal {
    internal interface IMockMethodSetupInternal : IMockMethodSetup {
        bool Matches(MockCall call);
    }
}
