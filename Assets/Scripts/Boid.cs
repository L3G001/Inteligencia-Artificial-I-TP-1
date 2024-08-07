using System.Collections.Generic;
using UnityEngine;
public class Boid : SteeringAgent
{
    public float life = 100;
    public int maxLife = 100;

    float timer = 0;

    FSM<Enums.BoidStateID> _fsm;

    public List<Node> path = new List<Node>();
    public bool RedElseBlue;

    private void Start()
    {
        life = maxLife;
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
        _fsm.AddState(Enums.BoidStateID.Escape, new Escape(this));
        _fsm.AddState(Enums.BoidStateID.InBase, new InBase(this));
        _fsm.AddState(Enums.BoidStateID.PathfindToBase, new PathFindToBase(this));


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
        if(life<40)_fsm.ChangeState(Enums.BoidStateID.PathfindToBase);
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
    public void TakeDamage(float damage)
    {
        life -= damage;
    }

    private void Update()
    {
        timer += Time.deltaTime;
     
       foreach (var boid in RedElseBlue ? GameManager.Instance.boidConfig.BlueAgents : GameManager.Instance.boidConfig.RedAgents)
       {
            if(InFOV(boid.transform)&&timer >=2)
            {

                Bullet bulletTransform = Instantiate(GameManager.Instance.boidConfig.Bullet, transform.position, Quaternion.identity);
                bulletTransform.redElseBlue = RedElseBlue;
                bulletTransform.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                bulletTransform.transform.up = (boid.transform.position-transform.position).normalized;
                timer = 0;

            }
       }


        _fsm.OnUpdate();
    }
    public bool InFOV(Transform obj)
    {
        var dir = obj.position - transform.position;

        if (dir.magnitude <= GameManager.Instance.boidConfig.viewRadius)
        {
            if (Vector3.Angle(transform.right, dir) <= 90 * 0.5f)
            {
                return GameManager.Instance.InLineOfSight(transform.position, obj.position);
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (GameManager.Instance == null) return;
        
        Gizmos.color = RedElseBlue ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.viewRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.separationRadius);

    }

}
