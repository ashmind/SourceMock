using System.Collections.Generic;
using System.Collections.Immutable;

namespace SourceMock.Tests.TestInterfaces {
    public interface INeedsCollectionDefaults {
        int[] GetArray();
        List<int> GetList();
        ImmutableList<int> GetImmutableList();
        ImmutableArray<int> GetImmutableArray();
        IEnumerable<int> GetIEnumerable();
        IList<int> GetIList();
        ICollection<int> GetICollection();
        IReadOnlyCollection<int> GetIReadOnlyCollection();
        IReadOnlyList<int> GetIReadOnlyList();
        IImmutableList<int> GetIImmutableList();

        Dictionary<string, string> GetDictionary();
        ImmutableDictionary<string, string> GetImmutableDictionary();
        IDictionary<string, string> GetIDictionary();
        IReadOnlyDictionary<string, string> GetIReadOnlyDictionary();
        IImmutableDictionary<string, string> GetIImmutableDictionary();

        HashSet<string> GetHashSet();
        ImmutableHashSet<string> GetImmutableHashSet();
        ISet<string> GetISet();
        IReadOnlySet<string> GetIReadOnlySet();
        IImmutableSet<string> GetIImmutableSet();
    }
}