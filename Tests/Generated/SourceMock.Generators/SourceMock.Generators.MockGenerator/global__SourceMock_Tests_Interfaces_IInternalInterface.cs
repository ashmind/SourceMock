#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class InternalInterfaceMock : global::SourceMock.Tests.Interfaces.IInternalInterface, IInternalInterfaceSetup, IInternalInterfaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IInternalInterface> {
        public IInternalInterfaceSetup Setup => this;
        public IInternalInterfaceCalls Calls => this;
    }

    internal interface IInternalInterfaceSetup {
    }

    internal interface IInternalInterfaceCalls {
    }
}