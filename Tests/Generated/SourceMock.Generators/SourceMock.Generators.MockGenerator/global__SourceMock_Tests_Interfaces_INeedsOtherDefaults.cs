#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsOtherDefaultsMock : global::SourceMock.Tests.Interfaces.INeedsOtherDefaults, INeedsOtherDefaultsSetup, INeedsOtherDefaultsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsOtherDefaults> {
        public INeedsOtherDefaultsSetup Setup => this;
        public INeedsOtherDefaultsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _executeAsync1Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task>,global::System.Threading.Tasks.Task> INeedsOtherDefaultsSetup.ExecuteAsync() => _executeAsync1Handler.Setup<System.Func<System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task>(null, null);
        public global::System.Threading.Tasks.Task ExecuteAsync() => _executeAsync1Handler.Call<System.Func<System.Threading.Tasks.Task>, global::System.Threading.Tasks.Task>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.ExecuteAsync() => _executeAsync1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringAsync2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<object>>,global::System.Threading.Tasks.Task<object>> INeedsOtherDefaultsSetup.GetStringAsync() => _getStringAsync2Handler.Setup<System.Func<System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>>(null, null);
        public global::System.Threading.Tasks.Task<object> GetStringAsync() => _getStringAsync2Handler.Call<System.Func<System.Threading.Tasks.Task<object>>, global::System.Threading.Tasks.Task<object>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetStringAsync() => _getStringAsync2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getListAsync3Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<System.Collections.Generic.IList<int>>>,global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> INeedsOtherDefaultsSetup.GetListAsync() => _getListAsync3Handler.Setup<System.Func<System.Threading.Tasks.Task<System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>(null, null);
        public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>> GetListAsync() => _getListAsync3Handler.Call<System.Func<System.Threading.Tasks.Task<System.Collections.Generic.IList<int>>>, global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsOtherDefaultsCalls.GetListAsync() => _getListAsync3Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsOtherDefaultsSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task>,global::System.Threading.Tasks.Task> ExecuteAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<object>>,global::System.Threading.Tasks.Task<object>> GetStringAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<System.Collections.Generic.IList<int>>>,global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<int>>> GetListAsync();
    }

    public interface INeedsOtherDefaultsCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ExecuteAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringAsync();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetListAsync();
    }
}