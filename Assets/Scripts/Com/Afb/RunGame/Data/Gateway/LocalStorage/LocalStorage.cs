using System;
using UnityEngine;

namespace Com.Afb.RunGame.Data.Gateway {
    public class LocalStorage : ILocalStorage {
        public T Get<T>(string key, T defaultValue = default(T)) {
            if (typeof(T) == typeof(int)) {
                return (T)(object)PlayerPrefs.GetInt(key, (int)(object)defaultValue);
            }
            else if (typeof(T) == typeof(float)) {
                return (T)(object)PlayerPrefs.GetFloat(key, (float)(object)defaultValue);
            }
            else if (typeof(T) == typeof(bool)) {
                return (T)(object)(PlayerPrefs.GetInt(key,
                    ((bool)(object)defaultValue) ? 1 : 0) == 1);
            }
            else if (typeof(T) == typeof(string)) {
                return (T)(object)PlayerPrefs.GetString(key, (string)(object)defaultValue);
            }
            else {
                throw new NotImplementedException();
                // TODO json string
            }
        }

        public void Set<T>(string key, T value) {
            if (typeof(T) == typeof(int)) {
                PlayerPrefs.SetInt(key, (int)(object)value);
            }
            else if (typeof(T) == typeof(float)) {
                PlayerPrefs.SetFloat(key, (float)(object)value);
            }
            else if (typeof(T) == typeof(bool)) {
                PlayerPrefs.SetInt(key,
                    ((bool)(object)value) ? 1 : 0);
            }
            else if (typeof(T) == typeof(string)) {
                PlayerPrefs.SetString(key, (string)(object)value);
            }
            else {
                throw new NotImplementedException();
                // TODO json string
            }
        }
    }
}
