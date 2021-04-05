using Xunit;
using SourceMock.Tests.TestInterfaces;
using SourceMock.Tests.TestInterfaces.Mocks;

namespace SourceMock.Tests {
    public class VerificationTests {
        [Fact]
        public void NoArguments() {
            var mock = Mock.Of<IMockable>().Get();

            mock.GetInt32();

            Assert.Equal(1, mock.Calls.GetInt32().Count);
        }

        [Fact]
        public void OneArgument() {
            var mock = Mock.Of<IMockable>().Get();

            mock.ParseToInt32("x");

            Assert.Equal(new[] { "x" }, mock.Calls.ParseToInt32());
        }

        [Fact]
        public void MultipleArguments() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Divide(4, 2);

            Assert.Equal(new[] { (4d, 2d) }, mock.Calls.Divide());
        }

        [Fact]
        public void MultipleArguments_Filtered() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Divide(4, 2);
            mock.Divide(1, 5);

            Assert.Single(mock.Calls.Divide(4, 2));
        }

        [Fact]
        public void Overloaded() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Sum(1, 2);

            Assert.Equal(new[] { (1, 2) }, mock.Calls.Sum(default, default));
        }

        [Fact]
        public void Property_Get() {
            var mock = Mock.Of<IMockable>().Get();

            var _ = mock.Count;

            Assert.Single(mock.Calls.Count.get);
        }

        [Fact]
        public void Property_Set() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Name = "test";

            Assert.Equal(new[] { "test" }, mock.Calls.Name.set());
        }
    }
}