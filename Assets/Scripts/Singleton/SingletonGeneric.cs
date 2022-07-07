using UnityEngine;

public class SingletonGeneric<T> : MonoBehaviour where T : SingletonGeneric<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = (T)this;
        }
    }
}
