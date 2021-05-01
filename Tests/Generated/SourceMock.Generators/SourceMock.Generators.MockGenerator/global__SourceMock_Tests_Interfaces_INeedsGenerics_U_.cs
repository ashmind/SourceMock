#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock<U> : global::SourceMock.Tests.Interfaces.INeedsGenerics<U>, INeedsGenericsSetup<U>, INeedsGenericsCalls<U>, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics<U>> {
        public INeedsGenericsSetup<U> Setup => this;
        public INeedsGenericsCalls<U> Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<U>, U> INeedsGenericsSetup<U>.Get() => _getHandler.Setup<System.Func<U>, U>(null, null);
        public U Get() => _getHandler.Call<System.Func<U>, U>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls<U>.Get() => _getHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _convertHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T> INeedsGenericsSetup<U>.Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _convertHandler.Setup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public T Convert<T>(U value) => _convertHandler.Call<NeedsGenericsDelegates<U>.ConvertFunc<T>, T>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<U> INeedsGenericsCalls<U>.Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _convertHandler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((U)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _castHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.CastFunc<T>, T> INeedsGenericsSetup<U>.Cast<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _castHandler.Setup<NeedsGenericsDelegates<U>.CastFunc<T>, T>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public T Cast<T>(U value)
            where T: U => _castHandler.Call<NeedsGenericsDelegates<U>.CastFunc<T>, T>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<U> INeedsGenericsCalls<U>.Cast<T>(SourceMock.Internal.MockArgumentMatcher<U> value) => _castHandler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((U)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _allConstraintsHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.AllConstraintsAction<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>> INeedsGenericsSetup<U>.AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>() => _allConstraintsHandler.Setup<NeedsGenericsDelegates<U>.AllConstraintsAction<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>, SourceMock.Internal.VoidReturn>(new[] { typeof(TNotNull), typeof(TClass), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null);
        public void AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClass: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable => _allConstraintsHandler.Call<NeedsGenericsDelegates<U>.AllConstraintsAction<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>, SourceMock.Internal.VoidReturn>(new[] { typeof(TNotNull), typeof(TClass), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls<U>.AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>() => _allConstraintsHandler.Calls(new[] { typeof(TNotNull), typeof(TClass), typeof(TNullableClass), typeof(TStruct), typeof(TUnmanaged) }, null, _ => SourceMock.NoArguments.Value);
    }

    public static class NeedsGenericsDelegates<U> {
        public delegate T ConvertFunc<T>(U value);
        public delegate T CastFunc<T>(U value);
        public delegate void AllConstraintsAction<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>();
    }

    public interface INeedsGenericsSetup<U> {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<U>, U> Get();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.ConvertFunc<T>, T> Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default);
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.CastFunc<T>, T> Cast<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default)
            where T: U;
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates<U>.AllConstraintsAction<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>> AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClass: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable;
    }

    public interface INeedsGenericsCalls<U> {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get();
        System.Collections.Generic.IReadOnlyList<U> Convert<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default);
        System.Collections.Generic.IReadOnlyList<U> Cast<T>(SourceMock.Internal.MockArgumentMatcher<U> value = default)
            where T: U;
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> AllConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>()
            where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TClass: class, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
            where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
            where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable;
    }
}