namespace SourceMock.Handlers {
    public class MockFuncHandler {
        public MockSetup Setup { get; } = new MockSetup();
        public int Call() {
            return Setup.ReturnValue;
        }
    }
}
