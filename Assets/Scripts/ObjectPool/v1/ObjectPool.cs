using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    public List<T> pool;

    public ObjectPool(string ObjectName, Transform parent)
    {
        pool = new List<T>();
        GameObject obj = new GameObject(ObjectName + " Pool");
        obj.transform.parent = parent;
        _objectName = ObjectName;
        for (int i = 0; i < 10; i++)
        {
            pool.Add(Factory.instance.Creator<T>(_objectName, obj.transform));
            pool[i].gameObject.SetActive(false);
        }
    }

    private string _objectName;

    public T GetObject()
    {
        if (pool.Count > 0)
        {
            foreach (T obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
            var newObj = Factory.instance.Creator<T>(_objectName);
            pool.Add(newObj);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
        else
        {
            var newObj = Factory.instance.Creator<T>(_objectName);
            pool.Add(newObj);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
}
