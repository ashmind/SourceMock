#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public class MockIEmptyInterface : global::SourceMock.Tests.TestInterfaces.IEmptyInterface, ISetupIEmptyInterface, ICallsIEmptyInterface {
        public ISetupIEmptyInterface Setup => this;
        public ICallsIEmptyInterface Calls => this;
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ISetupIEmptyInterface {
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ICallsIEmptyInterface {
    }
}