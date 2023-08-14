#nullable enable
namespace Microsoft.Extensions.Logging.Mocks {
    internal class LoggerMock<TCategoryName> : global::Microsoft.Extensions.Logging.ILogger<TCategoryName>, ILoggerSetup<TCategoryName>, ILoggerCalls<TCategoryName>, SourceMock.IMock<global::Microsoft.Extensions.Logging.ILogger<TCategoryName>> {
        public ILoggerSetup<TCategoryName> Setup => this;
        public ILoggerCalls<TCategoryName> Calls => this;

        private readonly SourceMock.Internal.MockMethodHandler _logHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<LoggerDelegates<TCategoryName>.LogAction<TState>> ILoggerSetup<TCategoryName>.Log<TState>(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel, SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.EventId> eventId, SourceMock.Internal.MockArgumentMatcher<TState> state, SourceMock.Internal.MockArgumentMatcher<global::System.Exception?> exception, SourceMock.Internal.MockArgumentMatcher<global::System.Func<TState, global::System.Exception?, string>> formatter) => _logHandler.Setup<LoggerDelegates<TCategoryName>.LogAction<TState>, SourceMock.Internal.VoidReturn>(new[] { typeof(TState) }, new SourceMock.Internal.IMockArgumentMatcher[] { logLevel, eventId, state, exception, formatter });
        public void Log<TState>(global::Microsoft.Extensions.Logging.LogLevel logLevel, global::Microsoft.Extensions.Logging.EventId eventId, TState state, global::System.Exception? exception, global::System.Func<TState, global::System.Exception?, string> formatter) => _logHandler.Call<LoggerDelegates<TCategoryName>.LogAction<TState>, SourceMock.Internal.VoidReturn>(new[] { typeof(TState) }, new object?[] { logLevel, eventId, state, exception, formatter });
        System.Collections.Generic.IReadOnlyList<(global::Microsoft.Extensions.Logging.LogLevel logLevel, global::Microsoft.Extensions.Logging.EventId eventId, TState state, global::System.Exception? exception, global::System.Func<TState, global::System.Exception?, string> formatter)> ILoggerCalls<TCategoryName>.Log<TState>(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel, SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.EventId> eventId, SourceMock.Internal.MockArgumentMatcher<TState> state, SourceMock.Internal.MockArgumentMatcher<global::System.Exception?> exception, SourceMock.Internal.MockArgumentMatcher<global::System.Func<TState, global::System.Exception?, string>> formatter) => _logHandler.Calls(new[] { typeof(TState) }, new SourceMock.Internal.IMockArgumentMatcher[] { logLevel, eventId, state, exception, formatter }, args => ((global::Microsoft.Extensions.Logging.LogLevel)args[0]!, (global::Microsoft.Extensions.Logging.EventId)args[1]!, (TState)args[2]!, (global::System.Exception?)args[3], (global::System.Func<TState, global::System.Exception?, string>)args[4]!));

        private readonly SourceMock.Internal.MockMethodHandler _isEnabledHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::Microsoft.Extensions.Logging.LogLevel, bool>, bool> ILoggerSetup<TCategoryName>.IsEnabled(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel) => _isEnabledHandler.Setup<System.Func<global::Microsoft.Extensions.Logging.LogLevel, bool>, bool>(null, new SourceMock.Internal.IMockArgumentMatcher[] { logLevel });
        public bool IsEnabled(global::Microsoft.Extensions.Logging.LogLevel logLevel) => _isEnabledHandler.Call<System.Func<global::Microsoft.Extensions.Logging.LogLevel, bool>, bool>(null, new object?[] { logLevel });
        System.Collections.Generic.IReadOnlyList<global::Microsoft.Extensions.Logging.LogLevel> ILoggerCalls<TCategoryName>.IsEnabled(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel) => _isEnabledHandler.Calls(null, new SourceMock.Internal.IMockArgumentMatcher[] { logLevel }, args => ((global::Microsoft.Extensions.Logging.LogLevel)args[0]!));

        private readonly SourceMock.Internal.MockMethodHandler _beginScopeHandler = new();
        SourceMock.Interfaces.IMockMethodSetup<LoggerDelegates<TCategoryName>.BeginScopeFunc<TState>, global::System.IDisposable?> ILoggerSetup<TCategoryName>.BeginScope<TState>(SourceMock.Internal.MockArgumentMatcher<TState> state) => _beginScopeHandler.Setup<LoggerDelegates<TCategoryName>.BeginScopeFunc<TState>, global::System.IDisposable?>(new[] { typeof(TState) }, new SourceMock.Internal.IMockArgumentMatcher[] { state });
        public global::System.IDisposable? BeginScope<TState>(TState state)
            where TState: notnull
        => _beginScopeHandler.Call<LoggerDelegates<TCategoryName>.BeginScopeFunc<TState>, global::System.IDisposable?>(new[] { typeof(TState) }, new object?[] { state });
        System.Collections.Generic.IReadOnlyList<TState> ILoggerCalls<TCategoryName>.BeginScope<TState>(SourceMock.Internal.MockArgumentMatcher<TState> state) => _beginScopeHandler.Calls(new[] { typeof(TState) }, new SourceMock.Internal.IMockArgumentMatcher[] { state }, args => ((TState)args[0]!));
    }

    internal static class LoggerDelegates<TCategoryName> {
        public delegate void LogAction<TState>(global::Microsoft.Extensions.Logging.LogLevel logLevel, global::Microsoft.Extensions.Logging.EventId eventId, TState state, global::System.Exception? exception, global::System.Func<TState, global::System.Exception?, string> formatter);
        public delegate global::System.IDisposable? BeginScopeFunc<TState>(TState state);
    }

    internal interface ILoggerSetup<TCategoryName> {
        SourceMock.Interfaces.IMockMethodSetup<LoggerDelegates<TCategoryName>.LogAction<TState>> Log<TState>(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel = default, SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.EventId> eventId = default, SourceMock.Internal.MockArgumentMatcher<TState> state = default, SourceMock.Internal.MockArgumentMatcher<global::System.Exception?> exception = default, SourceMock.Internal.MockArgumentMatcher<global::System.Func<TState, global::System.Exception?, string>> formatter = default);
        SourceMock.Interfaces.IMockMethodSetup<System.Func<global::Microsoft.Extensions.Logging.LogLevel, bool>, bool> IsEnabled(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel = default);
        SourceMock.Interfaces.IMockMethodSetup<LoggerDelegates<TCategoryName>.BeginScopeFunc<TState>, global::System.IDisposable?> BeginScope<TState>(SourceMock.Internal.MockArgumentMatcher<TState> state = default)
            where TState: notnull;
    }

    internal interface ILoggerCalls<TCategoryName> {
        System.Collections.Generic.IReadOnlyList<(global::Microsoft.Extensions.Logging.LogLevel logLevel, global::Microsoft.Extensions.Logging.EventId eventId, TState state, global::System.Exception? exception, global::System.Func<TState, global::System.Exception?, string> formatter)> Log<TState>(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel = default, SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.EventId> eventId = default, SourceMock.Internal.MockArgumentMatcher<TState> state = default, SourceMock.Internal.MockArgumentMatcher<global::System.Exception?> exception = default, SourceMock.Internal.MockArgumentMatcher<global::System.Func<TState, global::System.Exception?, string>> formatter = default);
        System.Collections.Generic.IReadOnlyList<global::Microsoft.Extensions.Logging.LogLevel> IsEnabled(SourceMock.Internal.MockArgumentMatcher<global::Microsoft.Extensions.Logging.LogLevel> logLevel = default);
        System.Collections.Generic.IReadOnlyList<TState> BeginScope<TState>(SourceMock.Internal.MockArgumentMatcher<TState> state = default)
            where TState: notnull;
    }
}