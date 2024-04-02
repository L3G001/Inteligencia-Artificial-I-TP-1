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

                if (EnviromentData.Instance.desicionData.rain) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
            case Questions.Day:
                if (EnviromentData.Instance.desicionData.day) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
            case Questions.Hunger:
                if (EnviromentData.Instance.citizen.Hungry >= 50) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
            case Questions.Cold:
                if (EnviromentData.Instance.citizen.Coold >= 50) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
            case Questions.Wood:
                if (EnviromentData.Instance.desicionData.wood >= 10) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
            case Questions.Food:
                if (EnviromentData.Instance.desicionData.food >= 10) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
                break;
           // case Questions.MitosisFood:
             //   if (EnviromentData.Instance.desicionData.food >= 30) TrueNode.ExecuteNode();
               // else FalseNode.ExecuteNode();
                //break;
            case Questions.HouseWood:
                if (EnviromentData.Instance.desicionData.wood >= 40) TrueNode.ExecuteNode();
                else FalseNode.ExecuteNode();
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
    Food,
    HouseWood,
    
}
