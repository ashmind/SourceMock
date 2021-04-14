using System;

namespace SourceMock.Interfaces {
    /// <summary>
    /// Provides a way to set up behavior for a mocked method.
    /// </summary>
    /// <typeparam name="TRun">The type of callback delegate passed into <see cref="IMockSetupRuns{TRun}.Runs(TRun)" />.</typeparam>
    public interface IMockMethodSetup<TRun> : IMockSetupThrows, IMockSetupRuns<TRun>
        where TRun : Delegate
    {
    }

    /// <summary>
    /// Provides a way to set up behavior for a mocked method that returns a value.
    /// </summary>
    /// <typeparam name="TReturn">The method return type.</typeparam>
    /// <typeparam name="TRun">The type of callback delegate passed into <see cref="IMockSetupRuns{TRun}.Runs(TRun)" />.</typeparam>
    public interface IMockMethodSetup<TRun, TReturn> : IMockMethodSetup<TRun>, IMockSetupReturns<TReturn>
        where TRun: Delegate
    {
    }
}
