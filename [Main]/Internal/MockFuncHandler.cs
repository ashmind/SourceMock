namespace SourceMock.Handlers {
    public class MockFuncHandler<TReturn> {
        public MockMethodSetup<TReturn> Setup { get; } = new MockMethodSetup<TReturn>();

        public TReturn Call() {
            if (!Setup.HasReturnValue)
                return default!;

            return Setup.ReturnValue!;
        }
    }
}
