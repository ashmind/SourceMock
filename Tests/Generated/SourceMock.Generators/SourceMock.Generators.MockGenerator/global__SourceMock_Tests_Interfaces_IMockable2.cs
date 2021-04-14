#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class Mockable2Mock : global::SourceMock.Tests.Interfaces.IMockable2, IMockable2Setup, IMockable2Calls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable2> {
        public IMockable2Setup Setup => this;
        public IMockable2Calls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getString1Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>,string> IMockable2Setup.GetString() => _getString1Handler.Setup<System.Func<string>, string>(null, null);
        public string GetString() => _getString1Handler.Call<System.Func<string>, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockable2Calls.GetString() => _getString1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface IMockable2Setup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>,string> GetString();
    }

    public interface IMockable2Calls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}