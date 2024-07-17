using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    public KeyCode jumpKey, attackKey, pauseKey, interactKey, sprintKey;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }
}
