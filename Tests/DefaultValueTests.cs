using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SourceMock.Tests.TestInterfaces;
using Xunit;

namespace SourceMock.Tests {
    public class DefaultValueTests {
        [Theory]
        [MemberData(nameof(AllCollectionMethods))]
        public void EmptyCollections(MethodInfo method) {
            var mock = Mock.Of<INeedsCollectionDefaults>().Get();

            var result = method.Invoke(mock, null);

            Assert.NotNull(result);
            Assert.Empty((IEnumerable)result!);
        }

        public static IEnumerable<object[]> AllCollectionMethods { get; } = typeof(INeedsCollectionDefaults)
            .GetMethods()
            .Select(m => new object[] { m })
            .ToList();
    }
}
