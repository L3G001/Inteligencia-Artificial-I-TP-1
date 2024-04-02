using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredNode : Node
{
    public List<Node> RandomNode;

    public override void ExecuteNode()
    {
        int ran = Random.Range(0, RandomNode.Count);
        RandomNode[ran].ExecuteNode();
    }
}
