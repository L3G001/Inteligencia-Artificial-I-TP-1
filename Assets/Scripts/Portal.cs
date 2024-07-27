using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] string _sceneToLoad = default;

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.LoadScene(_sceneToLoad);
    }
}
