#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public static class global__SourceMock_Tests_TestInterfaces_IEmptyInterface_Mock {
        public class Instance : global::SourceMock.Tests.TestInterfaces.IEmptyInterface, ISetupIEmptyInterface, ICalls {
            public ISetupIEmptyInterface Setup => this;
            public ICalls Calls => this;

        }


        public interface ICalls {
        }

        public static global__SourceMock_Tests_TestInterfaces_IEmptyInterface_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> _) => new();
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ISetupIEmptyInterface {
    }
}