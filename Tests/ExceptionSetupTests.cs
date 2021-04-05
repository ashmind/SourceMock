using System;
using SourceMock.Tests.Interfaces.Mocks;
using Xunit;

namespace SourceMock.Tests {
    public class ExceptionSetupTests {
        [Fact]
        public void Simple() {
            var mock = new MockableMock();

            mock.Setup.GetInt32().Throws<Exception>();

            Assert.Throws<Exception>(() => mock.GetInt32());
        }
    }
}
