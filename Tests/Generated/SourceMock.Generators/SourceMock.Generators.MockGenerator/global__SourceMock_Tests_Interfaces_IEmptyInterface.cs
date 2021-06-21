#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class EmptyInterfaceMock : global::SourceMock.Tests.Interfaces.IEmptyInterface, IEmptyInterfaceSetup, IEmptyInterfaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IEmptyInterface> {
        public IEmptyInterfaceSetup Setup => this;
        public IEmptyInterfaceCalls Calls => this;
    }

    internal interface IEmptyInterfaceSetup {
    }

    internal interface IEmptyInterfaceCalls {
    }
}