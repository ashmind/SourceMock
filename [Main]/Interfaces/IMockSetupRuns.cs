using System;

namespace SourceMock.Interfaces {
    /// <summary>
    /// Provides a way to configure custom behavior for a mocked method.
    /// </summary>
    /// <typeparam name="TRun">The type of the callback delegate passed into <see cref="Runs(TRun)" />.</typeparam>
    public interface IMockSetupRuns<TRun>
        where TRun: Delegate
    {
        /// <summary>
        /// Configures mocked method to run a specific callback delegate.
        /// </summary>
        /// <param name="run">The callback delegate to run.</param>
        void Runs(TRun run);
    }
}
