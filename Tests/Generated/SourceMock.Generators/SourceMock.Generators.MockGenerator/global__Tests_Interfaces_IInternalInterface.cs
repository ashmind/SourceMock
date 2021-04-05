#nullable enable
namespace Tests.Interfaces.Mocks {
    public class InternalInterfaceMock : global::Tests.Interfaces.IInternalInterface, IInternalInterfaceSetup, IInternalInterfaceCalls, SourceMock.IMock<global::Tests.Interfaces.IInternalInterface> {
        public IInternalInterfaceSetup Setup => this;
        public IInternalInterfaceCalls Calls => this;
    }

    public interface IInternalInterfaceSetup {
    }

    public interface IInternalInterfaceCalls {
    }
}