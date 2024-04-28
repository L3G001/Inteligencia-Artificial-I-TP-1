using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    Hunter myAgent;

    public Idle(Hunter myAgent)
    {
        this.myAgent = myAgent;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
       if(GameManager.Instance.hunterConfig.hunterCurrentFuel < GameManager.Instance.hunterConfig.hunterMaxFuel)
       {
            myAgent.ChangeFuelPerSecond(20f);
       }
       else
       {
            myAgent.ResetFuel();
            fsm.ChangeState(StateID.Patrol);
       }
    }

    public override void OnExit()
    {
        
    }
}
