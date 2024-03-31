using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredNode : Node
{
    public List<Node> RandomNode;
    public Node FalseNode;

    public override void ExecuteNode()
    {
        if (EnviromentData.Instance.citizen.Bored)
        {
            int ran = Random.Range(0, RandomNode.Count);
            RandomNode[ran].ExecuteNode();
        }
        else
        {
            FalseNode.ExecuteNode();
        }

    }
}
