#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class InheritedIntefaceMock : global::SourceMock.Tests.Interfaces.IInheritedInteface, IInheritedIntefaceSetup, IInheritedIntefaceCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IInheritedInteface> {
        public IInheritedIntefaceSetup Setup => this;
        public IInheritedIntefaceCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _method1Handler = new();
        SourceMock.IMockMethodSetup IInheritedIntefaceSetup.Method() => _method1Handler.Setup<SourceMock.Internal.VoidReturn>(null, null);
        public void Method() => _method1Handler.Call<SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IInheritedIntefaceCalls.Method() => _method1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getString2Handler = new();
        SourceMock.IMockMethodSetup<string> IInheritedIntefaceSetup.GetString() => _getString2Handler.Setup<string>(null, null);
        public string GetString() => _getString2Handler.Call<string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IInheritedIntefaceCalls.GetString() => _getString2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface IInheritedIntefaceSetup {
        SourceMock.IMockMethodSetup Method();
        SourceMock.IMockMethodSetup<string> GetString();
    }

    public interface IInheritedIntefaceCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Method();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}