#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsParameterModifiersMock : global::SourceMock.Tests.Interfaces.INeedsParameterModifiers, INeedsParameterModifiersSetup, INeedsParameterModifiersCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsParameterModifiers> {
        public INeedsParameterModifiersSetup Setup => this;
        public INeedsParameterModifiersCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _testIn1Handler = new();
        SourceMock.IMockMethodSetup<int> INeedsParameterModifiersSetup.TestIn(SourceMock.Internal.MockArgumentMatcher<int> value) => _testIn1Handler.Setup<int>(value);
        public int TestIn(in int value) => _testIn1Handler.Call<int>(value);
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestIn(SourceMock.Internal.MockArgumentMatcher<int> value) => _testIn1Handler.Calls(args => ((int)args[0]!), value);

        private readonly SourceMock.Internal.MockMethodHandler _testRef2Handler = new();
        SourceMock.IMockMethodSetup<int> INeedsParameterModifiersSetup.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRef2Handler.Setup<int>(value);
        public int TestRef(ref int value) => _testRef2Handler.Call<int>(value);
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRef2Handler.Calls(args => ((int)args[0]!), value);

        private readonly SourceMock.Internal.MockMethodHandler _testOut3Handler = new();
        SourceMock.IMockMethodSetup<int> INeedsParameterModifiersSetup.TestOut(SourceMock.Internal.MockArgumentMatcher<int> value) => _testOut3Handler.Setup<int>(value);
        public int TestOut(out int value) {
            value = default;
            return _testOut3Handler.Call<int>(value);
        }
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestOut(SourceMock.Internal.MockArgumentMatcher<int> value) => _testOut3Handler.Calls(args => ((int)args[0]!), value);
    }

    public interface INeedsParameterModifiersSetup {
        SourceMock.IMockMethodSetup<int> TestIn(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        SourceMock.IMockMethodSetup<int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        SourceMock.IMockMethodSetup<int> TestOut(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }

    public interface INeedsParameterModifiersCalls {
        System.Collections.Generic.IReadOnlyList<int> TestIn(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        System.Collections.Generic.IReadOnlyList<int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
        System.Collections.Generic.IReadOnlyList<int> TestOut(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }
}