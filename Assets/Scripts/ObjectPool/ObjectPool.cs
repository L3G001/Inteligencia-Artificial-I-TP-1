using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public ObjectPool(string ObjectName)
    { 
        _objectName = ObjectName;
        for (int i = 0; i < 10; i++)
        {
            pool.Add(Factory.instance.Creator<T>(_objectName));
            pool[i].gameObject.SetActive(false);
        }
    }

    public List<T> pool = new List<T>();
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
