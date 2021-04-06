﻿#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericsMock : global::SourceMock.Tests.Interfaces.INeedsGenerics, INeedsGenericsSetup, INeedsGenericsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenerics> {
        public INeedsGenericsSetup Setup => this;
        public INeedsGenericsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _parse1Handler = new();

        SourceMock.IMockMethodSetup<System.Action<string>,T> INeedsGenericsSetup.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Setup<System.Action<string>, T>(new[] { typeof(T)}, new SourceMock.Internal.IMockArgumentMatcher[] { value});
        public T Parse<T>(string value) => _parse1Handler.Call<System.Action<string>, T>(new[] { typeof(T)}, new object?[] { value});
        System.Collections.Generic.IReadOnlyList<string> INeedsGenericsCalls.Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value) => _parse1Handler.Calls(new[] { typeof(T)}, new SourceMock.Internal.IMockArgumentMatcher[] { value}, args => ((string)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _get2Handler = new();

        SourceMock.IMockMethodSetup<System.Action,T> INeedsGenericsSetup.Get<T>() => _get2Handler.Setup<System.Action, T>(new[] { typeof(T)}, null);
        public T Get<T>() => _get2Handler.Call<System.Action, T>(new[] { typeof(T)}, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsGenericsCalls.Get<T>() => _get2Handler.Calls(new[] { typeof(T)}, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsGenericsSetup {
        SourceMock.IMockMethodSetup<System.Action<string>,T> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        SourceMock.IMockMethodSetup<System.Action,T> Get<T>();
    }

    public interface INeedsGenericsCalls {
        System.Collections.Generic.IReadOnlyList<string> Parse<T>(SourceMock.Internal.MockArgumentMatcher<string> value = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Get<T>();
    }
}