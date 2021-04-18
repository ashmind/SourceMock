using Xunit;
using SourceMock.Tests.Interfaces.Mocks;
using SourceMock.Tests.Interfaces;

namespace SourceMock.Tests {
    public class RunsCallbackTests {
        [Fact]
        public void MethodArgument_ReturnValue() {
            var mock = new MockableMock();

            mock.Setup.ParseToInt32("1")
                .Runs(s => s == "1" ? 1 : default);

            var result = mock.ParseToInt32("1");

            Assert.Equal(1, result);
        }

        [Fact]
        public void MethodNoArgument_ReturnValue() {
            var mock = new MockableMock();
            mock.Setup.GetInt32().Runs(() => 3);

            var result = mock.GetInt32();

            Assert.Equal(3, result);
        }

        [Fact]
        public void Method_Void() {
            IEmptyInterface? runsArgument = null;
            var argument = new EmptyClass();

            var mock = new MockableMock();
            mock.Setup.Execute(default)
                .Runs(argument => runsArgument = argument);

            mock.Execute(argument);

            Assert.Equal(argument, runsArgument);
        }

        [Fact]
        public void Method_Out() {
            var mock = new NeedsParameterModifiersMock();
            mock.Setup.TestOut().Runs((out int x) => x = 5);

            mock.TestOut(out var result);

            Assert.Equal(5, result);
        }

        [Fact]
        public void Method_Generic() {
            var mock = new NeedsGenericsMock<int>();
            mock.Setup.Convert<string>().Runs(x => x.ToString());

            var result = mock.Convert<string>(5);

            Assert.Equal("5", result);
        }

        [Fact]
        public void Property_ReturnValue() {
            var mock = new MockableMock();
            mock.Setup.Count.get.Runs(() => 5);

            var result = mock.Count;

            Assert.Equal(5, result);
        }

        private class EmptyClass : IEmptyInterface { }
    }
}