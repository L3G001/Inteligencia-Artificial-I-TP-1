using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    void Start()
    {
        GameManager.instance._inputReader.InteractEvent += HandleInteract;
    }

    void Update()
    {
        
    }

    void HandleInteract()
    {

    }
}
