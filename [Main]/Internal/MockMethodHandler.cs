using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceMock.Internal {
    public class MockMethodHandler {
        private readonly IList<IMockMethodSetup> _setups = new List<IMockMethodSetup>();
        private readonly IList<object?[]> _calls = new List<object?[]>();

        public IMockMethodSetup<TReturn> Setup<TReturn>(params IMockArgumentMatcher[] arguments) {
            var setup = new MockMethodSetup<TReturn>(arguments);
            _setups.Add(setup);
            return setup;
        }

        public TReturn Call<TReturn>(params object?[] arguments) {
            _calls.Add(arguments);

            var setup = _setups
                .Cast<MockMethodSetup<TReturn>>()
                .FirstOrDefault(s => ArgumentsMatch(arguments, s.Arguments));
            return (setup != null ? setup.Execute() : DefaultValue.Get<TReturn>())!;
        }

        public IReadOnlyList<T> Calls<T>(Func<object?[], T> convertResult, params IMockArgumentMatcher[] arguments) {
            return _calls
                .Where(c => ArgumentsMatch(c, arguments))
                .Select(convertResult)
                .Cast<T>()
                .ToList();
        }

        private bool ArgumentsMatch(object?[] arguments, IReadOnlyList<IMockArgumentMatcher> matchers) {
            return arguments.Zip(matchers, (a, m) => m.Matches(a)).All(m => m);
        }
    }
}
