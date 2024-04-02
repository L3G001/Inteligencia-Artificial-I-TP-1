using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectDeactivator : MonoBehaviour
{
    private GameObject lastActiveChild;

    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                if (lastActiveChild != null && lastActiveChild != child.gameObject)
                {
                    lastActiveChild.SetActive(false);
                }
                lastActiveChild = child.gameObject;
            }
        }
    }
}
