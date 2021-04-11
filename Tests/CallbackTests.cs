using Xunit;
using SourceMock.Tests.Interfaces.Mocks;
using System.Linq;
using SourceMock.Tests.Interfaces;

namespace SourceMock.Tests {
    public class CallbackTests {
        [Fact]
        public void MethodArgument_ReturnValue() {
            var mock = new MockableMock();

            mock.Setup.ParseToInt32("1").Callback((s) => {
                if(s == "1") {
                    return 1;
                }

                return default;
            });

            var result = mock.ParseToInt32("1");

            Assert.Equal(1, result);
        }

        [Fact]
        public void MethodNoArgument_ReturnValue() {
            var mock = new MockableMock();
            mock.Setup.GetInt32().Callback(() => 3);

            var result = mock.GetInt32();

            Assert.Equal(3, result);
        }

        [Fact]
        public void Method_Void() {
            IEmptyInterface? callbackArgument = null;
            var argument = new EmptyClass();

            var mock = new MockableMock();
            mock.Setup.Execute(default).Callback((argument) => {
                callbackArgument = argument;
            });

            mock.Execute(argument);

            Assert.Equal(argument, callbackArgument);
        }

        [Fact]
        public void Property_ReturnValue() {
            var mock = new MockableMock();
            mock.Setup.Count.Callback(() => 5);

            var result = mock.Count;

            Assert.Equal(5, result);
        }

        private class EmptyClass : IEmptyInterface { }
    }
}