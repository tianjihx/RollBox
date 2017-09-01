using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{

    static protected T _instance;
    static public T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    return _instance;
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = gameObject.GetComponent<T>();
    }


}
