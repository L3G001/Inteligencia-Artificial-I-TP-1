using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool inSight;
    public List<Node> path = new List<Node>();
    [SerializeField]
    private List<Node> aPath = new List<Node>();
    [SerializeField]
    private int pathIndex = 0;
    public State state;
    private State _state
    {
        get
        {
            return state;
        }
        set
        {
            if (value != state)
            {
                state = value;
                if (value == State.Alert)
                {
                    aPath = new();
                    pathIndex = 0;
                    aPath = GameManager.instance.pathfinding.CalculateAStar(GameManager.instance.GetNode(transform.position), GameManager.instance.GetNode(GameManager.instance.TargetLastPos));

                }
                if (value == State.Returning)
                {
                    aPath = new();
                    pathIndex = 0;
                    aPath = GameManager.instance.pathfinding.CalculateAStar(GameManager.instance.GetNode(transform.position), GetNearestPathNode());
                }
                if (state == State.Alert && value == State.Patrol)
                {
                    aPath = new();
                    pathIndex = 0;
                }
                if (state == State.Returning && value == State.Patrol)
                {
                    aPath = new();

                }
            }
        }
    }


    private Vector3 dir;
    public float speed;
    public float viewRadius;
    public float viewAngle;
    float _normalRadius;
    float _normalAngle;

    private void Awake()
    {
        _normalRadius = viewRadius;
        _normalAngle = viewAngle;
    }

    private void Update()
    {

        inSight = InFOV(GameManager.instance.target);

        if (state != State.Alert && state != State.Chase)
        {
            if (GameManager.instance.playerInSight)
            {
                _state = State.Alert;
            }
        }
        switch (state)
        {
            case State.Patrol:

                if (InFOV(GameManager.instance.target))
                {
                    _state = State.Chase;
                }
                if (Vector3.Distance(path[pathIndex].transform.position, transform.position) <= 0.2f)
                {
                    pathIndex++;
                    if (pathIndex >= path.Count)
                    {
                        pathIndex = 0;
                    }
                }
                else
                {
                    dir = path[pathIndex].transform.position - transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].transform.position, speed * Time.deltaTime);
                }



                break;
            case State.Alert:

                if (InFOV(GameManager.instance.target))
                {
                    _state = State.Chase;
                }

                if (Vector3.Distance(aPath[pathIndex].transform.position, transform.position) <= 0.2f)
                {
                    pathIndex++;
                    if (pathIndex >= aPath.Count)
                    {
                        if (Vector3.Distance(GameManager.instance.GetNode(GameManager.instance.TargetLastPos).transform.position, transform.position)>2)
                        {
                            aPath = GameManager.instance.pathfinding.CalculateAStar(GameManager.instance.GetNode(transform.position), GameManager.instance.GetNode(GameManager.instance.TargetLastPos));
                            pathIndex = 0;
                        }
                        else
                        _state = State.Returning;
                    }
                }
                else
                {
                    dir = aPath[pathIndex].transform.position - transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, aPath[pathIndex].transform.position, speed * Time.deltaTime);
                }


                break;
            case State.Chase:
                if (!InFOV(GameManager.instance.target))
                {
                    GameManager.instance.TargetLastPos = GameManager.instance.target.position;
                    _state = State.Alert;
                }
                else
                {
                    GameManager.instance.TargetLastPos = GameManager.instance.target.position;
                    dir = GameManager.instance.target.position - transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.target.position, speed * Time.deltaTime);
                }
                break;
            case State.Returning:
                if (InFOV(GameManager.instance.target))
                {
                    _state = State.Chase;
                }
                if (aPath.Count == 0)
                {
                    aPath = GameManager.instance.pathfinding.CalculateAStar(GameManager.instance.GetNode(transform.position), GetNearestPathNode());
                }
                if (Vector3.Distance(aPath[pathIndex].transform.position, transform.position) <= 0.2f)
                {
                    pathIndex++;
                    if (pathIndex >= aPath.Count - 1)
                    {
                        aPath = new();
                        pathIndex = path.IndexOf(GetNearestPathNode());
                        _state = State.Patrol;
                    }
                }
                else
                {
                    dir = aPath[pathIndex].transform.position - transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, aPath[pathIndex].transform.position, speed * Time.deltaTime);
                }

                break;
            case State.Idle:
                _state = State.Returning;
                break;
        }
        transform.right = dir;
    }

    public Node GetNearestPathNode()
    {
        Node node = null;
        float minDistance = float.MaxValue;

        foreach (var n in path)
        {
            float distance = Vector3.Distance(n.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                node = n;
            }
        }

        return node;
    }

    public bool InFOV(Transform obj)
    {
        var dir = obj.position - transform.position;

        if (dir.magnitude <= viewRadius)
        {
            if (Vector3.Angle(transform.right, dir) <= viewAngle * 0.5f)
            {
                return GameManager.instance.InLineOfSight(transform.position, obj.position);
            }
        }

        return false;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (GameManager.instance) Gizmos.color = InFOV(GameManager.instance.target) ? Color.red :state == State.Alert? Color.yellow : Color.green;
        else Gizmos.color = Color.green;


        Vector3 lineA = GetVectorFromAngle(viewAngle * 0.5f + transform.eulerAngles.z);
        Vector3 lineB = GetVectorFromAngle(-viewAngle * 0.5f + transform.eulerAngles.z);

        Gizmos.DrawLine(transform.position, transform.position + lineA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * viewRadius);
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
}

public enum State
{
    Idle,
    Alert,
    Patrol,
    Chase,
    Returning

}

