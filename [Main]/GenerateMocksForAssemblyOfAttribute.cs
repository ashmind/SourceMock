using System;
using System.Text.RegularExpressions;

namespace SourceMock {
    /// <summary>
    /// Requests mock generation for all interfaces in the specified assembly.
    /// </summary>
    /// <remarks>
    /// When this attribute is used in a test project, SourceMock will generate
    /// mocks for all interfaces in the target assembly, and include those into
    /// the test project.
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

        /// <summary>
        /// Pattern of type names to exclude from mock generation.
        /// </summary>
        /// <remarks>
        /// Pattern is based on <see cref="Regex"/> syntax.
        /// </remarks>
        public string? ExcludeRegex { get; set; }
    }
}
