using Xunit;
using SourceMock.Tests.Interfaces.Mocks;
using SourceMock.Tests.Interfaces;

namespace SourceMock.Tests {
    public class ArgumentTests {
        [Fact]
        public void SingleArgument() {
            var mock = new MockableMock();

            mock.Setup.ParseToInt32("1").Returns(1);
            mock.Setup.ParseToInt32("2").Returns(2);

            Assert.Equal(2, mock.ParseToInt32("2"));
        }

        [Fact]
        public void SingleArgument_NullValue() {
            var mock = new MockableMock();

            mock.Setup.ParseToInt32(null).Returns(1);

            Assert.Equal(1, mock.ParseToInt32(null));
        }

        [Fact]
        public void SingleArgument_Interface() {
            var mock = new MockableMock();
            var argument = new EmptyClass();

            mock.Setup.TestInterface(argument).Returns(true);

            Assert.True(mock.TestInterface(argument));
        }

        [Fact]
        public void SingleArgument_Optional() {
            var mock = new MockableMock();

            mock.Setup.ParseToInt32().Returns(1);

            Assert.Equal(1, mock.ParseToInt32("x"));
        }

        [Fact]
        public void MultipleArguments() {
            var mock = new MockableMock();

            mock.Setup.Sum(1, 2).Returns(3);

            Assert.Equal(3, mock.Sum(1, 2));
        }

        [Fact]
        public void InArgument() {
            var mock = new NeedsParameterModifiersMock();

            mock.Setup.TestIn(1).Returns(2);

            Assert.Equal(2, mock.TestIn(1));
        }

        [Fact]
        public void RefArgument() {
            var mock = new NeedsParameterModifiersMock();

            mock.Setup.TestRef(1).Returns(2);

            var x = 1;
            Assert.Equal(2, mock.TestRef(ref x));
        }

        [Fact]
        public void OutArgument() {
            var mock = new NeedsParameterModifiersMock();

            mock.Setup.TestOut().Returns(2);

            int x;
            Assert.Equal(2, mock.TestOut(out x));
        }

        [Fact]
        public void Generic() {
            var mock = new NeedsGenericsMock();

            mock.Setup.Get<long>().Returns(1);
            mock.Setup.Get<int>().Returns(2);
            mock.Setup.Get<double>().Returns(3);

            Assert.Equal(2, mock.Get<int>());
        }

        private class EmptyClass : IEmptyInterface {}
    }
}