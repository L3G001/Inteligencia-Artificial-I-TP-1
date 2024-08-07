using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();

    private void Start()
    {
        UpdateNeighbords();
    }

    public void UpdateNeighbords()
    {
        foreach (var node in nodes)
        {
            node.neighbors.Clear();

            foreach (var otherNodes in nodes)
            {
                if (node == otherNodes)
                    continue;

                if(GameManagerIA.instance.InLineOfSight(node.transform.position, otherNodes.transform.position))  
                    node.neighbors.Add(otherNodes);
            }
        }
    }

    public Node GetNearestNode(Vector3 position)
    {
        Node nearestNode = null;
        float minDistance = float.MaxValue;

        foreach (var node in nodes)
        {
            float distance = Vector3.Distance(node.transform.position, position);

            if (distance < minDistance && GameManagerIA.instance.InLineOfSight(position, node.transform.position))
            {
                minDistance = distance;
                nearestNode = node;
            }
        }

        return nearestNode;
    }
}
