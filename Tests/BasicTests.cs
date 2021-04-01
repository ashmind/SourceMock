using Xunit;

namespace SourceMock.Tests {
    public class BasicTests {
        [Fact]
        public void Returns_SetsUpReturnValue() {
            var mock = Mocks.Get(default(IMockable));
            mock.Setup.SimpleReturn().Returns(3);

            Assert.Equal(3, mock.SimpleReturn());
        }
    }
}