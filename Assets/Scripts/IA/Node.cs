using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();
    int _cost = 1;

    public int Cost { get { return _cost; } }

    public List<Node> GetNeighbors { get { return neighbors; } }

    public void Start()
    {
        GameManagerIA.instance.grid.nodes.Add(this);
    }
}
