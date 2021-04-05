using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SourceMock {
    /// <summary>
    /// Provides a way to retrieve calls made to a mocked property that has a getter.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    public interface IMockPropertyCalls<T> {
        /// <summary>
        /// Provides a way to retrieve calls made to the mocked getter.
        /// </summary>
        /// <remarks>
        /// SourceMock does not currently support parametrized properties,
        /// so getter calls always use <see cref="NoArguments" />.
        /// </remarks>
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Aiming to match C# syntax")]
        IReadOnlyList<NoArguments> get { get; }
    }
}
