#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public class NeedsParameterModifiersMock : global::SourceMock.Tests.TestInterfaces.INeedsParameterModifiers, INeedsParameterModifiersSetup, INeedsParameterModifiersCalls {
        public INeedsParameterModifiersSetup Setup => this;
        public INeedsParameterModifiersCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler<int> _testRef1Handler = new();
        SourceMock.IMockMethodSetup<int> INeedsParameterModifiersSetup.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRef1Handler.Setup(value);
        public int TestRef(ref int value) => _testRef1Handler.Call(value);
        System.Collections.Generic.IReadOnlyList<int> INeedsParameterModifiersCalls.TestRef(SourceMock.Internal.MockArgumentMatcher<int> value) => _testRef1Handler.Calls(args => ((int)args[0]!), value);
    }

    [SourceMock.Internal.GeneratedMock]
    public interface INeedsParameterModifiersSetup {
        SourceMock.IMockMethodSetup<int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }

    [SourceMock.Internal.GeneratedMock]
    public interface INeedsParameterModifiersCalls {
        System.Collections.Generic.IReadOnlyList<int> TestRef(SourceMock.Internal.MockArgumentMatcher<int> value = default);
    }
}