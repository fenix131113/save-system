using System;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class PlayerPrefsSaveLoader : ISaveLoader
    {
        /// <param name="data"></param>
        /// <param name="key">Save key</param>
        public void Save<T>(T data, string key = null)
        {
            if (key == null || data == null)
                return;

            if (typeof(T) == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)(object)data);
                return;
            }

            if (typeof(T) == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)(object)data);
                return;
            }

            if (typeof(T) == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)(object)data);
            }
            else
            {
                try
                {
                    var converter = SaveLoadConverterRegistry.GetConverter(typeof(T));
                    var serialized = converter == null ? JsonUtility.ToJson(data) : converter.Serialize(data);

                    PlayerPrefs.SetString(key, serialized);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error when trying to parse save {typeof(T)}: " + e);
                    throw;
                }
            }
        }

        /// <param name="result"></param>
        /// <param name="key">Save key</param>
        public bool TryLoad<T>(out T result, string key = null)
        {
            if (key == null || !PlayerPrefs.HasKey(key))
            {
                result = default;
                return false;
            }

            if (typeof(T) == typeof(float))
            {
                result = (T)(object)PlayerPrefs.GetFloat(key);
                return true;
            }

            if (typeof(T) == typeof(int))
            {
                result = (T)(object)PlayerPrefs.GetInt(key);
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                result = (T)(object)PlayerPrefs.GetString(key);
                return true;
            }

            try
            {
                var converter = SaveLoadConverterRegistry.GetConverter(typeof(T));
                var data = PlayerPrefs.GetString(key);

                var deserialized = converter == null ? JsonUtility.FromJson<T>(data) : (T)converter.Deserialize(data);

                result = deserialized;
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