using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Pathfinding pathfinding;
    public Enemy enemy;
    public LayerMask layerMask;
    Node _startingNode;
    Node _goalNode;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_startingNode != null && _goalNode != null)
            {
                /*
                var list = pathfinding.CalculateDijkstra(_startingNode, _goalNode);
                enemy.SetMove(list);
                */
                StartCoroutine(pathfinding.CoroutineTheta(_startingNode, _goalNode));
            }
        }

    }

    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        var dir = end - start;

        return !Physics.Raycast(start, dir, dir.magnitude, layerMask);
    }

    public void SetStartingNode(Node node)
    {
        if(_startingNode != null)
        {
            _startingNode.GetComponent<Renderer>().material.color = Color.white;
        }

        _startingNode = node;
        _startingNode.GetComponent<Renderer>().material.color = Color.red;
        enemy.transform.position = node.transform.position;
    }

    public void SetGoalNode(Node node)
    {
        if (_goalNode != null)
        {
            _goalNode.GetComponent<Renderer>().material.color = Color.white;
        }

        _goalNode = node;
        _goalNode.GetComponent<Renderer>().material.color = Color.yellow;
    }

}
