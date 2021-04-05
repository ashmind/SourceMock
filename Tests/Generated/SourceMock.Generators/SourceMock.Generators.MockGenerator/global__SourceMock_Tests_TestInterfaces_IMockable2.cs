#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public class MockIMockable2 : global::SourceMock.Tests.TestInterfaces.IMockable2, ISetupIMockable2, ICallsIMockable2 {
        public ISetupIMockable2 Setup => this;
        public ICallsIMockable2 Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler<string> _getString1Handler = new();
        SourceMock.IMockMethodSetup<string> ISetupIMockable2.GetString() => _getString1Handler.Setup();
        public string GetString() => _getString1Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable2.GetString() => _getString1Handler.Calls(_ => SourceMock.NoArguments.Value);
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ISetupIMockable2 {
        SourceMock.IMockMethodSetup<string> GetString();
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ICallsIMockable2 {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}