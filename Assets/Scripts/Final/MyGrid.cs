using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public List<Node> grid = new();
    
    private IEnumerator Start()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        yield return wait;
        UpdateNeighbords();
    }

    public void UpdateNeighbords()
    {
        //Una opcion
        foreach (var node in grid)
        {
            node.neighbors.Clear();

            foreach (var otherNodes in grid)
            {
                if (node == otherNodes)
                    continue;

                if(GameManager.Instance.InLineOfSight(node.transform.position, otherNodes.transform.position,node.nodeCollider,otherNodes.nodeCollider))  
                    node.neighbors.Add(otherNodes);
            }
        }

        //Otra opcion, tirar Raycast hacia los costados y adelante y atras
    }
    public Node GetNearesNode(Vector3 position)
    {
        Node nearest = null;
        float minDist = float.MaxValue;
        foreach (var node in grid)
        {
            float dist = Vector3.Distance(node.transform.position, position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = node;
            }
        }
        return nearest;
    }
}
