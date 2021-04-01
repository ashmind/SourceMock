namespace SourceMock.Tests
{
    public interface IMockable
    {
        int Int32Return();
        int? NullableInt32Return();
        string StringReturn();
        string? NullableStringReturn();
    }
}
