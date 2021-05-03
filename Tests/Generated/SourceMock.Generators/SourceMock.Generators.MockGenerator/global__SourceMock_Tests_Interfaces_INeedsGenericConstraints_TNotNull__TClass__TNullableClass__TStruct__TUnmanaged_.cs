#nullable enable
namespace SourceMock.Tests.Interfaces.Mocks {
    public class NeedsGenericConstraintsMock<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged> : global::SourceMock.Tests.Interfaces.INeedsGenericConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>, INeedsGenericConstraintsSetup<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>, INeedsGenericConstraintsCalls<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>, SourceMock.IMock<global::SourceMock.Tests.Interfaces.INeedsGenericConstraints<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged>>
        where TNotNull: notnull, global::SourceMock.Tests.Interfaces.IMockable, new()
        where TClass: class, global::SourceMock.Tests.Interfaces.IMockable, new()
        where TNullableClass: class?, global::SourceMock.Tests.Interfaces.IMockable, new()
        where TStruct: struct, global::SourceMock.Tests.Interfaces.IMockable
        where TUnmanaged: unmanaged, global::SourceMock.Tests.Interfaces.IMockable
    {
        public INeedsGenericConstraintsSetup<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged> Setup => this;
        public INeedsGenericConstraintsCalls<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged> Calls => this;
    }

    public interface INeedsGenericConstraintsSetup<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged> {
    }

    public interface INeedsGenericConstraintsCalls<TNotNull, TClass, TNullableClass, TStruct, TUnmanaged> {
    }
}