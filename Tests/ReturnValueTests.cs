using Xunit;

namespace SourceMock.Tests {
    public class ReturnValueTests {
        [Fact]
        public void ValueType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.GetInt32().Returns(3);

            Assert.Equal(3, mock.GetInt32());
        }

        [Fact]
        public void NullableValueType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.GetInt32Nullable().Returns(null);

            Assert.Null(mock.GetInt32Nullable());
        }

        [Fact]
        public void ReferenceType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.GetString().Returns("a");

            Assert.Equal("a", mock.GetString());
        }

        [Fact]
        public void NullableReferenceType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.GetStringNullable().Returns(null);

            Assert.Null(mock.GetStringNullable());
        }
    }
}