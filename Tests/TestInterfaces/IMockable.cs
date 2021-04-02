using System;

namespace SourceMock.Tests.TestInterfaces {
    public interface IMockable {
        int GetInt32();
        int? GetInt32Nullable();
        string GetString();
        string? GetStringNullable();

        int ParseToInt32(string? value);
        bool TestInterface(IEmptyInterface value);

        int Sum(int value1, int value2);
    }
}
