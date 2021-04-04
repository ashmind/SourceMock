using System;

namespace SourceMock {
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateMocksForAssemblyOfAttribute : Attribute {
        public GenerateMocksForAssemblyOfAttribute(Type anyTypeInTargetAssembly) {
            AnyTypeInTargetAssembly = anyTypeInTargetAssembly;
        }

        public Type AnyTypeInTargetAssembly { get; }
    }
}
