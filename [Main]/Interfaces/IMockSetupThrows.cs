using System;

namespace SourceMock.Interfaces {
    /// <summary>
    /// Provides a way to configure an exception to be thrown by a mocked method.
    /// </summary>
    public interface IMockSetupThrows {
        /// <summary>
        /// Configures mocked method to throw the specified exception when called.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> to throw.</param>
        void Throws(Exception exception);
    }
}
