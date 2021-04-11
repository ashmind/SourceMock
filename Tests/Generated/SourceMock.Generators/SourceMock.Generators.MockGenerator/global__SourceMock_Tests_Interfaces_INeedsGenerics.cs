#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock : global::SourceMock.Tests.Interfaces.INeedsGenerics, INeedsGenericsSetup, INeedsGenericsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics> {
        public INeedsGenericsSetup Setup => this;
        public INeedsGenericsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _parse1Handler = new();
        SourceMock.IMockMethodSetup<System.Func<string,T>,T> INeedsGenericsSetup.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Setup<System.Func<string,T>, T>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public T Parse<T>(string value) => _parse1Handler.Call<System.Func<string,T>, T>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<string> INeedsGenericsCalls.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((string)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _get2Handler = new();
        SourceMock.IMockMethodSetup<System.Func<T>,T> INeedsGenericsSetup.Get<T>() => _get2Handler.Setup<System.Func<T>, T>(new[] { typeof(T) }, null);
        public T Get<T>() => _get2Handler.Call<System.Func<T>, T>(new[] { typeof(T) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls.Get<T>() => _get2Handler.Calls(new[] { typeof(T) }, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _setOptional3Handler = new();
        SourceMock.IMockMethodSetup<System.Action<T?>> INeedsGenericsSetup.SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value) where T: default => _setOptional3Handler.Setup<System.Action<T?>, SourceMock.Internal.VoidReturn>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public void SetOptional<T>(T? value) => _setOptional3Handler.Call<System.Action<T?>, SourceMock.Internal.VoidReturn>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<T?> INeedsGenericsCalls.SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value) where T: default => _setOptional3Handler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((T?)args[0]));

        private readonly SourceMock.Internal.MockMethodHandler _getAll4Handler = new();
        SourceMock.IMockMethodSetup<System.Func<System.Collections.Generic.IEnumerable<T?>>,global::System.Collections.Generic.IEnumerable<T?>> INeedsGenericsSetup.GetAll<T>() where T: default => _getAll4Handler.Setup<System.Func<System.Collections.Generic.IEnumerable<T?>>, global::System.Collections.Generic.IEnumerable<T?>>(new[] { typeof(T) }, null);
        public global::System.Collections.Generic.IEnumerable<T?> GetAll<T>() => _getAll4Handler.Call<System.Func<System.Collections.Generic.IEnumerable<T?>>, global::System.Collections.Generic.IEnumerable<T?>>(new[] { typeof(T) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls.GetAll<T>() where T: default => _getAll4Handler.Calls(new[] { typeof(T) }, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsGenericsSetup {
        SourceMock.IMockMethodSetup<System.Func<string,T>,T> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        SourceMock.IMockMethodSetup<System.Func<T>,T> Get<T>();
        SourceMock.IMockMethodSetup<System.Action<T?>> SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value = default);
        SourceMock.IMockMethodSetup<System.Func<System.Collections.Generic.IEnumerable<T?>>,global::System.Collections.Generic.IEnumerable<T?>> GetAll<T>();
    }

    public interface INeedsGenericsCalls {
        System.Collections.Generic.IReadOnlyList<string> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get<T>();
        System.Collections.Generic.IReadOnlyList<T?> SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetAll<T>();
    }
}