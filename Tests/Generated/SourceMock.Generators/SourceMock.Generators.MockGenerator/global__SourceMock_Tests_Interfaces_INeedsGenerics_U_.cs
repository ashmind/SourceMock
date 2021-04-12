#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock<U> : global::SourceMock.Tests.Interfaces.INeedsGenerics<U>, INeedsGenericsSetup<U>, INeedsGenericsCalls<U>, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics<U>> {
        public INeedsGenericsSetup<U> Setup => this;
        public INeedsGenericsCalls<U> Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _get1Handler = new();
        SourceMock.IMockMethodSetup<System.Func<U>,U> INeedsGenericsSetup<U>.Get() => _get1Handler.Setup<System.Func<U>, U>(null, null);
        public U Get() => _get1Handler.Call<System.Func<U>, U>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls<U>.Get() => _get1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsGenericsSetup<U> {
        SourceMock.IMockMethodSetup<System.Func<U>,U> Get();
    }

    public interface INeedsGenericsCalls<U> {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get();
    }
}