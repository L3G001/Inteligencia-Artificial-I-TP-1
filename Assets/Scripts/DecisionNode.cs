using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionNode : Node
{
    public Node TrueNode;
    public Node FalseNode;
    public Questions questions;

    public override void ExecuteNode()
    {
        switch ((questions))
        {
            case Questions.Raining:

                break;
            case Questions.Day:

                break;
            case Questions.Hunger:

                break;
            case Questions.Cold:

                break;
            case Questions.Wood:

                break;
            case Questions.Food:

                break;
        }
    }
}
    public enum Questions
{
    Raining,
    Day,
    Hunger,
    Cold,
    Wood,
    Food
}
