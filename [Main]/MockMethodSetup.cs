namespace SourceMock
{
    public class MockMethodSetup
    {
        public void Returns(int value)
        {
            ReturnValue = value;
        }

        internal int ReturnValue { get; private set; }
    }
}
