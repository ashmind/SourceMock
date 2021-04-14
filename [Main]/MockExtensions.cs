using System;
using System.Threading.Tasks;
using SourceMock.Interfaces;

/// <summary>
/// Provides a set of extension methods for SourceMock interfaces.
/// </summary>
public static class MockExtensions {
    /// <summary>
    /// Configures mocked method to throw the specified exception when called.
    /// </summary>
    /// <typeparam name="TException">The specific type of <see cref="Exception" /> to throw.</typeparam>
    /// <param name="setup">The method to configure.</param>
    public static void Throws<TException>(this IMockSetupThrows setup)
        where TException : Exception, new()
    {
        setup.Throws(new Exception());
    }

    /// <summary>
    /// Configures mocked method to return the specified value, wrapped in a <see cref="Task{T}" />.
    /// </summary>
    /// <param name="setup">The method to configure.</param>
    /// <param name="value">The value to return.</param>
    public static void ReturnsAsync<T>(this IMockSetupReturns<Task<T>> setup, T value) {
        setup.Returns(Task.FromResult(value));
    }
}