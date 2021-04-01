namespace SourceMock
{
    public class MockMethodSetup<TReturn>
    {
        public void Returns(TReturn value)
        {
            HasReturnValue = true;
            ReturnValue = value;
        }

        internal bool HasReturnValue { get; private set; }
        internal TReturn? ReturnValue { get; private set; }
    }
}
