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
    }
}