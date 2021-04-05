#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class InternalInterfaceMock : global::SourceMock.Tests.Interfaces.IInternalInterface, IInternalInterfaceSetup, IInternalInterfaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IInternalInterface> {
        public IInternalInterfaceSetup Setup => this;
        public IInternalInterfaceCalls Calls => this;
    }

    public interface IInternalInterfaceSetup {
    }

    public interface IInternalInterfaceCalls {
    }
}