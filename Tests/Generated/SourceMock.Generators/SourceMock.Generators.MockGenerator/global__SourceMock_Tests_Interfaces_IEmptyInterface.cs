#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class EmptyInterfaceMock : global::SourceMock.Tests.Interfaces.IEmptyInterface, IEmptyInterfaceSetup, IEmptyInterfaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IEmptyInterface> {
        public IEmptyInterfaceSetup Setup => this;
        public IEmptyInterfaceCalls Calls => this;
    }

    public interface IEmptyInterfaceSetup {
    }

    public interface IEmptyInterfaceCalls {
    }
}