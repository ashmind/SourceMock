namespace SourceMock.Tests.Interfaces {
    public interface INeedsParameterModifiers {
        int TestIn(in int value);
        int TestRef(ref int value);
        int TestOut(out int value);
    }
}
