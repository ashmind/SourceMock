#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    [SourceMock.Internal.GeneratedMock]
    public static class global__SourceMock_Tests_TestInterfaces_IMockable_Mock {
        public class Instance : global::SourceMock.Tests.TestInterfaces.IMockable, ISetupIMockable, ICallsIMockable {
            public ISetupIMockable Setup => this;
            public ICallsIMockable Calls => this;

            private readonly SourceMock.Internal.MockMethodHandler<int> _getInt321Handler = new();
            SourceMock.IMockMethodSetup<int> ISetupIMockable.GetInt32() => _getInt321Handler.Setup();
            public int GetInt32() => _getInt321Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.GetInt32() => _getInt321Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<int?> _getInt32Nullable2Handler = new();
            SourceMock.IMockMethodSetup<int?> ISetupIMockable.GetInt32Nullable() => _getInt32Nullable2Handler.Setup();
            public int? GetInt32Nullable() => _getInt32Nullable2Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.GetInt32Nullable() => _getInt32Nullable2Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<string> _getString3Handler = new();
            SourceMock.IMockMethodSetup<string> ISetupIMockable.GetString() => _getString3Handler.Setup();
            public string GetString() => _getString3Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.GetString() => _getString3Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<string?> _getStringNullable4Handler = new();
            SourceMock.IMockMethodSetup<string?> ISetupIMockable.GetStringNullable() => _getStringNullable4Handler.Setup();
            public string? GetStringNullable() => _getStringNullable4Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.GetStringNullable() => _getStringNullable4Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::SourceMock.Tests.TestInterfaces.IMockable2> _getMockable25Handler = new();
            SourceMock.IMockMethodSetup<global::SourceMock.Tests.TestInterfaces.IMockable2> ISetupIMockable.GetMockable2() => _getMockable25Handler.Setup();
            public global::SourceMock.Tests.TestInterfaces.IMockable2 GetMockable2() => _getMockable25Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.GetMockable2() => _getMockable25Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<int> _parseToInt326Handler = new();
            SourceMock.IMockMethodSetup<int> ISetupIMockable.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Setup(value);
            public int ParseToInt32(string? value) => _parseToInt326Handler.Call(value);
            System.Collections.Generic.IReadOnlyList<string?> ICallsIMockable.ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value) => _parseToInt326Handler.Calls(args => ((string?)args[0]), value);

            private readonly SourceMock.Internal.MockMethodHandler<bool> _testInterface7Handler = new();
            SourceMock.IMockMethodSetup<bool> ISetupIMockable.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> value) => _testInterface7Handler.Setup(value);
            public bool TestInterface(global::SourceMock.Tests.TestInterfaces.IEmptyInterface value) => _testInterface7Handler.Call(value);
            System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> ICallsIMockable.TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> value) => _testInterface7Handler.Calls(args => ((global::SourceMock.Tests.TestInterfaces.IEmptyInterface)args[0]!), value);

            private readonly SourceMock.Internal.MockMethodHandler<double> _divide8Handler = new();
            SourceMock.IMockMethodSetup<double> ISetupIMockable.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Setup(value1, value2);
            public double Divide(double value1, double value2) => _divide8Handler.Call(value1, value2);
            System.Collections.Generic.IReadOnlyList<(double value1, double value2)> ICallsIMockable.Divide(SourceMock.Internal.MockArgumentMatcher<double> value1, SourceMock.Internal.MockArgumentMatcher<double> value2) => _divide8Handler.Calls(args => ((double)args[0]!, (double)args[1]!), value1, value2);

            private readonly SourceMock.Internal.MockMethodHandler<int> _sum9Handler = new();
            SourceMock.IMockMethodSetup<int> ISetupIMockable.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Setup(value1, value2);
            public int Sum(int value1, int value2) => _sum9Handler.Call(value1, value2);
            System.Collections.Generic.IReadOnlyList<(int value1, int value2)> ICallsIMockable.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2) => _sum9Handler.Calls(args => ((int)args[0]!, (int)args[1]!), value1, value2);

            private readonly SourceMock.Internal.MockMethodHandler<int> _sum10Handler = new();
            SourceMock.IMockMethodSetup<int> ISetupIMockable.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Setup(value1, value2, value3);
            public int Sum(int value1, int value2, int value3) => _sum10Handler.Call(value1, value2, value3);
            System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> ICallsIMockable.Sum(SourceMock.Internal.MockArgumentMatcher<int> value1, SourceMock.Internal.MockArgumentMatcher<int> value2, SourceMock.Internal.MockArgumentMatcher<int> value3) => _sum10Handler.Calls(args => ((int)args[0]!, (int)args[1]!, (int)args[2]!), value1, value2, value3);

            private readonly SourceMock.Internal.MockMethodHandler<SourceMock.Internal.VoidReturn> _execute11Handler = new();
            SourceMock.IMockMethodSetup ISetupIMockable.Execute() => _execute11Handler.Setup();
            public void Execute() => _execute11Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICallsIMockable.Execute() => _execute11Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockPropertyHandler<int> _count12Handler = new(false);
            SourceMock.IMockPropertySetup<int> ISetupIMockable.Count => _count12Handler.Setup();
            public int Count => _count12Handler.GetterHandler.Call();
            SourceMock.IMockPropertyCalls<int> ICallsIMockable.Count => _count12Handler.Calls();

            private readonly SourceMock.Internal.MockPropertyHandler<string> _name13Handler = new(true);
            SourceMock.IMockSettablePropertySetup<string> ISetupIMockable.Name => _name13Handler.Setup();
            public string Name {
                get => _name13Handler.GetterHandler.Call();
                set => _name13Handler.SetterHandler.Call(value);
            }
            SourceMock.IMockSettablePropertyCalls<string> ICallsIMockable.Name => _name13Handler.Calls();

        }


        public static global__SourceMock_Tests_TestInterfaces_IMockable_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.IMockable> _) => new();
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ISetupIMockable {
        SourceMock.IMockMethodSetup<int> GetInt32();
        SourceMock.IMockMethodSetup<int?> GetInt32Nullable();
        SourceMock.IMockMethodSetup<string> GetString();
        SourceMock.IMockMethodSetup<string?> GetStringNullable();
        SourceMock.IMockMethodSetup<global::SourceMock.Tests.TestInterfaces.IMockable2> GetMockable2();
        SourceMock.IMockMethodSetup<int> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        SourceMock.IMockMethodSetup<bool> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> value = default);
        SourceMock.IMockMethodSetup<double> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        SourceMock.IMockMethodSetup<int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        SourceMock.IMockMethodSetup<int> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        SourceMock.IMockMethodSetup Execute();
        SourceMock.IMockPropertySetup<int> Count { get; }
        SourceMock.IMockSettablePropertySetup<string> Name { get; }
    }

    [SourceMock.Internal.GeneratedMock]
    public interface ICallsIMockable {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetInt32Nullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetString();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetStringNullable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetMockable2();
        System.Collections.Generic.IReadOnlyList<string?> ParseToInt32(SourceMock.Internal.MockArgumentMatcher<string?> value = default);
        System.Collections.Generic.IReadOnlyList<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> TestInterface(SourceMock.Internal.MockArgumentMatcher<global::SourceMock.Tests.TestInterfaces.IEmptyInterface> value = default);
        System.Collections.Generic.IReadOnlyList<(double value1, double value2)> Divide(SourceMock.Internal.MockArgumentMatcher<double> value1 = default, SourceMock.Internal.MockArgumentMatcher<double> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default);
        System.Collections.Generic.IReadOnlyList<(int value1, int value2, int value3)> Sum(SourceMock.Internal.MockArgumentMatcher<int> value1 = default, SourceMock.Internal.MockArgumentMatcher<int> value2 = default, SourceMock.Internal.MockArgumentMatcher<int> value3 = default);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> Execute();
        SourceMock.IMockPropertyCalls<int> Count { get; }
        SourceMock.IMockSettablePropertyCalls<string> Name { get; }
    }
}