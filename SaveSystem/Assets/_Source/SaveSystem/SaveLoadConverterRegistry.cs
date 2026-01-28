using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveLoadConverterRegistry
    {
        private static Dictionary<Type, ISaveLoadConverter> _cache;

        private static bool _initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            if (_initialized) return;
            InitializeInternal();
            _initialized = true;
        }

        private static void InitializeInternal()
        {
            if (_initialized)
                return;

            _cache = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    typeof(ISaveLoadConverter).IsAssignableFrom(t))
                .Select(t => (ISaveLoadConverter)Activator.CreateInstance(t))
                .ToDictionary(
                    c => c.GetType().BaseType!.GetGenericArguments()[0],
                    c => c
                );

            _initialized = true;
        }

        public static ISaveLoadConverter GetConverter(Type type)
        {
            if (!_initialized)
                InitializeInternal();

            _cache.TryGetValue(type, out var converter);
            return converter;
        }
    }
}