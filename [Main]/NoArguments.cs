namespace SourceMock {
    /// <summary>
    /// Represents argument list for methods that have no arguments.
    /// </summary>
    public class NoArguments {
        private NoArguments() {}
        /// <summary>
        /// Represents the singleton instance of <see cref="NoArguments" /> class. No other instances exist.
        /// </summary>
        public static NoArguments Value { get; } = new NoArguments();
    }
}
