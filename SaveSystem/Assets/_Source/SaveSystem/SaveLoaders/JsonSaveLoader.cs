using System.IO;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class JsonSaveLoader : ISaveLoader
    {
        /// <param name="data"></param>
        /// <param name="key">Save path</param>
        public void Save<T>(T data, string key = null)
        {
            if(key == null || data == null)
                return;
            
            File.WriteAllText(key, JsonUtility.ToJson(data));
        }

        /// <param name="result"></param>
        /// <param name="key">Save path</param>
        public bool TryLoad<T>(out T result, string key = null)
        {
            if(key == null || !File.Exists(key))
            {
                result = default;
                return false;
            }

            var value = File.ReadAllText(key);
            result = JsonUtility.FromJson<T>(value); //TODO: add custom convertor system for complex classes
            return true;
        }
    }
}