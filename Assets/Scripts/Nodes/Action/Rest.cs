using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Node
{
    public override void ExecuteNode()
    {
        if(EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.GoToSleep();
        }
    }
}
