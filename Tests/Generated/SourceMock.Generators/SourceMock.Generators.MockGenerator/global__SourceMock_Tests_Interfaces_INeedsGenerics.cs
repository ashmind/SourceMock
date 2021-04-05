#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock : global::SourceMock.Tests.Interfaces.INeedsGenerics, INeedsGenericsSetup, INeedsGenericsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics> {
        public INeedsGenericsSetup Setup => this;
        public INeedsGenericsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _parse1Handler = new();
        SourceMock.IMockMethodSetup<T> INeedsGenericsSetup.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Setup<T>(value);
        public T Parse<T>(string value) => _parse1Handler.Call<T>(value);
        System.Collections.Generic.IReadOnlyList<string> INeedsGenericsCalls.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Calls(args => ((string)args[0]!), value);
    }

    public interface INeedsGenericsSetup {
        SourceMock.IMockMethodSetup<T> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
    }

    public interface INeedsGenericsCalls {
        System.Collections.Generic.IReadOnlyList<string> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
    }
}