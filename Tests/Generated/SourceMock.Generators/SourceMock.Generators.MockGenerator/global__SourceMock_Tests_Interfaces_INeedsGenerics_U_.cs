#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock<U> : global::SourceMock.Tests.Interfaces.INeedsGenerics<U>, INeedsGenericsSetup<U>, INeedsGenericsCalls<U>, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics<U>> {
        public INeedsGenericsSetup<U> Setup => this;
        public INeedsGenericsCalls<U> Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<U>, U> INeedsGenericsSetup<U>.Get() => _getHandler.Setup<System.Func<U>, U>(null, null);
        public U Get() => _getHandler.Call<System.Func<U>, U>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls<U>.Get() => _getHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _convertHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T> INeedsGenericsSetup<U>.Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _convertHandler.Setup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public T Convert<T>(U value) => _convertHandler.Call<NeedsGenericsDelegates<U>.ConvertFunc<T>, T>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<U> INeedsGenericsCalls<U>.Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _convertHandler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((U)args[0]!));
    }

    public static class NeedsGenericsDelegates<U> {
        public delegate T ConvertFunc<T>(U value);
    }

    public interface INeedsGenericsSetup<U> {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<U>, U> Get();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T> Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default);
    }

    public interface INeedsGenericsCalls<U> {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get();
        System.Collections.Generic.IReadOnlyList<U> Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default);
    }
}