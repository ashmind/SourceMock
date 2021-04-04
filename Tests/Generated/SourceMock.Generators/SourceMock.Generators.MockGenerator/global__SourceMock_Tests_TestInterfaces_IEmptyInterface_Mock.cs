#nullable enable
public static class global__SourceMock_Tests_TestInterfaces_IEmptyInterface_Mock {
    public class Instance : global::SourceMock.Tests.TestInterfaces.IEmptyInterface, ISetup, ICalls {
        public ISetup Setup => this;
        public ICalls Calls => this;

    }


    public interface ISetup {
    }

    public interface ICalls {
    }

    public interface IReturnedSetup : ISetup, SourceMock.IMockMethodSetup<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> {}

    public static global__SourceMock_Tests_TestInterfaces_IEmptyInterface_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> _) => new();
}