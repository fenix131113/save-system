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
                PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
            }
        }

        /// <param name="result"></param>
        /// <param name="key">Save key</param>
        public bool TryLoad<T>(out T result, string key = null) //TODO: add custom convertor system for complex classes
        {
            if (key == null)
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

            result = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            
            return true;
        }
    }
}