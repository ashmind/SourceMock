using Xunit;

namespace SourceMock.Tests {
    public class ArgumentMatchingTests {
        [Fact]
        public void SingleArgument() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.ParseToInt32("1").Returns(1);
            mock.Setup.ParseToInt32("2").Returns(2);

            Assert.Equal(2, mock.ParseToInt32("2"));
        }

        [Fact]
        public void SingleArgument_NullValue() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.ParseToInt32(null).Returns(1);

            Assert.Equal(1, mock.ParseToInt32(null));
        }

        [Fact]
        public void SingleArgument_Optional() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.ParseToInt32().Returns(1);

            Assert.Equal(1, mock.ParseToInt32("x"));
        }
    }
}