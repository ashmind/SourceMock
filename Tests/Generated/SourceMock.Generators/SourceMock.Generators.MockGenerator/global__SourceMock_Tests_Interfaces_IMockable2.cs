#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class Mockable2Mock : global::SourceMock.Tests.Interfaces.IMockable2, IMockable2Setup, IMockable2Calls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable2> {
        public IMockable2Setup Setup => this;
        public IMockable2Calls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getStringHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> IMockable2Setup.GetString() => _getStringHandler.Setup<System.Func<string>, string>(null, null);
        public string GetString() => _getStringHandler.Call<System.Func<string>, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockable2Calls.GetString() => _getStringHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    internal interface IMockable2Setup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> GetString();
    }

    internal interface IMockable2Calls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}