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

    [SourceMock.Internal.GeneratedMock]
    public static class global__SourceMock_Tests_TestInterfaces_IEmptyInterface_Mock {
        public static MockIEmptyInterface Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> _) => new();
    }
}