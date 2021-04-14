using System;

namespace SourceMock.Interfaces {
    /// <summary>
    /// Provides a way to set up behavior for a mocked property that has a getter.
    /// </summary>
    public interface IMockPropertySetup<T> : IMockSetupReturns<T> {
        /// <summary>
        /// Provides a way to to set up behavior for the mocked getter.
        /// </summary>
        public IMockMethodSetup<Func<T>, T> get { get; }
    }
}
