#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class DisposableMock : global::SourceMock.Tests.Interfaces.Disposable, IDisposableSetup, IDisposableCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.Disposable> {
        public IDisposableSetup Setup => this;
        public IDisposableCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _disposeHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IDisposableSetup.Dispose() => _disposeHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        public override void Dispose() => _disposeHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IDisposableCalls.Dispose() => _disposeHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    internal interface IDisposableSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Action> Dispose();
    }

    internal interface IDisposableCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Dispose();
    }
}