#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class AbstractClassMock : global::SourceMock.Tests.Interfaces.AbstractClass, IAbstractClassSetup, IAbstractClassCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.AbstractClass> {
        public IAbstractClassSetup Setup => this;
        public IAbstractClassCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _protectedHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.Protected() => _protectedHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        protected override void Protected() => _protectedHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.Protected() => _protectedHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _protectedInternalHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.ProtectedInternal() => _protectedInternalHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        protected internal override void ProtectedInternal() => _protectedInternalHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.ProtectedInternal() => _protectedInternalHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _privateProtectedHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IAbstractClassSetup.PrivateProtected() => _privateProtectedHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        private protected override void PrivateProtected() => _privateProtectedHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.PrivateProtected() => _privateProtectedHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>, int> IAbstractClassSetup.Get() => _getHandler.Setup<System.Func<int>, int>(null, null);
        public override int Get() => _getHandler.Call<System.Func<int>, int>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.Get() => _getHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>, string?> IAbstractClassSetup.GetString() => _getStringHandler.Setup<System.Func<string?>, string?>(null, null);
        public override string? GetString() => _getStringHandler.Call<System.Func<string?>, string?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IAbstractClassCalls.GetString() => _getStringHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockPropertyHandler<int> _virtualPropertyHandler = new(false);
        SourceMock.Interfaces.IMockPropertySetup<int> IAbstractClassSetup.VirtualProperty => _virtualPropertyHandler.Setup();
        public override int VirtualProperty => _virtualPropertyHandler.GetterHandler.Call<System.Func<int>, int>(null, null);
        SourceMock.Interfaces.IMockPropertyCalls<int> IAbstractClassCalls.VirtualProperty => _virtualPropertyHandler.Calls();
    }

    internal interface IAbstractClassSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Action> Protected();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> ProtectedInternal();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> PrivateProtected();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>, int> Get();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>, string?> GetString();
        SourceMock.Interfaces.IMockPropertySetup<int> VirtualProperty { get; }
    }

    internal interface IAbstractClassCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Protected();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ProtectedInternal();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> PrivateProtected();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
        SourceMock.Interfaces.IMockPropertyCalls<int> VirtualProperty { get; }
    }
}