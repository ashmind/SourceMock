using System;

namespace SourceMock {
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false)]
    public class GenerateMockAttribute : Attribute {
    }
}
