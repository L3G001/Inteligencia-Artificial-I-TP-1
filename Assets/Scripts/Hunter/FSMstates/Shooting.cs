using UnityEngine;

public class Shooting : State
{
    ArpShoter _myArpShoter;
    SteeringAgent _target;
    float _shootRadius = 5f;
    public Shooting(ArpShoter myArpShoter)
    {
        _myArpShoter = myArpShoter;
    }
    public override void OnEnter()
    {
        SteeringAgent clooserAgent = GameManager.Instance.boidConfig.allAgents[0];
        foreach (var Agent in GameManager.Instance.boidConfig.allAgents)
        {
            if (Vector3.Distance(_myArpShoter.transform.position, Agent.transform.position) < Vector3.Distance(clooserAgent.transform.position, _myArpShoter.transform.position))
            {
                clooserAgent = Agent;
            }
        }
        if (Vector3.Distance(clooserAgent.transform.position, _myArpShoter.transform.position) < _shootRadius)
        {
            _target = clooserAgent;
        }
        else
        {
            fsm.ChangeState(StateID.Patrol);
        }
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        _myArpShoter.AimToTarget(_target);
        _myArpShoter.ApplyRotation();
        if (_myArpShoter.CompareAngle(_target))
        {
            if (GameManager.Instance.hunterConfig.hunterCurrentFuel >= GameManager.Instance.hunterConfig.hunterShootCost)
            {
                _myArpShoter.Shoot();
                GameManager.Instance.hunterConfig.hunterCurrentFuel -= GameManager.Instance.hunterConfig.hunterShootCost;
                _target.gameObject.SetActive(false);
                GameManager.Instance.hunterConfig.huntedBoids++;
            }
            else
            {
                fsm.ChangeState(StateID.Patrol);
            }
            fsm.ChangeState(StateID.Patrol);
        }
        if(Vector3.Distance(_target.transform.position,_myArpShoter.transform.position) > _shootRadius)
        {
            fsm.ChangeState(StateID.Patrol);
        }
    }
}
