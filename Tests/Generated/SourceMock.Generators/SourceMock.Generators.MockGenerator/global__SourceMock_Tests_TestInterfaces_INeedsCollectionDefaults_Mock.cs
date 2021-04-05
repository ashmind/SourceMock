#nullable enable
namespace SourceMock.Tests.TestInterfaces.Mocks {
    public static class global__SourceMock_Tests_TestInterfaces_INeedsCollectionDefaults_Mock {
        public class Instance : global::SourceMock.Tests.TestInterfaces.INeedsCollectionDefaults, ISetup, ICalls {
            public ISetup Setup => this;
            public ICalls Calls => this;

            private readonly SourceMock.Internal.MockMethodHandler<int[]> _getArray1Handler = new();
            SourceMock.IMockMethodSetup<int[]> ISetup.GetArray() => _getArray1Handler.Setup();
            public int[] GetArray() => _getArray1Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetArray() => _getArray1Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.List<int>> _getList2Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.List<int>> ISetup.GetList() => _getList2Handler.Setup();
            public global::System.Collections.Generic.List<int> GetList() => _getList2Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetList() => _getList2Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.ImmutableList<int>> _getImmutableList3Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableList<int>> ISetup.GetImmutableList() => _getImmutableList3Handler.Setup();
            public global::System.Collections.Immutable.ImmutableList<int> GetImmutableList() => _getImmutableList3Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetImmutableList() => _getImmutableList3Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.ImmutableArray<int>> _getImmutableArray4Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableArray<int>> ISetup.GetImmutableArray() => _getImmutableArray4Handler.Setup();
            public global::System.Collections.Immutable.ImmutableArray<int> GetImmutableArray() => _getImmutableArray4Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetImmutableArray() => _getImmutableArray4Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IEnumerable<int>> _getIEnumerable5Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IEnumerable<int>> ISetup.GetIEnumerable() => _getIEnumerable5Handler.Setup();
            public global::System.Collections.Generic.IEnumerable<int> GetIEnumerable() => _getIEnumerable5Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIEnumerable() => _getIEnumerable5Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IList<int>> _getIList6Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IList<int>> ISetup.GetIList() => _getIList6Handler.Setup();
            public global::System.Collections.Generic.IList<int> GetIList() => _getIList6Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIList() => _getIList6Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.ICollection<int>> _getICollection7Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.ICollection<int>> ISetup.GetICollection() => _getICollection7Handler.Setup();
            public global::System.Collections.Generic.ICollection<int> GetICollection() => _getICollection7Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetICollection() => _getICollection7Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IReadOnlyCollection<int>> _getIReadOnlyCollection8Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyCollection<int>> ISetup.GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Setup();
            public global::System.Collections.Generic.IReadOnlyCollection<int> GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIReadOnlyCollection() => _getIReadOnlyCollection8Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IReadOnlyList<int>> _getIReadOnlyList9Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyList<int>> ISetup.GetIReadOnlyList() => _getIReadOnlyList9Handler.Setup();
            public global::System.Collections.Generic.IReadOnlyList<int> GetIReadOnlyList() => _getIReadOnlyList9Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIReadOnlyList() => _getIReadOnlyList9Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.IImmutableList<int>> _getIImmutableList10Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableList<int>> ISetup.GetIImmutableList() => _getIImmutableList10Handler.Setup();
            public global::System.Collections.Immutable.IImmutableList<int> GetIImmutableList() => _getIImmutableList10Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIImmutableList() => _getIImmutableList10Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.Dictionary<string, string>> _getDictionary11Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.Dictionary<string, string>> ISetup.GetDictionary() => _getDictionary11Handler.Setup();
            public global::System.Collections.Generic.Dictionary<string, string> GetDictionary() => _getDictionary11Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetDictionary() => _getDictionary11Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.ImmutableDictionary<string, string>> _getImmutableDictionary12Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableDictionary<string, string>> ISetup.GetImmutableDictionary() => _getImmutableDictionary12Handler.Setup();
            public global::System.Collections.Immutable.ImmutableDictionary<string, string> GetImmutableDictionary() => _getImmutableDictionary12Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetImmutableDictionary() => _getImmutableDictionary12Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IDictionary<string, string>> _getIDictionary13Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IDictionary<string, string>> ISetup.GetIDictionary() => _getIDictionary13Handler.Setup();
            public global::System.Collections.Generic.IDictionary<string, string> GetIDictionary() => _getIDictionary13Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIDictionary() => _getIDictionary13Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IReadOnlyDictionary<string, string>> _getIReadOnlyDictionary14Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyDictionary<string, string>> ISetup.GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Setup();
            public global::System.Collections.Generic.IReadOnlyDictionary<string, string> GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIReadOnlyDictionary() => _getIReadOnlyDictionary14Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.IImmutableDictionary<string, string>> _getIImmutableDictionary15Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableDictionary<string, string>> ISetup.GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Setup();
            public global::System.Collections.Immutable.IImmutableDictionary<string, string> GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIImmutableDictionary() => _getIImmutableDictionary15Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.HashSet<string>> _getHashSet16Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.HashSet<string>> ISetup.GetHashSet() => _getHashSet16Handler.Setup();
            public global::System.Collections.Generic.HashSet<string> GetHashSet() => _getHashSet16Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetHashSet() => _getHashSet16Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.ImmutableHashSet<string>> _getImmutableHashSet17Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableHashSet<string>> ISetup.GetImmutableHashSet() => _getImmutableHashSet17Handler.Setup();
            public global::System.Collections.Immutable.ImmutableHashSet<string> GetImmutableHashSet() => _getImmutableHashSet17Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetImmutableHashSet() => _getImmutableHashSet17Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.ISet<string>> _getISet18Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.ISet<string>> ISetup.GetISet() => _getISet18Handler.Setup();
            public global::System.Collections.Generic.ISet<string> GetISet() => _getISet18Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetISet() => _getISet18Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Generic.IReadOnlySet<string>> _getIReadOnlySet19Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlySet<string>> ISetup.GetIReadOnlySet() => _getIReadOnlySet19Handler.Setup();
            public global::System.Collections.Generic.IReadOnlySet<string> GetIReadOnlySet() => _getIReadOnlySet19Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIReadOnlySet() => _getIReadOnlySet19Handler.Calls(_ => SourceMock.NoArguments.Value);

            private readonly SourceMock.Internal.MockMethodHandler<global::System.Collections.Immutable.IImmutableSet<string>> _getIImmutableSet20Handler = new();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableSet<string>> ISetup.GetIImmutableSet() => _getIImmutableSet20Handler.Setup();
            public global::System.Collections.Immutable.IImmutableSet<string> GetIImmutableSet() => _getIImmutableSet20Handler.Call();
            System.Collections.Generic.IReadOnlyList<SourceMock.NoArguments> ICalls.GetIImmutableSet() => _getIImmutableSet20Handler.Calls(_ => SourceMock.NoArguments.Value);

        }


        public interface ISetup {
            SourceMock.IMockMethodSetup<int[]> GetArray();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.List<int>> GetList();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableList<int>> GetImmutableList();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableArray<int>> GetImmutableArray();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IEnumerable<int>> GetIEnumerable();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IList<int>> GetIList();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.ICollection<int>> GetICollection();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyCollection<int>> GetIReadOnlyCollection();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyList<int>> GetIReadOnlyList();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableList<int>> GetIImmutableList();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.Dictionary<string, string>> GetDictionary();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableDictionary<string, string>> GetImmutableDictionary();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IDictionary<string, string>> GetIDictionary();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlyDictionary<string, string>> GetIReadOnlyDictionary();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableDictionary<string, string>> GetIImmutableDictionary();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.HashSet<string>> GetHashSet();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.ImmutableHashSet<string>> GetImmutableHashSet();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.ISet<string>> GetISet();
            SourceMock.IMockMethodSetup<global::System.Collections.Generic.IReadOnlySet<string>> GetIReadOnlySet();
            SourceMock.IMockMethodSetup<global::System.Collections.Immutable.IImmutableSet<string>> GetIImmutableSet();
        }

        public interface ICalls {
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

        public static global__SourceMock_Tests_TestInterfaces_INeedsCollectionDefaults_Mock.Instance Get(this SourceMock.Mock<global::SourceMock.Tests.TestInterfaces.INeedsCollectionDefaults> _) => new();
    }
}