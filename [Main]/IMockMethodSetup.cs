using System;

namespace SourceMock {
    /// <summary>
    /// Provides a way to set up behavior for a mocked method.
    /// </summary>
    public interface IMockMethodSetup {
        /// <summary>
        /// Configures mocked method to throw the specified exception when called.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> to throw.</param>
        void Throws(Exception exception);
    }

    /// <summary>
    /// Provides a way to set up behavior for a mocked method that returns a value.
    /// </summary>
    /// <typeparam name="TRun">The delegate that gets invoked when the method is called</typeparam>
    public interface IMockMethodSetup<TRun> : IMockMethodSetup where TRun : Delegate  {
        /// <summary>
        /// Configures mocked method to execute the delegate and return the value
        /// </summary>
        void Runs(TRun callback);
    }

    /// <summary>
    /// Provides a way to set up behavior for a mocked method that returns a value.
    /// </summary>
    /// <typeparam name="TReturn">The method return type.</typeparam>
    /// <typeparam name="TRun">The delegate that gets invoked when the method is called</typeparam>
    public interface IMockMethodSetup<TRun, TReturn> : IMockMethodSetup<TRun> where TRun : Delegate  {
        /// <summary>
        /// Configures mocked method to return the specified value when called.
        /// </summary>
        /// <param name="value">The value to return.</param>
        void Returns(TReturn value);
    }
}
