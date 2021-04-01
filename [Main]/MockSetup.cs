namespace SourceMock
{
    public class MockSetup
    {
        public void Returns(int value)
        {
            ReturnValue = value;
        }

        internal int ReturnValue { get; private set; }
    }
}
