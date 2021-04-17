using System;

namespace SourceMock {
    /// <summary>
    /// Requests mock generation for specified classes or interfaces.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateMocksForTypesAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMocksForTypesAttribute" /> class.
        /// </summary>
        /// <param name="types">Types to generate mocks for.</param>
        public GenerateMocksForTypesAttribute(params Type[] types) {
            Types = types;
        }

        internal Type[] Types { get; }
    }
}
