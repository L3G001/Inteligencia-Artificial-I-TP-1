using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance;

    public GameObject _interactUI;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }
}
