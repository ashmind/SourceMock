using System;

namespace SourceMock {
    /// <summary>Requests mock generation for a specific assembly.</summary>
    /// <remarks>
    /// When this attribute is used in a test assembly, SourceMock will generate
    /// mocks for all interfaces in the target assembly and include those into
    /// the test assembly.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateMocksForAssemblyOfAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMocksForAssemblyOfAttribute" /> class.
        /// </summary>
        /// <param name="anyTypeInTargetAssembly">
        /// Any type in the target assembly.
        /// </param>
        public GenerateMocksForAssemblyOfAttribute(Type anyTypeInTargetAssembly) {
            AnyTypeInTargetAssembly = anyTypeInTargetAssembly;
        }

        internal Type AnyTypeInTargetAssembly { get; }
    }
}
