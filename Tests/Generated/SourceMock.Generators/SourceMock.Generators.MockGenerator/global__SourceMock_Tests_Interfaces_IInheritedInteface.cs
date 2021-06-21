#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class InheritedIntefaceMock : global::SourceMock.Tests.Interfaces.IInheritedInteface, IInheritedIntefaceSetup, IInheritedIntefaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IInheritedInteface> {
        public IInheritedIntefaceSetup Setup => this;
        public IInheritedIntefaceCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _methodHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IInheritedIntefaceSetup.Method() => _methodHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        public void Method() => _methodHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IInheritedIntefaceCalls.Method() => _methodHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> IInheritedIntefaceSetup.GetString() => _getStringHandler.Setup<System.Func<string>, string>(null, null);
        public string GetString() => _getStringHandler.Call<System.Func<string>, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IInheritedIntefaceCalls.GetString() => _getStringHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    internal interface IInheritedIntefaceSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Action> Method();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> GetString();
    }

    internal interface IInheritedIntefaceCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Method();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}