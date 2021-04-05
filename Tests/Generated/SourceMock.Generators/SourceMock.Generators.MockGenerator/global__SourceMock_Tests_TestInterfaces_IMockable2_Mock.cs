#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public static class global__SourceMock_Tests_TestInterfaces_IMockable2_Mock {
        public class Instance : global::SourceMock.Tests.TestInterfaces.IMockable2, ISetupIMockable2, ICalls {
            public ISetupIMockable2 Setup => this;
            public ICalls Calls => this;

            private readonly SourceMock.Internal.MockMethodHandler<string> _getString1Handler = new();
            SourceMock.IMockMethodSetup<string> ISetupIMockable2.GetString() => _getString1Handler.Setup();
            public string GetString() => _getString1Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetString() => _getString1Handler.Calls(_ => SourceMock.NoArguments.Value);

        }


        public interface ICalls {
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
        }

        public static global__SourceMock_Tests_TestInterfaces_IMockable2_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IMockable2> _) => new();
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ISetupIMockable2 {
        SourceMock.IMockMethodSetup<string> GetString();
    }
}