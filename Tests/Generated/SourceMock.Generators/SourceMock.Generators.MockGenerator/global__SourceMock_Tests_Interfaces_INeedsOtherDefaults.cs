#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsOtherDefaultsMock : global::SourceMock.Tests.Interfaces.INeedsOtherDefaults, INeedsOtherDefaultsSetup, INeedsOtherDefaultsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsOtherDefaults> {
        public INeedsOtherDefaultsSetup Setup => this;
        public INeedsOtherDefaultsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _executeAsync1Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task> INeedsOtherDefaultsSetup.ExecuteAsync() => _executeAsync1Handler.Setup<global::System.Threading.Tasks.Task>();
        public global::System.Threading.Tasks.Task ExecuteAsync() => _executeAsync1Handler.Call<global::System.Threading.Tasks.Task>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.ExecuteAsync() => _executeAsync1Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringAsync2Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<object>> INeedsOtherDefaultsSetup.GetStringAsync() => _getStringAsync2Handler.Setup<global::System.Threading.Tasks.Task<object>>();
        public global::System.Threading.Tasks.Task<object> GetStringAsync() => _getStringAsync2Handler.Call<global::System.Threading.Tasks.Task<object>>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetStringAsync() => _getStringAsync2Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getListAsync3Handler = new();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> INeedsOtherDefaultsSetup.GetListAsync() => _getListAsync3Handler.Setup<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>();
        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>> GetListAsync() => _getListAsync3Handler.Call<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetListAsync() => _getListAsync3Handler.Calls(_ => SourceMock.NoArguments.Value);
    }

    public interface INeedsOtherDefaultsSetup {
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task> ExecuteAsync();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<object>> GetStringAsync();
        SourceMock.IMockMethodSetup<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> GetListAsync();
    }

    public interface INeedsOtherDefaultsCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ExecuteAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetListAsync();
    }
}