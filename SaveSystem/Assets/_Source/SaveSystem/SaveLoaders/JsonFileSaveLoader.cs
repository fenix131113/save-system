using System;
using System.IO;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class JsonFileSaveLoader : ISaveLoader
    {
        public void Save<T>(T data, string key = null)
        {
            if (key == null || data == null)
                return;

            var converter = SaveLoadConverterRegistry.GetConverter(typeof(T));

            try
            {
                var serialized = converter != null
                    ? converter.Serialize(data)
                    : JsonUtility.ToJson(data);
                
                File.WriteAllText(key, serialized);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when trying to parse save {typeof(T)}: " + e);
                throw;
            }
        }

        public bool TryLoad<T>(out T result, string key = null)
        {
            if (key == null || !File.Exists(key))
            {
                result = default;
                return false;
            }

            var text = File.ReadAllText(key);
            var converter = SaveLoadConverterRegistry.GetConverter(typeof(T));

            try
            {
                result = converter != null
                    ? (T)converter.Deserialize(text)
                    : JsonUtility.FromJson<T>(text);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when trying to parse load {typeof(T)}: " + e);
                throw;
            }
            
            return true;
        }
    }
}