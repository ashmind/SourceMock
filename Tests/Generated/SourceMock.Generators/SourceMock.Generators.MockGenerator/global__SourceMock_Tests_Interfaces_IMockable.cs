#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class MockableMock : global::SourceMock.Tests.Interfaces.IMockable, IMockableSetup, IMockableCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.IMockable> {
        public IMockableSetup Setup => this;
        public IMockableCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getInt32Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>, int> IMockableSetup.GetInt32() => _getInt32Handler.Setup<System.Func<int>, int>(null, null);
        public int GetInt32() => _getInt32Handler.Call<System.Func<int>, int>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32() => _getInt32Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getInt32NullableHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int?>, int?> IMockableSetup.GetInt32Nullable() => _getInt32NullableHandler.Setup<System.Func<int?>, int?>(null, null);
        public int? GetInt32Nullable() => _getInt32NullableHandler.Call<System.Func<int?>, int?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetInt32Nullable() => _getInt32NullableHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> IMockableSetup.GetString() => _getStringHandler.Setup<System.Func<string>, string>(null, null);
        public string GetString() => _getStringHandler.Call<System.Func<string>, string>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetString() => _getStringHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringNullableHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>, string?> IMockableSetup.GetStringNullable() => _getStringNullableHandler.Setup<System.Func<string?>, string?>(null, null);
        public string? GetStringNullable() => _getStringNullableHandler.Call<System.Func<string?>, string?>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringNullable() => _getStringNullableHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getMockable2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2> IMockableSetup.GetMockable2() => _getMockable2Handler.Setup<System.Func<global::SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        public global::SourceMock.Tests.Interfaces.IMockable2 GetMockable2() => _getMockable2Handler.Call<System.Func<global::SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetMockable2() => _getMockable2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getStringAsyncHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>> IMockableSetup.GetStringAsync() => _getStringAsyncHandler.Setup<System.Func<global::System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>>(null, null);
        public global::System.Threading.Tasks.Task<string> GetStringAsync() => _getStringAsyncHandler.Call<System.Func<global::System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.GetStringAsync() => _getStringAsyncHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _parseToInt32Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?, int>, int> IMockableSetup.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt32Handler.Setup<System.Func<string?, int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public int ParseToInt32(string? value) => _parseToInt32Handler.Call<System.Func<string?, int>, int>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<string?> IMockableCalls.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt32Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((string?)args[0]));

        private readonly SourceMock.Internal.MockMethodHandler _testInterfaceHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface, bool>, bool> IMockableSetup.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterfaceHandler.Setup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface, bool>, bool>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public bool TestInterface(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _testInterfaceHandler.Call<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface, bool>, bool>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _testInterfaceHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _divideHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<double,double, double>, double> IMockableSetup.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divideHandler.Setup<System.Func<double,double, double>, double>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 });
        public double Divide(double value1, double value2) => _divideHandler.Call<System.Func<double,double, double>, double>(null, new object?[] { value1, value2 });
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> IMockableCalls.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divideHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 }, args => ((double)args[0]!, (double)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sumHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int, int>, int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sumHandler.Setup<System.Func<int,int, int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 });
        public int Sum(int value1, int value2) => _sumHandler.Call<System.Func<int,int, int>, int>(null, new object?[] { value1, value2 });
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sumHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2 }, args => ((int)args[0]!, (int)args[1]!));

        private readonly SourceMock.Internal.MockMethodHandler _sum2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int, int>, int> IMockableSetup.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum2Handler.Setup<System.Func<int,int,int, int>, int>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3 });
        public int Sum(int value1, int value2, int value3) => _sum2Handler.Call<System.Func<int,int,int, int>, int>(null, new object?[] { value1, value2, value3 });
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> IMockableCalls.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum2Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value1, value2, value3 }, args => ((int)args[0]!, (int)args[1]!, (int)args[2]!));

        private readonly SourceMock.Internal.MockMethodHandler _executeHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action> IMockableSetup.Execute() => _executeHandler.Setup<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        public void Execute() => _executeHandler.Call<System.Action, SourceMock.Internal.VoidReturn>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> IMockableCalls.Execute() => _executeHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _execute2Handler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>> IMockableSetup.Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _execute2Handler.Setup<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, SourceMock.Internal.VoidReturn>(null, new SourceMock.Internal.IMockArgumentMatcher[] { value });
        public void Execute(global::SourceMock.Tests.Interfaces.IEmptyInterface value) => _execute2Handler.Call<System.Action<global::SourceMock.Tests.Interfaces.IEmptyInterface>, SourceMock.Internal.VoidReturn>(null, new object?[] { value });
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.Interfaces.IEmptyInterface> IMockableCalls.Execute(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value) => _execute2Handler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { value }, args => ((global::SourceMock.Tests.Interfaces.IEmptyInterface)args[0]!));

        private readonly SourceMock.Internal.MockPropertyHandler<int> _countHandler = new(false);
        SourceMock.Interfaces.IMockPropertySetup<int> IMockableSetup.Count => _countHandler.Setup();
        public int Count => _countHandler.GetterHandler.Call<System.Func<int>, int>(null, null);
        SourceMock.Interfaces.IMockPropertyCalls<int> IMockableCalls.Count => _countHandler.Calls();

        private readonly SourceMock.Internal.MockPropertyHandler<string> _nameHandler = new(true);
        SourceMock.Interfaces.IMockSettablePropertySetup<string> IMockableSetup.Name => _nameHandler.Setup();
        public string Name {
            get => _nameHandler.GetterHandler.Call<System.Func<string>, string>(null, null);
            set => _nameHandler.SetterHandler.Call<System.Action<string>, string>(null, new object?[] { value });
        }
        SourceMock.Interfaces.IMockSettablePropertyCalls<string> IMockableCalls.Name => _nameHandler.Calls();
    }

    public interface IMockableSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int>, int> GetInt32();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int?>, int?> GetInt32Nullable();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string>, string> GetString();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?>, string?> GetStringNullable();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IMockable2>, global::SourceMock.Tests.Interfaces.IMockable2> GetMockable2();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Threading.Tasks.Task<string>>, global::System.Threading.Tasks.Task<string>> GetStringAsync();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<string?, int>, int> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::SourceMock.Tests.Interfaces.IEmptyInterface, bool>, bool> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.Interfaces.IEmptyInterface> value = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<double,double, double>, double> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int, int>, int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int,int,int, int>, int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
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