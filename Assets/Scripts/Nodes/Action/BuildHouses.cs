using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHouses : Node
{
    public override void ExecuteNode()
    {
        if(EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.BuildHouses();
        }
    }
}
