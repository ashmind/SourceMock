using Xunit;

namespace SourceMock.Tests {
    public class ReturnValueTests {
        [Fact]
        public void Returns_SetsUpReturnValue_ForValueType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.Int32Return().Returns(3);

            Assert.Equal(3, mock.Int32Return());
        }

        [Fact]
        public void Returns_SetsUpNullReturnValue_ForNullableValueType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.NullableInt32Return().Returns(null);

            Assert.Null(mock.NullableInt32Return());
        }

        [Fact]
        public void Returns_SetsUpReturnValue_ForReferenceType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.StringReturn().Returns("a");

            Assert.Equal("a", mock.StringReturn());
        }

        [Fact]
        public void Returns_SetsUpReturnValue_ForNullableReferenceType() {
            var mock = Mocks.Get(default(IMockable));

            mock.Setup.NullableStringReturn().Returns(null);

            Assert.Null(mock.NullableStringReturn());
        }
    }
}