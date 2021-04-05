using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceMock.Internal {
    public partial class MockMethodHandler {
        private readonly IList<IMockMethodSetupInternal> _setups = new List<IMockMethodSetupInternal>();
        private readonly IList<MockCall> _calls = new List<MockCall>();

        public IMockMethodSetup<TReturn> Setup<TReturn>(IReadOnlyList<Type>? genericArguments, IReadOnlyList<IMockArgumentMatcher>? arguments) {
            genericArguments ??= Array.Empty<Type>();
            arguments ??= Array.Empty<IMockArgumentMatcher>();
            var setup = new MockMethodSetup<TReturn>(genericArguments, arguments);
            _setups.Add(setup);

            return setup;
        }

        public TReturn Call<TReturn>(IReadOnlyList<Type>? genericArguments, IReadOnlyList<object?>? arguments) {
            genericArguments ??= Array.Empty<Type>();
            arguments ??= Array.Empty<object?>();
            var call = new MockCall(genericArguments, arguments);
            _calls.Add(call);

            // setups added later take priority
            var setup = _setups.LastOrDefault(s => s.Matches(call));
            return (setup != null ? ((MockMethodSetup<TReturn>)setup).Execute() : DefaultValue.Get<TReturn>())!;
        }

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
