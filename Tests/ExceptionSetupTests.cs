using System;
using SourceMock.Tests.TestInterfaces;
using SourceMock.Tests.TestInterfaces.Mocks;
using Xunit;

namespace SourceMock.Tests {
    public class ExceptionSetupTests {
        [Fact]
        public void Simple() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.GetInt32().Throws<Exception>();

            Assert.Throws<Exception>(() => mock.GetInt32());
        }
    }
}
