using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T Instance => m_instance;

    private void Awake()
    {
        _OnAwake();
        if (m_instance == null)
        {
            m_instance = (T)FindObjectOfType(typeof(T));
            
            if (m_instance == null)
            {
                var singletonObject = new GameObject();
                m_instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString();

                DontDestroyOnLoad(singletonObject);
            }
        }
    }

    protected virtual void _OnAwake() {}
}
