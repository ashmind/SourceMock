#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    internal class NeedsCollectionDefaultsMock : global::SourceMock.Tests.Interfaces.INeedsCollectionDefaults, INeedsCollectionDefaultsSetup, INeedsCollectionDefaultsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsCollectionDefaults> {
        public INeedsCollectionDefaultsSetup Setup => this;
        public INeedsCollectionDefaultsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getArrayHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int[]>, int[]> INeedsCollectionDefaultsSetup.GetArray() => _getArrayHandler.Setup<System.Func<int[]>, int[]>(null, null);
        public int[] GetArray() => _getArrayHandler.Call<System.Func<int[]>, int[]>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetArray() => _getArrayHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getListHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.List<int>>, global::System.Collections.Generic.List<int>> INeedsCollectionDefaultsSetup.GetList() => _getListHandler.Setup<System.Func<global::System.Collections.Generic.List<int>>, global::System.Collections.Generic.List<int>>(null, null);
        public global::System.Collections.Generic.List<int> GetList() => _getListHandler.Call<System.Func<global::System.Collections.Generic.List<int>>, global::System.Collections.Generic.List<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetList() => _getListHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableListHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableList<int>>, global::System.Collections.Immutable.ImmutableList<int>> INeedsCollectionDefaultsSetup.GetImmutableList() => _getImmutableListHandler.Setup<System.Func<global::System.Collections.Immutable.ImmutableList<int>>, global::System.Collections.Immutable.ImmutableList<int>>(null, null);
        public global::System.Collections.Immutable.ImmutableList<int> GetImmutableList() => _getImmutableListHandler.Call<System.Func<global::System.Collections.Immutable.ImmutableList<int>>, global::System.Collections.Immutable.ImmutableList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableList() => _getImmutableListHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableArrayHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableArray<int>>, global::System.Collections.Immutable.ImmutableArray<int>> INeedsCollectionDefaultsSetup.GetImmutableArray() => _getImmutableArrayHandler.Setup<System.Func<global::System.Collections.Immutable.ImmutableArray<int>>, global::System.Collections.Immutable.ImmutableArray<int>>(null, null);
        public global::System.Collections.Immutable.ImmutableArray<int> GetImmutableArray() => _getImmutableArrayHandler.Call<System.Func<global::System.Collections.Immutable.ImmutableArray<int>>, global::System.Collections.Immutable.ImmutableArray<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableArray() => _getImmutableArrayHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIEnumerableHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IEnumerable<int>>, global::System.Collections.Generic.IEnumerable<int>> INeedsCollectionDefaultsSetup.GetIEnumerable() => _getIEnumerableHandler.Setup<System.Func<global::System.Collections.Generic.IEnumerable<int>>, global::System.Collections.Generic.IEnumerable<int>>(null, null);
        public global::System.Collections.Generic.IEnumerable<int> GetIEnumerable() => _getIEnumerableHandler.Call<System.Func<global::System.Collections.Generic.IEnumerable<int>>, global::System.Collections.Generic.IEnumerable<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIEnumerable() => _getIEnumerableHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIListHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IList<int>>, global::System.Collections.Generic.IList<int>> INeedsCollectionDefaultsSetup.GetIList() => _getIListHandler.Setup<System.Func<global::System.Collections.Generic.IList<int>>, global::System.Collections.Generic.IList<int>>(null, null);
        public global::System.Collections.Generic.IList<int> GetIList() => _getIListHandler.Call<System.Func<global::System.Collections.Generic.IList<int>>, global::System.Collections.Generic.IList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIList() => _getIListHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getICollectionHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.ICollection<int>>, global::System.Collections.Generic.ICollection<int>> INeedsCollectionDefaultsSetup.GetICollection() => _getICollectionHandler.Setup<System.Func<global::System.Collections.Generic.ICollection<int>>, global::System.Collections.Generic.ICollection<int>>(null, null);
        public global::System.Collections.Generic.ICollection<int> GetICollection() => _getICollectionHandler.Call<System.Func<global::System.Collections.Generic.ICollection<int>>, global::System.Collections.Generic.ICollection<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetICollection() => _getICollectionHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyCollectionHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyCollection<int>>, global::System.Collections.Generic.IReadOnlyCollection<int>> INeedsCollectionDefaultsSetup.GetIReadOnlyCollection() => _getIReadOnlyCollectionHandler.Setup<System.Func<global::System.Collections.Generic.IReadOnlyCollection<int>>, global::System.Collections.Generic.IReadOnlyCollection<int>>(null, null);
        public global::System.Collections.Generic.IReadOnlyCollection<int> GetIReadOnlyCollection() => _getIReadOnlyCollectionHandler.Call<System.Func<global::System.Collections.Generic.IReadOnlyCollection<int>>, global::System.Collections.Generic.IReadOnlyCollection<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyCollection() => _getIReadOnlyCollectionHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyListHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyList<int>>, global::System.Collections.Generic.IReadOnlyList<int>> INeedsCollectionDefaultsSetup.GetIReadOnlyList() => _getIReadOnlyListHandler.Setup<System.Func<global::System.Collections.Generic.IReadOnlyList<int>>, global::System.Collections.Generic.IReadOnlyList<int>>(null, null);
        public global::System.Collections.Generic.IReadOnlyList<int> GetIReadOnlyList() => _getIReadOnlyListHandler.Call<System.Func<global::System.Collections.Generic.IReadOnlyList<int>>, global::System.Collections.Generic.IReadOnlyList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyList() => _getIReadOnlyListHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableListHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableList<int>>, global::System.Collections.Immutable.IImmutableList<int>> INeedsCollectionDefaultsSetup.GetIImmutableList() => _getIImmutableListHandler.Setup<System.Func<global::System.Collections.Immutable.IImmutableList<int>>, global::System.Collections.Immutable.IImmutableList<int>>(null, null);
        public global::System.Collections.Immutable.IImmutableList<int> GetIImmutableList() => _getIImmutableListHandler.Call<System.Func<global::System.Collections.Immutable.IImmutableList<int>>, global::System.Collections.Immutable.IImmutableList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableList() => _getIImmutableListHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getDictionaryHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.Dictionary<string, string>>, global::System.Collections.Generic.Dictionary<string, string>> INeedsCollectionDefaultsSetup.GetDictionary() => _getDictionaryHandler.Setup<System.Func<global::System.Collections.Generic.Dictionary<string, string>>, global::System.Collections.Generic.Dictionary<string, string>>(null, null);
        public global::System.Collections.Generic.Dictionary<string, string> GetDictionary() => _getDictionaryHandler.Call<System.Func<global::System.Collections.Generic.Dictionary<string, string>>, global::System.Collections.Generic.Dictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetDictionary() => _getDictionaryHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableDictionaryHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableDictionary<string, string>>, global::System.Collections.Immutable.ImmutableDictionary<string, string>> INeedsCollectionDefaultsSetup.GetImmutableDictionary() => _getImmutableDictionaryHandler.Setup<System.Func<global::System.Collections.Immutable.ImmutableDictionary<string, string>>, global::System.Collections.Immutable.ImmutableDictionary<string, string>>(null, null);
        public global::System.Collections.Immutable.ImmutableDictionary<string, string> GetImmutableDictionary() => _getImmutableDictionaryHandler.Call<System.Func<global::System.Collections.Immutable.ImmutableDictionary<string, string>>, global::System.Collections.Immutable.ImmutableDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableDictionary() => _getImmutableDictionaryHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIDictionaryHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IDictionary<string, string>>, global::System.Collections.Generic.IDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIDictionary() => _getIDictionaryHandler.Setup<System.Func<global::System.Collections.Generic.IDictionary<string, string>>, global::System.Collections.Generic.IDictionary<string, string>>(null, null);
        public global::System.Collections.Generic.IDictionary<string, string> GetIDictionary() => _getIDictionaryHandler.Call<System.Func<global::System.Collections.Generic.IDictionary<string, string>>, global::System.Collections.Generic.IDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIDictionary() => _getIDictionaryHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyDictionaryHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyDictionary<string, string>>, global::System.Collections.Generic.IReadOnlyDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIReadOnlyDictionary() => _getIReadOnlyDictionaryHandler.Setup<System.Func<global::System.Collections.Generic.IReadOnlyDictionary<string, string>>, global::System.Collections.Generic.IReadOnlyDictionary<string, string>>(null, null);
        public global::System.Collections.Generic.IReadOnlyDictionary<string, string> GetIReadOnlyDictionary() => _getIReadOnlyDictionaryHandler.Call<System.Func<global::System.Collections.Generic.IReadOnlyDictionary<string, string>>, global::System.Collections.Generic.IReadOnlyDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyDictionary() => _getIReadOnlyDictionaryHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableDictionaryHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableDictionary<string, string>>, global::System.Collections.Immutable.IImmutableDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIImmutableDictionary() => _getIImmutableDictionaryHandler.Setup<System.Func<global::System.Collections.Immutable.IImmutableDictionary<string, string>>, global::System.Collections.Immutable.IImmutableDictionary<string, string>>(null, null);
        public global::System.Collections.Immutable.IImmutableDictionary<string, string> GetIImmutableDictionary() => _getIImmutableDictionaryHandler.Call<System.Func<global::System.Collections.Immutable.IImmutableDictionary<string, string>>, global::System.Collections.Immutable.IImmutableDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableDictionary() => _getIImmutableDictionaryHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getHashSetHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.HashSet<string>>, global::System.Collections.Generic.HashSet<string>> INeedsCollectionDefaultsSetup.GetHashSet() => _getHashSetHandler.Setup<System.Func<global::System.Collections.Generic.HashSet<string>>, global::System.Collections.Generic.HashSet<string>>(null, null);
        public global::System.Collections.Generic.HashSet<string> GetHashSet() => _getHashSetHandler.Call<System.Func<global::System.Collections.Generic.HashSet<string>>, global::System.Collections.Generic.HashSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetHashSet() => _getHashSetHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableHashSetHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableHashSet<string>>, global::System.Collections.Immutable.ImmutableHashSet<string>> INeedsCollectionDefaultsSetup.GetImmutableHashSet() => _getImmutableHashSetHandler.Setup<System.Func<global::System.Collections.Immutable.ImmutableHashSet<string>>, global::System.Collections.Immutable.ImmutableHashSet<string>>(null, null);
        public global::System.Collections.Immutable.ImmutableHashSet<string> GetImmutableHashSet() => _getImmutableHashSetHandler.Call<System.Func<global::System.Collections.Immutable.ImmutableHashSet<string>>, global::System.Collections.Immutable.ImmutableHashSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableHashSet() => _getImmutableHashSetHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getISetHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.ISet<string>>, global::System.Collections.Generic.ISet<string>> INeedsCollectionDefaultsSetup.GetISet() => _getISetHandler.Setup<System.Func<global::System.Collections.Generic.ISet<string>>, global::System.Collections.Generic.ISet<string>>(null, null);
        public global::System.Collections.Generic.ISet<string> GetISet() => _getISetHandler.Call<System.Func<global::System.Collections.Generic.ISet<string>>, global::System.Collections.Generic.ISet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetISet() => _getISetHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlySetHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlySet<string>>, global::System.Collections.Generic.IReadOnlySet<string>> INeedsCollectionDefaultsSetup.GetIReadOnlySet() => _getIReadOnlySetHandler.Setup<System.Func<global::System.Collections.Generic.IReadOnlySet<string>>, global::System.Collections.Generic.IReadOnlySet<string>>(null, null);
        public global::System.Collections.Generic.IReadOnlySet<string> GetIReadOnlySet() => _getIReadOnlySetHandler.Call<System.Func<global::System.Collections.Generic.IReadOnlySet<string>>, global::System.Collections.Generic.IReadOnlySet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlySet() => _getIReadOnlySetHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableSetHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableSet<string>>, global::System.Collections.Immutable.IImmutableSet<string>> INeedsCollectionDefaultsSetup.GetIImmutableSet() => _getIImmutableSetHandler.Setup<System.Func<global::System.Collections.Immutable.IImmutableSet<string>>, global::System.Collections.Immutable.IImmutableSet<string>>(null, null);
        public global::System.Collections.Immutable.IImmutableSet<string> GetIImmutableSet() => _getIImmutableSetHandler.Call<System.Func<global::System.Collections.Immutable.IImmutableSet<string>>, global::System.Collections.Immutable.IImmutableSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableSet() => _getIImmutableSetHandler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    internal interface INeedsCollectionDefaultsSetup {
        SourceMock.Interfaces.IMockMethodSetup<System.Func<int[]>, int[]> GetArray();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.List<int>>, global::System.Collections.Generic.List<int>> GetList();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableList<int>>, global::System.Collections.Immutable.ImmutableList<int>> GetImmutableList();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableArray<int>>, global::System.Collections.Immutable.ImmutableArray<int>> GetImmutableArray();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IEnumerable<int>>, global::System.Collections.Generic.IEnumerable<int>> GetIEnumerable();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IList<int>>, global::System.Collections.Generic.IList<int>> GetIList();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.ICollection<int>>, global::System.Collections.Generic.ICollection<int>> GetICollection();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyCollection<int>>, global::System.Collections.Generic.IReadOnlyCollection<int>> GetIReadOnlyCollection();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyList<int>>, global::System.Collections.Generic.IReadOnlyList<int>> GetIReadOnlyList();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableList<int>>, global::System.Collections.Immutable.IImmutableList<int>> GetIImmutableList();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.Dictionary<string, string>>, global::System.Collections.Generic.Dictionary<string, string>> GetDictionary();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableDictionary<string, string>>, global::System.Collections.Immutable.ImmutableDictionary<string, string>> GetImmutableDictionary();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IDictionary<string, string>>, global::System.Collections.Generic.IDictionary<string, string>> GetIDictionary();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlyDictionary<string, string>>, global::System.Collections.Generic.IReadOnlyDictionary<string, string>> GetIReadOnlyDictionary();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableDictionary<string, string>>, global::System.Collections.Immutable.IImmutableDictionary<string, string>> GetIImmutableDictionary();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.HashSet<string>>, global::System.Collections.Generic.HashSet<string>> GetHashSet();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.ImmutableHashSet<string>>, global::System.Collections.Immutable.ImmutableHashSet<string>> GetImmutableHashSet();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.ISet<string>>, global::System.Collections.Generic.ISet<string>> GetISet();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Generic.IReadOnlySet<string>>, global::System.Collections.Generic.IReadOnlySet<string>> GetIReadOnlySet();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::System.Collections.Immutable.IImmutableSet<string>>, global::System.Collections.Immutable.IImmutableSet<string>> GetIImmutableSet();
    }

    internal interface INeedsCollectionDefaultsCalls {
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetArray();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetList();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetImmutableList();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetImmutableArray();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIEnumerable();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIList();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetICollection();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIReadOnlyCollection();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIReadOnlyList();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIImmutableList();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetDictionary();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetImmutableDictionary();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIDictionary();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIReadOnlyDictionary();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIImmutableDictionary();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetHashSet();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetImmutableHashSet();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetISet();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIReadOnlySet();
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> GetIImmutableSet();
    }
}