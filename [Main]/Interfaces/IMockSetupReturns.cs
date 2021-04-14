namespace SourceMock.Interfaces {
    /// <summary>
    /// Provides a way to configure a return value for a mocked method or property.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return value</typeparam>
    public interface IMockSetupReturns<TReturn> {
        /// <summary>
        /// Configures mock to return the specified value when called.
        /// </summary>
        /// <param name="value">The value to return.</param>
        void Returns(TReturn value);
    }
}
