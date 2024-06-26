using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Enemy> enemies = new List<Enemy>();
    public Vector3 TargetLastPos;
    public Transform target;
    public List<Node> nodes = new List<Node>();
    public Pathfinding pathfinding;
    public LayerMask layerMask;

    public bool playerInSight;

    private void Update()
    {
       bool inSight = false;
        foreach (var enemy in enemies)
        {
            if (enemy.inSight)
            {
                inSight = true;
                break;
            }
        }
       playerInSight = inSight;
    }

    private void Awake()
    {
        instance = this;
    }

    public Node GetNode(Vector3 position)
    {
        Node node = null;
        float minDistance = float.MaxValue;

        foreach (var n in nodes)
        {
            float distance = Vector3.Distance(n.transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                node = n;
            }
        }

        return node;
    }

    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        var dir = end - start;

        return !Physics.Raycast(start, dir, dir.magnitude, layerMask);
    }

}
