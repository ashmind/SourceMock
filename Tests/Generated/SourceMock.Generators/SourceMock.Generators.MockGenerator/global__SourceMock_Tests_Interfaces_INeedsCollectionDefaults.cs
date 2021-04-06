#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsCollectionDefaultsMock : global::SourceMock.Tests.Interfaces.INeedsCollectionDefaults, INeedsCollectionDefaultsSetup, INeedsCollectionDefaultsCalls, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsCollectionDefaults> {
        public INeedsCollectionDefaultsSetup Setup => this;
        public INeedsCollectionDefaultsCalls Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _getArray1Handler = new();

        SourceMock.IMockMethodSetup<System.Action,int[]> INeedsCollectionDefaultsSetup.GetArray() => _getArray1Handler.Setup<System.Action, int[]>(null, null);
        public int[] GetArray() => _getArray1Handler.Call<System.Action, int[]>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetArray() => _getArray1Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getList2Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.List<int>> INeedsCollectionDefaultsSetup.GetList() => _getList2Handler.Setup<System.Action, global::System.Collections.Generic.List<int>>(null, null);
        public global::System.Collections.Generic.List<int> GetList() => _getList2Handler.Call<System.Action, global::System.Collections.Generic.List<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetList() => _getList2Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableList3Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableList<int>> INeedsCollectionDefaultsSetup.GetImmutableList() => _getImmutableList3Handler.Setup<System.Action, global::System.Collections.Immutable.ImmutableList<int>>(null, null);
        public global::System.Collections.Immutable.ImmutableList<int> GetImmutableList() => _getImmutableList3Handler.Call<System.Action, global::System.Collections.Immutable.ImmutableList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableList() => _getImmutableList3Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableArray4Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableArray<int>> INeedsCollectionDefaultsSetup.GetImmutableArray() => _getImmutableArray4Handler.Setup<System.Action, global::System.Collections.Immutable.ImmutableArray<int>>(null, null);
        public global::System.Collections.Immutable.ImmutableArray<int> GetImmutableArray() => _getImmutableArray4Handler.Call<System.Action, global::System.Collections.Immutable.ImmutableArray<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableArray() => _getImmutableArray4Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIEnumerable5Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IEnumerable<int>> INeedsCollectionDefaultsSetup.GetIEnumerable() => _getIEnumerable5Handler.Setup<System.Action, global::System.Collections.Generic.IEnumerable<int>>(null, null);
        public global::System.Collections.Generic.IEnumerable<int> GetIEnumerable() => _getIEnumerable5Handler.Call<System.Action, global::System.Collections.Generic.IEnumerable<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIEnumerable() => _getIEnumerable5Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIList6Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IList<int>> INeedsCollectionDefaultsSetup.GetIList() => _getIList6Handler.Setup<System.Action, global::System.Collections.Generic.IList<int>>(null, null);
        public global::System.Collections.Generic.IList<int> GetIList() => _getIList6Handler.Call<System.Action, global::System.Collections.Generic.IList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIList() => _getIList6Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getICollection7Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.ICollection<int>> INeedsCollectionDefaultsSetup.GetICollection() => _getICollection7Handler.Setup<System.Action, global::System.Collections.Generic.ICollection<int>>(null, null);
        public global::System.Collections.Generic.ICollection<int> GetICollection() => _getICollection7Handler.Call<System.Action, global::System.Collections.Generic.ICollection<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetICollection() => _getICollection7Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyCollection8Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyCollection<int>> INeedsCollectionDefaultsSetup.GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Setup<System.Action, global::System.Collections.Generic.IReadOnlyCollection<int>>(null, null);
        public global::System.Collections.Generic.IReadOnlyCollection<int> GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Call<System.Action, global::System.Collections.Generic.IReadOnlyCollection<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyList9Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyList<int>> INeedsCollectionDefaultsSetup.GetIReadOnlyList() => _getIReadOnlyList9Handler.Setup<System.Action, global::System.Collections.Generic.IReadOnlyList<int>>(null, null);
        public global::System.Collections.Generic.IReadOnlyList<int> GetIReadOnlyList() => _getIReadOnlyList9Handler.Call<System.Action, global::System.Collections.Generic.IReadOnlyList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyList() => _getIReadOnlyList9Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableList10Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableList<int>> INeedsCollectionDefaultsSetup.GetIImmutableList() => _getIImmutableList10Handler.Setup<System.Action, global::System.Collections.Immutable.IImmutableList<int>>(null, null);
        public global::System.Collections.Immutable.IImmutableList<int> GetIImmutableList() => _getIImmutableList10Handler.Call<System.Action, global::System.Collections.Immutable.IImmutableList<int>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableList() => _getIImmutableList10Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getDictionary11Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.Dictionary<string, string>> INeedsCollectionDefaultsSetup.GetDictionary() => _getDictionary11Handler.Setup<System.Action, global::System.Collections.Generic.Dictionary<string, string>>(null, null);
        public global::System.Collections.Generic.Dictionary<string, string> GetDictionary() => _getDictionary11Handler.Call<System.Action, global::System.Collections.Generic.Dictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetDictionary() => _getDictionary11Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableDictionary12Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableDictionary<string, string>> INeedsCollectionDefaultsSetup.GetImmutableDictionary() => _getImmutableDictionary12Handler.Setup<System.Action, global::System.Collections.Immutable.ImmutableDictionary<string, string>>(null, null);
        public global::System.Collections.Immutable.ImmutableDictionary<string, string> GetImmutableDictionary() => _getImmutableDictionary12Handler.Call<System.Action, global::System.Collections.Immutable.ImmutableDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableDictionary() => _getImmutableDictionary12Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIDictionary13Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIDictionary() => _getIDictionary13Handler.Setup<System.Action, global::System.Collections.Generic.IDictionary<string, string>>(null, null);
        public global::System.Collections.Generic.IDictionary<string, string> GetIDictionary() => _getIDictionary13Handler.Call<System.Action, global::System.Collections.Generic.IDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIDictionary() => _getIDictionary13Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlyDictionary14Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Setup<System.Action, global::System.Collections.Generic.IReadOnlyDictionary<string, string>>(null, null);
        public global::System.Collections.Generic.IReadOnlyDictionary<string, string> GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Call<System.Action, global::System.Collections.Generic.IReadOnlyDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableDictionary15Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableDictionary<string, string>> INeedsCollectionDefaultsSetup.GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Setup<System.Action, global::System.Collections.Immutable.IImmutableDictionary<string, string>>(null, null);
        public global::System.Collections.Immutable.IImmutableDictionary<string, string> GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Call<System.Action, global::System.Collections.Immutable.IImmutableDictionary<string, string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getHashSet16Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.HashSet<string>> INeedsCollectionDefaultsSetup.GetHashSet() => _getHashSet16Handler.Setup<System.Action, global::System.Collections.Generic.HashSet<string>>(null, null);
        public global::System.Collections.Generic.HashSet<string> GetHashSet() => _getHashSet16Handler.Call<System.Action, global::System.Collections.Generic.HashSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetHashSet() => _getHashSet16Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getImmutableHashSet17Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableHashSet<string>> INeedsCollectionDefaultsSetup.GetImmutableHashSet() => _getImmutableHashSet17Handler.Setup<System.Action, global::System.Collections.Immutable.ImmutableHashSet<string>>(null, null);
        public global::System.Collections.Immutable.ImmutableHashSet<string> GetImmutableHashSet() => _getImmutableHashSet17Handler.Call<System.Action, global::System.Collections.Immutable.ImmutableHashSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetImmutableHashSet() => _getImmutableHashSet17Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getISet18Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.ISet<string>> INeedsCollectionDefaultsSetup.GetISet() => _getISet18Handler.Setup<System.Action, global::System.Collections.Generic.ISet<string>>(null, null);
        public global::System.Collections.Generic.ISet<string> GetISet() => _getISet18Handler.Call<System.Action, global::System.Collections.Generic.ISet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetISet() => _getISet18Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIReadOnlySet19Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlySet<string>> INeedsCollectionDefaultsSetup.GetIReadOnlySet() => _getIReadOnlySet19Handler.Setup<System.Action, global::System.Collections.Generic.IReadOnlySet<string>>(null, null);
        public global::System.Collections.Generic.IReadOnlySet<string> GetIReadOnlySet() => _getIReadOnlySet19Handler.Call<System.Action, global::System.Collections.Generic.IReadOnlySet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIReadOnlySet() => _getIReadOnlySet19Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);

        private readonly SourceMock.Internal.MockMethodHandler _getIImmutableSet20Handler = new();

        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableSet<string>> INeedsCollectionDefaultsSetup.GetIImmutableSet() => _getIImmutableSet20Handler.Setup<System.Action, global::System.Collections.Immutable.IImmutableSet<string>>(null, null);
        public global::System.Collections.Immutable.IImmutableSet<string> GetIImmutableSet() => _getIImmutableSet20Handler.Call<System.Action, global::System.Collections.Immutable.IImmutableSet<string>>(null, null);
        System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> INeedsCollectionDefaultsCalls.GetIImmutableSet() => _getIImmutableSet20Handler.Calls(null, null, _ => SourceMock.NoArguments.Value);
    }

    public interface INeedsCollectionDefaultsSetup {
        SourceMock.IMockMethodSetup<System.Action,int[]> GetArray();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.List<int>> GetList();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableList<int>> GetImmutableList();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableArray<int>> GetImmutableArray();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IEnumerable<int>> GetIEnumerable();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IList<int>> GetIList();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.ICollection<int>> GetICollection();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyCollection<int>> GetIReadOnlyCollection();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyList<int>> GetIReadOnlyList();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableList<int>> GetIImmutableList();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.Dictionary<string, string>> GetDictionary();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableDictionary<string, string>> GetImmutableDictionary();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IDictionary<string, string>> GetIDictionary();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlyDictionary<string, string>> GetIReadOnlyDictionary();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableDictionary<string, string>> GetIImmutableDictionary();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.HashSet<string>> GetHashSet();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.ImmutableHashSet<string>> GetImmutableHashSet();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.ISet<string>> GetISet();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Generic.IReadOnlySet<string>> GetIReadOnlySet();
        SourceMock.IMockMethodSetup<System.Action,global::System.Collections.Immutable.IImmutableSet<string>> GetIImmutableSet();
    }

    public interface INeedsCollectionDefaultsCalls {
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