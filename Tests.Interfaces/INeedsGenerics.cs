using System.Collections.Generic;

namespace SourceMock.Tests.Interfaces {
    public interface INeedsGenerics {
        T Parse<T>(string value);
        T Get<T>();
        void SetOptional<T>(T? value);
        IEnumerable<T?> GetAll<T>();
    }

    public interface INeedsGenerics<U> {
        U Get();
        T Convert<T>(U value);
        T Cast<T>(U value)
            where T: U;

        void AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, IMockable, new()
            where TClass : class, IMockable, new()
            where TNullableClass : class?, IMockable, new()
            where TStruct : struct, IMockable
            where TUnmanaged : unmanaged, IMockable;
    }
}
