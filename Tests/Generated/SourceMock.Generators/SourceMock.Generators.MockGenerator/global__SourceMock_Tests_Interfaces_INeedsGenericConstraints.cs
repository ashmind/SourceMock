#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericConstraintsMock : global::SourceMock.Tests.Interfaces.INeedsGenericConstraints, INeedsGenericConstraintsSetup, INeedsGenericConstraintsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenericConstraints> {
        public INeedsGenericConstraintsSetup Setup => this;
        public INeedsGenericConstraintsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _methodHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericConstraintsDelegates.MethodAction<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>> INeedsGenericConstraintsSetup.Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>() => _methodHandler.Setup<NeedsGenericConstraintsDelegates.MethodAction<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>, SourceMock.Internal.VoidReturn>(new[] { typeof(TNotNull), typeof(TClassNotNull), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null);
        public void Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClassNotNull: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable
        => _methodHandler.Call<NeedsGenericConstraintsDelegates.MethodAction<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>, SourceMock.Internal.VoidReturn>(new[] { typeof(TNotNull), typeof(TClassNotNull), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericConstraintsCalls.Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>() => _methodHandler.Calls(new[] { typeof(TNotNull), typeof(TClassNotNull), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null, _ => SourceMock.NoArguments.Value);
    }

    public static class NeedsGenericConstraintsDelegates {
        public delegate void MethodAction<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>();
    }

    public interface INeedsGenericConstraintsSetup {
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericConstraintsDelegates.MethodAction<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>> Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClassNotNull: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable;
    }

    public interface INeedsGenericConstraintsCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Method<TNotNull, TClassNotNull, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClassNotNull: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable;
    }
}