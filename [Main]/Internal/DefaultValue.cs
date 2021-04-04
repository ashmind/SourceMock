using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SourceMock.Internal {
    internal static class DefaultValue {
        private static readonly IDictionary<Type, Type> MutableCollectionTypes = MapCollectionInterfacesToClassTypes(
            typeof(List<>), typeof(Dictionary<,>), typeof(HashSet<>)
        );
        private static readonly IDictionary<Type, Type> ImmutableCollectionTypes = MapCollectionInterfacesToClassTypes(
            typeof(ImmutableList<>), typeof(ImmutableDictionary<,>), typeof(ImmutableHashSet<>)
        );

        public static T? Get<T>() {
            return (T?)GetStandard(typeof(T));
        }

        private static object? GetStandard(Type type) {
            if (type.IsValueType) {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImmutableArray<>))
                    return type.GetField(nameof(ImmutableArray<object>.Empty)).GetValue(null);

                return Activator.CreateInstance(type);
            }

            if (type.IsArray)
                return Array.CreateInstance(type.GetElementType(), 0);

            if (type.IsGenericType) {
                var definition = type.GetGenericTypeDefinition();
                var arguments = type.GetGenericArguments();
                if (MutableCollectionTypes.TryGetValue(definition, out var classType))
                    return Activator.CreateInstance(classType.MakeGenericType(arguments));
                if (ImmutableCollectionTypes.TryGetValue(definition, out classType))
                    return classType.MakeGenericType(arguments).GetField("Empty").GetValue(null);
            }

            return null;
        }

        private static IDictionary<Type, Type> MapCollectionInterfacesToClassTypes(params Type[] classTypes) {
            var results = new Dictionary<Type, Type>();
            foreach (var classType in classTypes) {
                var classTypeDefinition = classType.GetGenericTypeDefinition();
                results.Add(classTypeDefinition, classTypeDefinition);

                var classTypeArgumentCount = classType.GetGenericArguments().Length;
                foreach (var interfaceType in classType.GetInterfaces()) {
                    if (!interfaceType.IsGenericType)
                        continue;

                    var interfaceTypeDefinition = interfaceType.GetGenericTypeDefinition();
                    if (results.ContainsKey(interfaceTypeDefinition))
                        continue;

                    if (interfaceType.GetGenericArguments().Length != classTypeArgumentCount)
                        continue;

                    results.Add(interfaceTypeDefinition, classTypeDefinition);
                }
            }
            return results;
        }
    }
}
