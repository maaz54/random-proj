using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// singleton instance
        /// </summary>
        private static T _instance;

        /// <summary>
        /// return singleton instance
        /// </summary>
        /// <value></value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
