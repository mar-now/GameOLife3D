using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("More intances of " + typeof(T).Name + "found.");
            Destroy(this);
        }
        else
            Instance = this as T;
    }
}
