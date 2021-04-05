#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class MockableMock : global::SourceMock.Tests.Interfaces.IMockable, IMockableSetup, IMockableCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable> {
        public IMockableSetup Setup => this;
        public IMockableCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getInt321Handler = new();
        SourceMock.IMockMethodSetup<int> IMockableSetup.GetInt32() => _getInt321Handler.Setup<int>();
        public int GetInt32() => _getInt321Handler.Call<int>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32() => _getInt321Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getInt32Nullable2Handler = new();
        SourceMock.IMockMethodSetup<int?> IMockableSetup.GetInt32Nullable() => _getInt32Nullable2Handler.Setup<int?>();
        public int? GetInt32Nullable() => _getInt32Nullable2Handler.Call<int?>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32Nullable() => _getInt32Nullable2Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getString3Handler = new();
        SourceMock.IMockMethodSetup<string> IMockableSetup.GetString() => _getString3Handler.Setup<string>();
        public string GetString() => _getString3Handler.Call<string>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetString() => _getString3Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringNullable4Handler = new();
        SourceMock.IMockMethodSetup<string?> IMockableSetup.GetStringNullable() => _getStringNullable4Handler.Setup<string?>();
        public string? GetStringNullable() => _getStringNullable4Handler.Call<string?>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringNullable() => _getStringNullable4Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getMockable25Handler = new();
        SourceMock.IMockMethodSetup<global::SourceMock.Tests.Interfaces.IMockable2> IMockableSetup.GetMockable2() => _getMockable25Handler.Setup<global::SourceMock.Tests.Interfaces.IMockable2>();
        public global::SourceMock.Tests.Interfaces.IMockable2 GetMockable2() => _getMockable25Handler.Call<global::SourceMock.Tests.Interfaces.IMockable2>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetMockable2() => _getMockable25Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _parseToInt326Handler = new();
        SourceMock.IMockMethodSetup<int> IMockableSetup.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Setup<int>(value);
        public int ParseToInt32(string? value) => _parseToInt326Handler.Call<int>(value);
        System.Collections.Generic.IReadOnlyList<string?> IMockableCalls.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Calls(args => ((string?)args[0]), value);

        private readonly SourceMock.Internal.MockMethodHandler _testInterface7Handler = new();
        SourceMock.IMockMethodSetup<bool> IMockableSetup.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface7Handler.Setup<bool>(value);
        public bool TestInterface(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _testInterface7Handler.Call<bool>(value);
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface7Handler.Calls(args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!), value);

        private readonly SourceMock.Internal.MockMethodHandler _divide8Handler = new();
        SourceMock.IMockMethodSetup<double> IMockableSetup.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Setup<double>(value1, value2);
        public double Divide(double value1, double value2) => _divide8Handler.Call<double>(value1, value2);
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> IMockableCalls.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Calls(args => ((double)args[0]!, (double)args[1]!), value1, value2);

        private readonly SourceMock.Internal.MockMethodHandler _sum9Handler = new();
        SourceMock.IMockMethodSetup<int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Setup<int>(value1, value2);
        public int Sum(int value1, int value2) => _sum9Handler.Call<int>(value1, value2);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Calls(args => ((int)args[0]!, (int)args[1]!), value1, value2);

        private readonly SourceMock.Internal.MockMethodHandler _sum10Handler = new();
        SourceMock.IMockMethodSetup<int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Setup<int>(value1, value2, value3);
        public int Sum(int value1, int value2, int value3) => _sum10Handler.Call<int>(value1, value2, value3);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Calls(args => ((int)args[0]!, (int)args[1]!, (int)args[2]!), value1, value2, value3);

        private readonly SourceMock.Internal.MockMethodHandler _execute11Handler = new();
        SourceMock.IMockMethodSetup IMockableSetup.Execute() => _execute11Handler.Setup<SourceMock.Internal.VoidReturn>();
        public void Execute() => _execute11Handler.Call<SourceMock.Internal.VoidReturn>();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.Execute() => _execute11Handler.Calls(_ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockPropertyHandler<int> _count12Handler = new(false);
        SourceMock.IMockPropertySetup<int> IMockableSetup.Count => _count12Handler.Setup();
        public int Count => _count12Handler.GetterHandler.Call<int>();
        SourceMock.IMockPropertyCalls<int> IMockableCalls.Count => _count12Handler.Calls();

        private readonly SourceMock.Internal.MockPropertyHandler<string> _name13Handler = new(true);
        SourceMock.IMockSettablePropertySetup<string> IMockableSetup.Name => _name13Handler.Setup();
        public string Name {
            get => _name13Handler.GetterHandler.Call<string>();
            set => _name13Handler.SetterHandler.Call<string>(value);
        }
        SourceMock.IMockSettablePropertyCalls<string> IMockableCalls.Name => _name13Handler.Calls();
    }

    public interface IMockableSetup {
        SourceMock.IMockMethodSetup<int> GetInt32();
        SourceMock.IMockMethodSetup<int?> GetInt32Nullable();
        SourceMock.IMockMethodSetup<string> GetString();
        SourceMock.IMockMethodSetup<string?> GetStringNullable();
        SourceMock.IMockMethodSetup<global::SourceMock.Tests.Interfaces.IMockable2> GetMockable2();
        SourceMock.IMockMethodSetup<int> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        SourceMock.IMockMethodSetup<bool> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.IMockMethodSetup<double> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        SourceMock.IMockMethodSetup<int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        SourceMock.IMockMethodSetup<int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        SourceMock.IMockMethodSetup Execute();
        SourceMock.IMockPropertySetup<int> Count { get; }
        SourceMock.IMockSettablePropertySetup<string> Name { get; }
    }

    public interface IMockableCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32Nullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringNullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetMockable2();
        System.Collections.Generic.IReadOnlyList<string?> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Execute();
        SourceMock.IMockPropertyCalls<int> Count { get; }
        SourceMock.IMockSettablePropertyCalls<string> Name { get; }
    }
}