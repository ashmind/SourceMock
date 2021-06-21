#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class NeedsGenericsMock : global::SourceMock.Tests.Interfaces.INeedsGenerics, INeedsGenericsSetup, INeedsGenericsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics> {
        public INeedsGenericsSetup Setup => this;
        public INeedsGenericsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _parseHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.ParseFunc<T>, T> INeedsGenericsSetup.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parseHandler.Setup<NeedsGenericsDelegates.ParseFunc<T>, T>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public T Parse<T>(string value) => _parseHandler.Call<NeedsGenericsDelegates.ParseFunc<T>, T>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<string> INeedsGenericsCalls.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parseHandler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((string)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _getHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.GetFunc<T>, T> INeedsGenericsSetup.Get<T>() => _getHandler.Setup<NeedsGenericsDelegates.GetFunc<T>, T>(new[] { typeof(T) }, null);
        public T Get<T>() => _getHandler.Call<NeedsGenericsDelegates.GetFunc<T>, T>(new[] { typeof(T) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls.Get<T>() => _getHandler.Calls(new[] { typeof(T) }, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _setOptionalHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.SetOptionalAction<T>> INeedsGenericsSetup.SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value) where T: default => _setOptionalHandler.Setup<NeedsGenericsDelegates.SetOptionalAction<T>, SourceMock.Internal.VoidReturn>(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public void SetOptional<T>(T? value) => _setOptionalHandler.Call<NeedsGenericsDelegates.SetOptionalAction<T>, SourceMock.Internal.VoidReturn>(new[] { typeof(T) }, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<T?> INeedsGenericsCalls.SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value) where T: default => _setOptionalHandler.Calls(new[] { typeof(T) }, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((T?)args[0]));

        private readonly SourceMock.Internal.MockMethodHandler _getAllHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.GetAllFunc<T>, global::System.Collections.Generic.IEnumerable<T?>> INeedsGenericsSetup.GetAll<T>() where T: default => _getAllHandler.Setup<NeedsGenericsDelegates.GetAllFunc<T>, global::System.Collections.Generic.IEnumerable<T?>>(new[] { typeof(T) }, null);
        public global::System.Collections.Generic.IEnumerable<T?> GetAll<T>() => _getAllHandler.Call<NeedsGenericsDelegates.GetAllFunc<T>, global::System.Collections.Generic.IEnumerable<T?>>(new[] { typeof(T) }, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls.GetAll<T>() where T: default => _getAllHandler.Calls(new[] { typeof(T) }, null, _ => SourceMock.NoArguments.Value);
    }

    internal static class NeedsGenericsDelegates {
        public delegate T ParseFunc<T>(string value);
        public delegate T GetFunc<T>();
        public delegate void SetOptionalAction<T>(T? value);
        public delegate global::System.Collections.Generic.IEnumerable<T?> GetAllFunc<T>();
    }

    internal interface INeedsGenericsSetup {
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.ParseFunc<T>, T> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.GetFunc<T>, T> Get<T>();
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.SetOptionalAction<T>> SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value = default);
        SourceMock.Interfaces.IMockMethodSetup<NeedsGenericsDelegates.GetAllFunc<T>, global::System.Collections.Generic.IEnumerable<T?>> GetAll<T>();
    }

    internal interface INeedsGenericsCalls {
        System.Collections.Generic.IReadOnlyList<string> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get<T>();
        System.Collections.Generic.IReadOnlyList<T?> SetOptional<T>(SourceMock.Internal.MockArgumentMatcher<T?> value = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetAll<T>();
    }
}