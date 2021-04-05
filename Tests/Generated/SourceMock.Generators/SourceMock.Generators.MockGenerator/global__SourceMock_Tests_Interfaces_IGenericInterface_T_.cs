#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class GenericInterfaceMock<T> : global::SourceMock.Tests.Interfaces.IGenericInterface<T>, IGenericInterfaceSetup, IGenericInterfaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IGenericInterface<T>> {
        public IGenericInterfaceSetup Setup => this;
        public IGenericInterfaceCalls Calls => this;
    }

    public interface IGenericInterfaceSetup {
    }

    public interface IGenericInterfaceCalls {
    }
}