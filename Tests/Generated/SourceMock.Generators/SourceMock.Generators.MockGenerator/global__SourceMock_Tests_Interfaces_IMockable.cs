#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class MockableMock : global::SourceMock.Tests.Interfaces.IMockable, IMockableSetup, IMockableCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable> {
        public IMockableSetup Setup => this;
        public IMockableCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getInt321Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>,int> IMockableSetup.GetInt32() => _getInt321Handler.Setup<System.Func<int>, int>(null, null);
        public int GetInt32() => _getInt321Handler.Call<System.Func<int>, int>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32() => _getInt321Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getInt32Nullable2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int?>,int?> IMockableSetup.GetInt32Nullable() => _getInt32Nullable2Handler.Setup<System.Func<int?>, int?>(null, null);
        public int? GetInt32Nullable() => _getInt32Nullable2Handler.Call<System.Func<int?>, int?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32Nullable() => _getInt32Nullable2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getString3Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>,string> IMockableSetup.GetString() => _getString3Handler.Setup<System.Func<string>, string>(null, null);
        public string GetString() => _getString3Handler.Call<System.Func<string>, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetString() => _getString3Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringNullable4Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>,string?> IMockableSetup.GetStringNullable() => _getStringNullable4Handler.Setup<System.Func<string?>, string?>(null, null);
        public string? GetStringNullable() => _getStringNullable4Handler.Call<System.Func<string?>, string?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringNullable() => _getStringNullable4Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getMockable25Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<SourceMock.Tests.Interfaces.IMockable2>,global::SourceMock.Tests.Interfaces.IMockable2> IMockableSetup.GetMockable2() => _getMockable25Handler.Setup<System.Func<SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        public global::SourceMock.Tests.Interfaces.IMockable2 GetMockable2() => _getMockable25Handler.Call<System.Func<SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetMockable2() => _getMockable25Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringAsync6Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<string>>,global::System.Threading.Tasks.Task<string>> IMockableSetup.GetStringAsync() => _getStringAsync6Handler.Setup<System.Func<System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>>(null, null);
        public global::System.Threading.Tasks.Task<string> GetStringAsync() => _getStringAsync6Handler.Call<System.Func<System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringAsync() => _getStringAsync6Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _parseToInt327Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?,int>,int> IMockableSetup.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt327Handler.Setup<System.Func<string?,int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public int ParseToInt32(string? value) => _parseToInt327Handler.Call<System.Func<string?,int>, int>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<string?> IMockableCalls.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt327Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((string?)args[0]));

        private readonly SourceMock.Internal.MockMethodHandler _testInterface8Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface,bool>,bool> IMockableSetup.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface8Handler.Setup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface,bool>, bool>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public bool TestInterface(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _testInterface8Handler.Call<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface,bool>, bool>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterface8Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _divide9Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<double,double,double>,double> IMockableSetup.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide9Handler.Setup<System.Func<double,double,double>, double>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 });
        public double Divide(double value1, double value2) => _divide9Handler.Call<System.Func<double,double,double>, double>(null, new object?[] { value1, value2 });
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> IMockableCalls.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide9Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 }, args => ((double)args[0]!, (double)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sum10Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int>,int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum10Handler.Setup<System.Func<int,int,int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 });
        public int Sum(int value1, int value2) => _sum10Handler.Call<System.Func<int,int,int>, int>(null, new object?[] { value1, value2 });
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum10Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 }, args => ((int)args[0]!, (int)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sum11Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int,int>,int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum11Handler.Setup<System.Func<int,int,int,int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3 });
        public int Sum(int value1, int value2, int value3) => _sum11Handler.Call<System.Func<int,int,int,int>, int>(null, new object?[] { value1, value2, value3 });
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum11Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3 }, args => ((int)args[0]!, (int)args[1]!, (int)args[2]!));

        private readonly SourceMock.Internal.MockMethodHandler _execute12Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IMockableSetup.Execute() => _execute12Handler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        public void Execute() => _execute12Handler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.Execute() => _execute12Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _execute13Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>> IMockableSetup.Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _execute13Handler.Setup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, SourceMock.Internal.VoidReturn>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public void Execute(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _execute13Handler.Call<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, SourceMock.Internal.VoidReturn>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _execute13Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!));

        private readonly SourceMock.Internal.MockPropertyHandler<int> _count14Handler = new(false);
        SourceMock.Interfaces.IMockPropertySetup<int> IMockableSetup.Count => _count14Handler.Setup();
        public int Count => _count14Handler.GetterHandler.Call<System.Func<int>, int>(null, null);
        SourceMock.Interfaces.IMockPropertyCalls<int> IMockableCalls.Count => _count14Handler.Calls();

        private readonly SourceMock.Internal.MockPropertyHandler<string> _name15Handler = new(true);
        SourceMock.Interfaces.IMockSettablePropertySetup<string> IMockableSetup.Name => _name15Handler.Setup();
        public string Name {
            get => _name15Handler.GetterHandler.Call<System.Func<string,string>, string>(null, null);
            set => _name15Handler.SetterHandler.Call<System.Func<string,string>, string>(null, new object?[] { value });
        }
        SourceMock.Interfaces.IMockSettablePropertyCalls<string> IMockableCalls.Name => _name15Handler.Calls();
    }

    public interface IMockableSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>,int> GetInt32();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int?>,int?> GetInt32Nullable();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>,string> GetString();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>,string?> GetStringNullable();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<SourceMock.Tests.Interfaces.IMockable2>,global::SourceMock.Tests.Interfaces.IMockable2> GetMockable2();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<System.Threading.Tasks.Task<string>>,global::System.Threading.Tasks.Task<string>> GetStringAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?,int>,int> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface,bool>,bool> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<double,double,double>,double> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int>,int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int,int>,int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Action> Execute();
        SourceMock.Interfaces.IMockMethodSetup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>> Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.Interfaces.IMockPropertySetup<int> Count { get; }
        SourceMock.Interfaces.IMockSettablePropertySetup<string> Name { get; }
    }

    public interface IMockableCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32Nullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringNullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetMockable2();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringAsync();
        System.Collections.Generic.IReadOnlyList<string?> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Execute();
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.Interfaces.IMockPropertyCalls<int> Count { get; }
        SourceMock.Interfaces.IMockSettablePropertyCalls<string> Name { get; }
    }
}