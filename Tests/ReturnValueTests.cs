using Xunit;
using SourceMock.Tests.TestInterfaces;
using SourceMock.Tests.TestInterfaces.Mocks;

namespace SourceMock.Tests {
    public class ReturnValueTests {
        [Fact]
        public void ValueType() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.GetInt32().Returns(3);

            Assert.Equal(3, mock.GetInt32());
        }

        [Fact]
        public void NullableValueType() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.GetInt32Nullable().Returns(null);

            Assert.Null(mock.GetInt32Nullable());
        }

        [Fact]
        public void ReferenceType() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.GetString().Returns("a");

            Assert.Equal("a", mock.GetString());
        }

        [Fact]
        public void NullableReferenceType() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.GetStringNullable().Returns(null);

            Assert.Null(mock.GetStringNullable());
        }

        [Fact]
        public void Property() {
            var mock = Mock.Of<IMockable>().Get();

            mock.Setup.Count.Returns(10);

            Assert.Equal(10, mock.Count);
        }
    }
}