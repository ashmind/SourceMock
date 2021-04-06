using Xunit;
using SourceMock.Tests.Interfaces.Mocks;

namespace SourceMock.Tests {
    public class CallbackTests {
        [Fact]
        public void A() {
            string? calledWith = null;

            var mock = new MockableMock();

            mock.Setup.ParseToInt32("1").Callback((string? s) => {
                calledWith = s;
            });

            mock.ParseToInt32("1");

            Assert.Equal("1", calledWith);
        }
    }
}