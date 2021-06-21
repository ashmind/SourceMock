#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class NeedsParameterModifiersMock : global::SourceMock.Tests.Interfaces.INeedsParameterModifiers, INeedsParameterModifiersSetup, INeedsParameterModifiersCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsParameterModifiers> {
        public INeedsParameterModifiersSetup Setup => this;
        public INeedsParameterModifiersCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _testInHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestInFunc, int> INeedsParameterModifiersSetup.TestIn(SourceMock.Internal.MockArgumentMatcher<int> value) => _testInHandler.Setup<NeedsParameterModifiersDelegates.TestInFunc, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public int TestIn(in int value) => _testInHandler.Call<NeedsParameterModifiersDelegates.TestInFunc, int>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestIn(SourceMock.Internal.MockArgumentMatcher<int> value) => _testInHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((int)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _testRefHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestRefFunc, int> INeedsParameterModifiersSetup.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRefHandler.Setup<NeedsParameterModifiersDelegates.TestRefFunc, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public int TestRef(ref int value) => _testRefHandler.Call<NeedsParameterModifiersDelegates.TestRefFunc, int>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRefHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((int)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _testOutHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestOutFunc, int> INeedsParameterModifiersSetup.TestOut(SourceMock.Internal.MockArgumentMatcher<int> value) => _testOutHandler.Setup<NeedsParameterModifiersDelegates.TestOutFunc, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public int TestOut(out int value) {
            var arguments = new object?[] { default(int) };
            var result = _testOutHandler.Call<NeedsParameterModifiersDelegates.TestOutFunc, int>(null, arguments);
            value = (int)arguments[0]!;
            return result;
        }
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestOut(SourceMock.Internal.MockArgumentMatcher<int> value) => _testOutHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((int)args[0]!));
    }

    internal static class NeedsParameterModifiersDelegates {
        public delegate int TestInFunc(in int value);
        public delegate int TestRefFunc(ref int value);
        public delegate int TestOutFunc(out int value);
    }

    internal interface INeedsParameterModifiersSetup {
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestInFunc, int> TestIn(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestRefFunc, int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        SourceMock.Interfaces.IMockMethodSetup<NeedsParameterModifiersDelegates.TestOutFunc, int> TestOut(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }

    internal interface INeedsParameterModifiersCalls {
        System.Collections.Generic.IReadOnlyList<int> TestIn(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        System.Collections.Generic.IReadOnlyList<int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        System.Collections.Generic.IReadOnlyList<int> TestOut(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }
}