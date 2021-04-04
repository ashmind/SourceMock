#nullable enable
public static class global__SourceMock_Tests_TestInterfaces_INeedsOtherDefaults_Mock {
    public class Instance : global::SourceMock.Tests.TestInterfaces.INeedsOtherDefaults, ISetup, ICalls {
        public ISetup Setup => this;
        public ICalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler<global::System.Threading.Tasks.Task> _executeAsync1Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task> ISetup.ExecuteAsync() => _executeAsync1Handler.Setup();
        public global::System.Threading.Tasks.Task ExecuteAsync() => _executeAsync1Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.ExecuteAsync() => _executeAsync1Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler<global::System.Threading.Tasks.Task<object>> _getStringAsync2Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<object>> ISetup.GetStringAsync() => _getStringAsync2Handler.Setup();
        public global::System.Threading.Tasks.Task<object> GetStringAsync() => _getStringAsync2Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetStringAsync() => _getStringAsync2Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> _getListAsync3Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> ISetup.GetListAsync() => _getListAsync3Handler.Setup();
        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>> GetListAsync() => _getListAsync3Handler.Call();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetListAsync() => _getListAsync3Handler.Calls(_ => SourceMock.NoArguments.Value);

    }


    public interface ISetup {
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task> ExecuteAsync();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<object>> GetStringAsync();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> GetListAsync();
    }

    public interface ICalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ExecuteAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetListAsync();
    }

    public interface IReturnedSetup : ISetup, SourceMock.IMockMethodSetup<global::SourceMock.Tests.TestInterfaces.INeedsOtherDefaults> {}

    public static global__SourceMock_Tests_TestInterfaces_INeedsOtherDefaults_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.INeedsOtherDefaults> _) => new();
}