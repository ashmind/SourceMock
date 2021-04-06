using System;

namespace SourceMock {
    /// <summary>
    /// Provides a way to set up behavior for a mocked property that has a getter.
    /// </summary>
    public interface IMockPropertySetup<T> {
        /// <summary>
        /// Provides a way to to set up behavior for the mocked getter.
        /// </summary>
        public IMockMethodSetup<Action<T>, T> get { get; }

        /// <summary>
        /// Configures mocked getter to return the specified value when called.
        /// </summary>
        /// <param name="value">The value to return.</param>
        /// <remarks>
        /// This is a shortcut for <code>get.Returns</code>.
        /// </remarks>
        void Returns(T value);
    }
}
