using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCards : Node
{
    public override void ExecuteNode()
    {
        if (EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.PlayCards();
        }
    }
}
