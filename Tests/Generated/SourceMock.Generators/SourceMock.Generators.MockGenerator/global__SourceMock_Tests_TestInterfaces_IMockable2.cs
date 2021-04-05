#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public class Mockable2Mock : global::SourceMock.Tests.TestInterfaces.IMockable2, IMockable2Setup, IMockable2Calls {
        public IMockable2Setup Setup => this;
        public IMockable2Calls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler<string> _getString1Handler = new();
        SourceMock.IMockMethodSetup<string> IMockable2Setup.GetString() => _getString1Handler.Setup();
        public string GetString() => _getString1Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockable2Calls.GetString() => _getString1Handler.Calls(_ => SourceMock.NoArguments.Value);
    }

    [SourceMock.Internal.GeneratedMock]
    public interface IMockable2Setup {
        SourceMock.IMockMethodSetup<string> GetString();
    }

    [SourceMock.Internal.GeneratedMock]
    public interface IMockable2Calls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}