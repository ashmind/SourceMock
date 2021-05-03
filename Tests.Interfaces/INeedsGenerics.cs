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
    }
}
