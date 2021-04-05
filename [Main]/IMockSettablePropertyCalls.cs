using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SourceMock.Internal;

namespace SourceMock {
    /// <summary>
    /// Provides a way to retrieve calls made to a specific mocked property that has both getter and setter.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    public interface IMockSettablePropertyCalls<T> : IMockPropertyCalls<T> {
        /// <summary>
        /// Provides a way to retrieve assignments made to the mocked setter.
        /// </summary>
        /// <param name="value">The optional value to filter the assignments</param>
        /// <returns>
        /// Either all assignments made; or assignments that match <paramref name="value" />
        /// if <c>value</c> is specified.
        /// </returns>
        /// <remarks>
        /// SourceMock does not currently support parametrized properties,
        /// so setter calls always have one parameter of type <typeparamref name="T" />.
        /// </remarks>
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Aiming to match C# syntax")]
        IReadOnlyList<T> set(MockArgumentMatcher<T> value = default);
    }
}
