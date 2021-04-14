#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class AbstractClassMock : global::SourceMock.Tests.Interfaces.AbstractClass, IAbstractClassSetup, IAbstractClassCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.AbstractClass> {
        public IAbstractClassSetup Setup => this;
        public IAbstractClassCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _protected1Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.Protected() => _protected1Handler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        protected override void Protected() => _protected1Handler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.Protected() => _protected1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _protectedInternal2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.ProtectedInternal() => _protectedInternal2Handler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        protected internal override void ProtectedInternal() => _protectedInternal2Handler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.ProtectedInternal() => _protectedInternal2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _privateProtected3Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.PrivateProtected() => _privateProtected3Handler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        private protected override void PrivateProtected() => _privateProtected3Handler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.PrivateProtected() => _privateProtected3Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _get4Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>,int> IAbstractClassSetup.Get() => _get4Handler.Setup<System.Func<int>, int>(null, null);
        public override int Get() => _get4Handler.Call<System.Func<int>, int>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.Get() => _get4Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getString5Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>,string?> IAbstractClassSetup.GetString() => _getString5Handler.Setup<System.Func<string?>, string?>(null, null);
        public override string? GetString() => _getString5Handler.Call<System.Func<string?>, string?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.GetString() => _getString5Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface IAbstractClassSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Action> Protected();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> ProtectedInternal();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> PrivateProtected();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>,int> Get();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>,string?> GetString();
    }

    public interface IAbstractClassCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Protected();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ProtectedInternal();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> PrivateProtected();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
    }
}