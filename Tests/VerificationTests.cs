using Xunit;
using SourceMock.Tests.TestInterfaces;

namespace SourceMock.Tests {
    public class VerificationTests {
        [Fact]
        public void NoArguments() {
            var mock = Mock.Of<IMockable>().Get();

            mock.GetInt32();

            Assert.Equal(1, mock.Calls.GetInt32.Count);
        }

        [Fact]
        public void OneArgument() {
            var mock = Mock.Of<IMockable>().Get();

            mock.ParseToInt32("x");

            Assert.Equal(new[] { "x" }, mock.Calls.ParseToInt32);
        }

        [Fact]
        public void MultipleArguments() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Sum(1, 2);

            Assert.Equal(new[] { (1, 2) }, mock.Calls.Sum);
        }
    }
}