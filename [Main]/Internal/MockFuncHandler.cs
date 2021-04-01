namespace SourceMock.Handlers {
    public class MockFuncHandler {
        public MockMethodSetup Setup { get; } = new MockMethodSetup();
        public int Call() {
            return Setup.ReturnValue;
        }
    }
}
