#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public class EmptyInterfaceMock : global::SourceMock.Tests.TestInterfaces.IEmptyInterface, IEmptyInterfaceSetup, IEmptyInterfaceCalls {
        public IEmptyInterfaceSetup Setup => this;
        public IEmptyInterfaceCalls Calls => this;
    }

    [SourceMock.Internal.GeneratedMock]
    public interface IEmptyInterfaceSetup {
    }

    [SourceMock.Internal.GeneratedMock]
    public interface IEmptyInterfaceCalls {
    }
}