using System;
using System.Collections.Generic;
using System.Linq;
using SourceMock.Interfaces;

namespace SourceMock.Internal {
    /// <summary>This type supports generated code and is not intended to be used directly.</summary>
    public class MockMethodHandler {
        private readonly IList<IMockCallMatcher> _setups = new List<IMockCallMatcher>();
        private readonly IList<MockCall> _calls = new List<MockCall>();

        /// <summary>This method supports generated code and is not intended to be used directly.</summary>
        public IMockMethodSetup<TRun, TReturn> Setup<TRun, TReturn>(IReadOnlyList<Type>? genericArguments, IReadOnlyList<IMockArgumentMatcher>? arguments) where TRun : Delegate  {
            genericArguments ??= Array.Empty<Type>();
            arguments ??= Array.Empty<IMockArgumentMatcher>();
            var setup = new MockMethodSetup<TRun, TReturn>(genericArguments, arguments);
            _setups.Add(setup);

            return setup;
        }

        /// <summary>This method supports generated code and is not intended to be used directly.</summary>
        // Important: arguments must be an array here to ensure that DynamicInvoke in setup.Execute returns out arguments in the same array
        public TReturn Call<TRun, TReturn>(IReadOnlyList<Type>? genericArguments, object?[]? arguments) where TRun : Delegate  {
            genericArguments ??= Array.Empty<Type>();
            arguments ??= Array.Empty<object?>();
            var call = new MockCall(genericArguments, arguments);
            _calls.Add(call);

            // setups added later take priority
            var setup = _setups.LastOrDefault(s => s.Matches(call));
            return (setup != null ? ((MockMethodSetup<TRun, TReturn>)setup).Execute(arguments) : DefaultValue.Get<TReturn>())!;
        }

        /// <summary>This method supports generated code and is not intended to be used directly.</summary>
        public IReadOnlyList<T> Calls<T>(
            IReadOnlyList<Type>? genericArguments,
            IReadOnlyList<IMockArgumentMatcher>? arguments,
            Func<IReadOnlyList<object?>, T> convertResult
        ) {
            genericArguments ??= Array.Empty<Type>();
            arguments ??= Array.Empty<IMockArgumentMatcher>();
            return _calls
                .Where(c => c.Matches(genericArguments, arguments))
                .Select(c => convertResult(c.Arguments))
                .Cast<T>()
                .ToList();
        }
    }
}
