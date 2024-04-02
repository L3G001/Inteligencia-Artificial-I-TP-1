using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : Node
{
    public override void ExecuteNode()
    {
        if (EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.Dance();
        }
    }
}
