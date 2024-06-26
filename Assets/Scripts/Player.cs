using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<float, float> EventRun;
    public static event Action EventWalk;

    public float addRadius, addAngle;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Programo que aumente la velocidad y corra.
            EventRun?.Invoke(addRadius, addAngle);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Programo que vuelva a la velocidad normal.
            EventWalk?.Invoke();
        }
    }
}
