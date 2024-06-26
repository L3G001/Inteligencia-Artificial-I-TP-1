using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public TextMeshProUGUI textCost;
    public List<Node> _neighbors = new List<Node>();
    
    public bool _blocked = false;
    public int _cost;

    public int Cost { get { return _cost; } }
    public bool Blocked { get { return _blocked; } }

    public List<Node> GetNeighbors
    {
        get 
        {
            return _neighbors;
        }
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        foreach (var neighbor in _neighbors)
        {
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
        Gizmos.color = Color.red;
        if (GameManager.instance)  if (GameManager.instance.GetNode(GameManager.instance.TargetLastPos)== this) Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    
}
