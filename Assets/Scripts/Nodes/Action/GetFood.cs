using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : Node
{
    public override void ExecuteNode()
    {
        if(EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.GetFood();
        }
    }
}
