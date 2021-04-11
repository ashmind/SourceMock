using System.Diagnostics;
using Roslyn.Utilities;
using System.Threading;
using System.Runtime.CompilerServices;
#if DEBUG
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
#endif

namespace SourceMock.Generators.Internal {
    internal static class GeneratorLog {
        #if DEBUG
        private static readonly DateTime _start;
        private static readonly Stopwatch _stopwatch;
        
        private static readonly CancellationTokenSource _logTaskCancellationSource;
        private static readonly Task? _logTask;
        private static readonly ConcurrentQueue<(DateTime date, string message)> _entries = new();

        static GeneratorLog() {
            static string GetLogPath([CallerFilePath] string path = "") {
                using var process = Process.GetCurrentProcess();
                var now = DateTime.Now;
                var fileName = $"{Path.GetFileName(process.MainModule.FileName)} ({process.Id}) {now:HH_mm_ss.ffffff}.log";
                return Path.Combine(Path.GetDirectoryName(path), "..", "Logs", $"{now:MMM dd}", fileName);
            }

            // Note that the time will drift over time (https://nima-ara-blog.azurewebsites.net/high-resolution-clock-in-net/),
            // but for debug scenarios that's totally fine
            _start = DateTime.Now;
            _stopwatch = Stopwatch.StartNew();

            var logPath = GetLogPath();
            Directory.CreateDirectory(Path.GetDirectoryName(logPath));

            _logTaskCancellationSource = new CancellationTokenSource();
            _logTask = Task.Run(async () => {
                while (!_logTaskCancellationSource.IsCancellationRequested) {
                    if (!_entries.IsEmpty) {
                        using var streamWriter = new StreamWriter(logPath, append: true);
                        while (_entries.TryDequeue(out var entry)) {
                            streamWriter.WriteLine($"[{entry.date:HH.mm.ss.fff}] {entry.message}");
                        }
                    }
                    await Task.Delay(100, _logTaskCancellationSource.Token).ConfigureAwait(false);
                }
            });

            Log($"Starting log from {typeof(GeneratorLog).Assembly.Location}");
            AppDomain.CurrentDomain.ProcessExit += (_, _) => {
                Log("Stopping log on process exit");
                SpinWait.SpinUntil(() => _entries.IsEmpty, 500);
                _logTaskCancellationSource.Cancel();
                SpinWait.SpinUntil(() => _logTask.IsCompleted, 500);
            };
        }
        #endif

        [Conditional("DEBUG")]
        [PerformanceSensitive("")]
        public static void Log(string message) {
            #if DEBUG
            _entries.Enqueue((_start.AddTicks(_stopwatch.ElapsedTicks), message));
            #endif
        }
    }
}
