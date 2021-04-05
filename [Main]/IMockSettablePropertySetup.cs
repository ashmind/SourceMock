using SourceMock.Internal;

namespace SourceMock {
    /// <summary>
    /// Provides a way to set up behavior for a mocked property that has both getter and setter.
    /// </summary>
    public interface IMockSettablePropertySetup<T> : IMockPropertySetup<T> {
        /// <summary>
        /// Provides a way to to set up behavior for the mocked setter.
        /// </summary>
        /// <param name="value">The property value to set up behavior for.</param>
        /// <returns>
        /// An <see cref="IMockMethodSetup" /> object to set up the setter behavior
        /// when assigned the given <paramref name="value"/>.
        /// </returns>
        IMockMethodSetup set(MockArgumentMatcher<T> value = default);
    }
}
