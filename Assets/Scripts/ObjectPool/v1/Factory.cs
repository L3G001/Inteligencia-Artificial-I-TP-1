using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory instance;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    public T Creator<T>(string ObjectName) where T : MonoBehaviour
    {
        return Object.Instantiate(Resources.Load<T>(ObjectName));
    }

    public T Creator<T>(string ObjectName, Transform parent) where T : MonoBehaviour
    {
        return Object.Instantiate(Resources.Load<T>(ObjectName), parent);
    }
}
