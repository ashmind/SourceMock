using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SourceMock.Generators.Internal {
    // Note: SourceGenerator documentation explicitly does NOT recommend caching.
    // https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#participate-in-the-ide-experience
    // However I can't in good conscience release something that regenerates tons of files on each cursor move.
    internal class GeneratorCache<TKey, TValue>: IDisposable {
        private static readonly TimeSpan Expiry = TimeSpan.FromMinutes(10);

        private readonly string _name;
        private readonly ConcurrentDictionary<TKey, Entry> _dictionary;
        private readonly CancellationTokenSource _cleanupCancellationSource = new();
        #pragma warning disable IDE0052 // Remove unread private members
        private readonly Task _cleanupTask;
        #pragma warning restore IDE0052 // Remove unread private members

        public GeneratorCache(string name, IEqualityComparer<TKey>? comparer = null) {
            _dictionary = new(comparer ?? EqualityComparer<TKey>.Default);
            _cleanupTask = Task.Run(CleanupAsync);
            _name = name;
        }

        public bool TryGetValue(TKey key, out TValue? value) {
            var found = _dictionary.TryGetValue(key, out var entry);
            if (!found) {
                value = default;
                return false;
            }

            value = entry.Value;
            entry.LastAccessUtc = DateTime.UtcNow;
            return found;
        }

        public void TryAdd(TKey key, TValue value) {
            _dictionary.TryAdd(key, new Entry(value, DateTime.UtcNow));
        }

        private async Task CleanupAsync() {
            while (!_cleanupCancellationSource.IsCancellationRequested) {
                await Task.Delay(Expiry, _cleanupCancellationSource.Token).ConfigureAwait(false);

                GeneratorLog.Log($"Starting cache cleanup for {_name}. Size: {_dictionary.Count}");
                var cutoff = DateTime.UtcNow - Expiry;
                var keysToRemove = new List<TKey>();
                foreach (var pair in _dictionary) {
                    if (pair.Value.LastAccessUtc < cutoff)
                        keysToRemove.Add(pair.Key);
                }

                foreach (var key in keysToRemove) {
                    _dictionary.TryRemove(key, out _);
                }
                GeneratorLog.Log($"Finished cache cleanup for {_name}. Size: {_dictionary.Count}");
            }
        }

        private class Entry {
            public Entry(TValue value, DateTime lastAccessUtc) {
                Value = value;
                LastAccessUtc = lastAccessUtc;
            }

            public TValue Value { get; }
            // Thread safety does not matter for this one --
            // if we set it from multiple threads, the times
            // will be close enough for it not to matter who
            // wins.
            public DateTime LastAccessUtc { get; set; }
        }

        public void Dispose() {
            _cleanupCancellationSource.Cancel();
            _cleanupCancellationSource.Dispose();
        }
    }
}
