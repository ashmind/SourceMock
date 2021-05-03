namespace SourceMock.Tests.Interfaces {
    public interface INeedsGenericConstraints {
        void Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull : notnull, IMockable, new()
            where TClassNotNull : class, IMockable, new()
            where TNullableClass : class?, IMockable, new()
            where TStruct : struct, IMockable
            where TUnmanaged : unmanaged, IMockable;
    }

    public interface INeedsGenericConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>
        where TNotNull : notnull, IMockable, new()
        where TClass : class, IMockable, new()
        where TNullableClass : class?, IMockable, new()
        where TStruct : struct, IMockable
        where TUnmanaged : unmanaged, IMockable
    {
    }
}
