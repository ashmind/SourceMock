namespace SourceMock.Tests
{
    public interface IMockable
    {
        int GetInt32();
        int? GetInt32Nullable();
        string GetString();
        string? GetStringNullable();

        int ParseToInt32(string value);
    }
}
