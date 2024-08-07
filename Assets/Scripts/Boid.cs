using System.Collections.Generic;
using UnityEngine;
public class Boid : SteeringAgent
{
    public int life = 100;
    public int maxLife = 100;



    FSM<Enums.BoidStateID> _fsm;

    public List<Node> path = new List<Node>();
    public bool RedElseBlue;

    private void Start()
    {
        if (RedElseBlue)
        {
            GameManager.Instance.boidConfig.RedAgents.Add(this);
        }
        else
        {
            GameManager.Instance.boidConfig.BlueAgents.Add(this);
        }

        _fsm = new FSM<Enums.BoidStateID>();

        _fsm.AddState(Enums.BoidStateID.PathFindToLead, new BoidPathFind(this, RedElseBlue ? GameManager.Instance.leaderConfig.redLeader : GameManager.Instance.leaderConfig.blueLeader));
        _fsm.AddState(Enums.BoidStateID.LeaderFloking, new LeaderFloking(this));
        _fsm.AddState(Enums.BoidStateID.FollowPath, new BoidFollowPath(this));

        _fsm.ChangeState(Enums.BoidStateID.LeaderFloking);

    }
    public void Spece()
    {
        if (RedElseBlue)
        {

            AddForce(Spacing(GameManager.Instance.boidConfig.RedAgents, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);
        }
        else
        {
            AddForce(Spacing(GameManager.Instance.boidConfig.BlueAgents, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);

        }
    }

    public void LeadFlocking()
    {
        var boids = RedElseBlue ? GameManager.Instance.boidConfig.RedAgents : GameManager.Instance.boidConfig.BlueAgents;
        AddForce(Spacing(boids, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);
        if (RedElseBlue)
        {
            if (GameManager.Instance.InLineOfSight(transform.position, GameManager.Instance.leaderConfig.redLeader.transform.position))
            {
                AddForce(Arrive(GameManager.Instance.leaderConfig.redLeader.transform.position) * GameManager.Instance.boidConfig.arriveWeight);
                AddForce(Spacing(GameManager.Instance.boidConfig.RedAgents, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);
            }
            _fsm.ChangeState(Enums.BoidStateID.PathFindToLead);

        }
        else
        {
            if (GameManager.Instance.InLineOfSight(transform.position, GameManager.Instance.leaderConfig.blueLeader.transform.position))
            {
                AddForce(Arrive(GameManager.Instance.leaderConfig.blueLeader.transform.position) * GameManager.Instance.boidConfig.arriveWeight);
                AddForce(Spacing(GameManager.Instance.boidConfig.BlueAgents, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);
            }
            _fsm.ChangeState(Enums.BoidStateID.PathFindToLead);
        }

    }


    private void Update()
    {
        _fsm.OnUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = RedElseBlue ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.viewRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.separationRadius);

    }

}
