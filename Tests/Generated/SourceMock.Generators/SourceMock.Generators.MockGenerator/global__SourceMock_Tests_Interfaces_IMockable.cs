#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class MockableMock : global::SourceMock.Tests.Interfaces.IMockable, IMockableSetup, IMockableCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable> {
        public IMockableSetup Setup => this;
        public IMockableCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getInt321Handler = new();

        SourceMock.IMockMethodSetup<System.Action,int> IMockableSetup.GetInt32() => _getInt321Handler.Setup<System.Action, int>(null, null);
        public int GetInt32() => _getInt321Handler.Call<System.Action, int>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32() => _getInt321Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getInt32Nullable2Handler = new();

        SourceMock.IMockMethodSetup<System.Action,int?> IMockableSetup.GetInt32Nullable() => _getInt32Nullable2Handler.Setup<System.Action, int?>(null, null);
        public int? GetInt32Nullable() => _getInt32Nullable2Handler.Call<System.Action, int?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32Nullable() => _getInt32Nullable2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getString3Handler = new();

        SourceMock.IMockMethodSetup<System.Action,string> IMockableSetup.GetString() => _getString3Handler.Setup<System.Action, string>(null, null);
        public string GetString() => _getString3Handler.Call<System.Action, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetString() => _getString3Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringNullable4Handler = new();

        SourceMock.IMockMethodSetup<System.Action,string?> IMockableSetup.GetStringNullable() => _getStringNullable4Handler.Setup<System.Action, string?>(null, null);
        public string? GetStringNullable() => _getStringNullable4Handler.Call<System.Action, string?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringNullable() => _getStringNullable4Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getMockable25Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::SourceMock.Tests.Interfaces.IMockable2> IMockableSetup.GetMockable2() => _getMockable25Handler.Setup<System.Action, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        public global::SourceMock.Tests.Interfaces.IMockable2 GetMockable2() => _getMockable25Handler.Call<System.Action, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetMockable2() => _getMockable25Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _parseToInt326Handler = new();

        SourceMock.IMockMethodSetup<System.Action<string?>,int> IMockableSetup.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Setup<System.Action<string?>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value});
        public int ParseToInt32(string? value) => _parseToInt326Handler.Call<System.Action<string?>, int>(null, new object?[] { value});
        System.Collections.Generic.IReadOnlyList<string?> IMockableCalls.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value}, args => ((string?)args[0]));

        private readonly SourceMock.Internal.MockMethodHandler _testInterface7Handler = new();

        SourceMock.IMockMethodSetup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>,bool> IMockableSetup.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface7Handler.Setup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, bool>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value});
        public bool TestInterface(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _testInterface7Handler.Call<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, bool>(null, new object?[] { value});
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface7Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value}, args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _divide8Handler = new();

        SourceMock.IMockMethodSetup<System.Action<double,double>,double> IMockableSetup.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Setup<System.Action<double,double>, double>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2});
        public double Divide(double value1, double value2) => _divide8Handler.Call<System.Action<double,double>, double>(null, new object?[] { value1, value2});
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> IMockableCalls.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2}, args => ((double)args[0]!, (double)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sum9Handler = new();

        SourceMock.IMockMethodSetup<System.Action<int,int>,int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Setup<System.Action<int,int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2});
        public int Sum(int value1, int value2) => _sum9Handler.Call<System.Action<int,int>, int>(null, new object?[] { value1, value2});
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2}, args => ((int)args[0]!, (int)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sum10Handler = new();

        SourceMock.IMockMethodSetup<System.Action<int,int,int>,int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Setup<System.Action<int,int,int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3});
        public int Sum(int value1, int value2, int value3) => _sum10Handler.Call<System.Action<int,int,int>, int>(null, new object?[] { value1, value2, value3});
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3}, args => ((int)args[0]!, (int)args[1]!, (int)args[2]!));

        private readonly SourceMock.Internal.MockMethodHandler _execute11Handler = new();

        SourceMock.IMockMethodSetup IMockableSetup.Execute() => _execute11Handler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        public void Execute() => _execute11Handler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.Execute() => _execute11Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockPropertyHandler<int> _count12Handler = new(false);

        SourceMock.IMockPropertySetup<int> IMockableSetup.Count => _count12Handler.Setup();
        public int Count => _count12Handler.GetterHandler.Call<System.Action, int>(null, null);
        SourceMock.IMockPropertyCalls<int> IMockableCalls.Count => _count12Handler.Calls();

        private readonly SourceMock.Internal.MockPropertyHandler<string> _name13Handler = new(true);

        SourceMock.IMockSettablePropertySetup<string> IMockableSetup.Name => _name13Handler.Setup();
        public string Name {
            get => _name13Handler.GetterHandler.Call<System.Action<string>, string>(null, null);
            set => _name13Handler.SetterHandler.Call<System.Action<string>, string>(null, new object?[] { value});
        }
        SourceMock.IMockSettablePropertyCalls<string> IMockableCalls.Name => _name13Handler.Calls();
    }

    public interface IMockableSetup {
        SourceMock.IMockMethodSetup<System.Action,int> GetInt32();
        SourceMock.IMockMethodSetup<System.Action,int?> GetInt32Nullable();
        SourceMock.IMockMethodSetup<System.Action,string> GetString();
        SourceMock.IMockMethodSetup<System.Action,string?> GetStringNullable();
        SourceMock.IMockMethodSetup<System.Action,global::SourceMock.Tests.Interfaces.IMockable2> GetMockable2();
        SourceMock.IMockMethodSetup<System.Action<string?>,int> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        SourceMock.IMockMethodSetup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>,bool> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.IMockMethodSetup<System.Action<double,double>,double> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        SourceMock.IMockMethodSetup<System.Action<int,int>,int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        SourceMock.IMockMethodSetup<System.Action<int,int,int>,int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
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