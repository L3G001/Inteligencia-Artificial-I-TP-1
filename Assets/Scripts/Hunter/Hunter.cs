using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : SteeringAgent
{
    FSM fsm;

    private void Start()
    {
        fsm = new();
        fsm.AddState(StateID.Patrol, new Patrol(this, GameManager.Instance.hunterConfig.waypointManagers.PatrolManager));
        fsm.AddState(StateID.Idle, new Idle(this));

        fsm.ChangeState(StateID.Patrol);
        ResetFuel();
        
    }
    private void Update()
    {
        fsm.OnUpdate();
        CheckFuel();
    }
    #region FuelMethods
    public void ChangeFuelPerSecond(float value)
    {
        GameManager.Instance.hunterConfig.hunterCurrentFuel += value*Time.deltaTime;
    }
    public void ResetFuel()
    {
        GameManager.Instance.hunterConfig.hunterCurrentFuel = GameManager.Instance.hunterConfig.hunterMaxFuel;
    }
    public void CheckFuel()
    {
        if(GameManager.Instance.hunterConfig.hunterCurrentFuel <= 0)
        {
            fsm.ChangeState(StateID.Idle);
        }
    }
    #endregion



    #region GetSteringMethods
    public void AddForce(Vector3 force)
    {
        base.AddForce(force);

    }
    public void Move()
    {
      base.Move();
    }
    public Vector3 Seek(Vector3 targetPos)
    {
        return base.Seek(targetPos);
    }
    public Vector3 Flee(Vector3 fleePos)
    {
        return base.Flee(fleePos);
    }
    public Vector3 Arrive(Vector3 targetPos)
    {
        return base.Arrive(targetPos);
    }
    public Vector3 Pursuit(SteeringAgent targetAgent)
    {
        return base.Pursuit(targetAgent);
    }
    public bool HasToUseOA()
    {
        return base.HasToUseOA();
    }
    public Vector3 ObstacleAvoidance()
    {
        return base.ObstacleAvoidance();
    }
    #endregion
}
