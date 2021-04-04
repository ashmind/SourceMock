#nullable enable
public static class global__SourceMock_Tests_TestInterfaces_IMockable2_Mock {
    public class Instance : global::SourceMock.Tests.TestInterfaces.IMockable2, ISetup, ICalls {
        public ISetup Setup => this;
        public ICalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler<string> _getString1Handler = new();
        SourceMock.IMockMethodSetup<string> ISetup.GetString() => _getString1Handler.Setup();
        public string GetString() => _getString1Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetString() => _getString1Handler.Calls(_ => SourceMock.NoArguments.Value);

    }


    public interface ISetup {
        SourceMock.IMockMethodSetup<string> GetString();
    }

    public interface ICalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }

    public interface IReturnedSetup : ISetup, SourceMock.IMockMethodSetup<global::SourceMock.Tests.TestInterfaces.IMockable2> {}

    public static global__SourceMock_Tests_TestInterfaces_IMockable2_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IMockable2> _) => new();
}