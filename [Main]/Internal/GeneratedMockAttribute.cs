using System;

namespace SourceMock.Internal {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class GeneratedMockAttribute : Attribute {
    }
}
