using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selling : State
{
    float timer = 0;
    public override void OnEnter()
    {
     timer = 0;
    }

    public override void OnExit()
    {
        GameManager.Instance.hunterConfig.hunterCurrentFuel = GameManager.Instance.hunterConfig.hunterMaxFuel;
        GameManager.Instance.hunterConfig.hunterGold += GameManager.Instance.hunterConfig.boidsInBoat * GameManager.Instance.hunterConfig.duckPrice;
        GameManager.Instance.hunterConfig.boidsInBoat = 0;
    }

    public override void OnUpdate()
    {

        if(timer < GameManager.Instance.hunterConfig.sellTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            fsm.ChangeState(StateID.Patrol);
        }
    }
}
