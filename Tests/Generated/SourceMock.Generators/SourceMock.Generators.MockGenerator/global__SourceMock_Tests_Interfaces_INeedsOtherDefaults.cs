#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsOtherDefaultsMock : global::SourceMock.Tests.Interfaces.INeedsOtherDefaults, INeedsOtherDefaultsSetup, INeedsOtherDefaultsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsOtherDefaults> {
        public INeedsOtherDefaultsSetup Setup => this;
        public INeedsOtherDefaultsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _executeAsyncHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task> INeedsOtherDefaultsSetup.ExecuteAsync() => _executeAsyncHandler.Setup<System.Func<global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task>(null, null);
        public global::System.Threading.Tasks.Task ExecuteAsync() => _executeAsyncHandler.Call<System.Func<global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.ExecuteAsync() => _executeAsyncHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringAsyncHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>> INeedsOtherDefaultsSetup.GetStringAsync() => _getStringAsyncHandler.Setup<System.Func<global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>>(null, null);
        public global::System.Threading.Tasks.Task<object> GetStringAsync() => _getStringAsyncHandler.Call<System.Func<global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetStringAsync() => _getStringAsyncHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getListAsyncHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> INeedsOtherDefaultsSetup.GetListAsync() => _getListAsyncHandler.Setup<System.Func<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>(null, null);
        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>> GetListAsync() => _getListAsyncHandler.Call<System.Func<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetListAsync() => _getListAsyncHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsOtherDefaultsSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task> ExecuteAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>> GetStringAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> GetListAsync();
    }

    public interface INeedsOtherDefaultsCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ExecuteAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetListAsync();
    }
}