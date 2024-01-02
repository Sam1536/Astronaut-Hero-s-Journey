using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamEbac.Core.Singleton
{

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            if (instance == null)
                instance = GetComponent<T>();
            else
            {
                Destroy(gameObject);
            }
        }
    }
}