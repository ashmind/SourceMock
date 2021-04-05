#nullable enable
namespace Tests.Interfaces.Mocks {
    public class InternalInterfaceMock : global::Tests.Interfaces.IInternalInterface, IInternalInterfaceSetup, IInternalInterfaceCalls {
        public IInternalInterfaceSetup Setup => this;
        public IInternalInterfaceCalls Calls => this;
    }

    public interface IInternalInterfaceSetup {
    }

    public interface IInternalInterfaceCalls {
    }
}