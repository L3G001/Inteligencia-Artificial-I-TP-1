using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public TextMeshProUGUI textCost;
    List<Node> _neighbors = new List<Node>();
    Grid _grid;
    int _x;
    int _y;
    bool _blocked = false;
    int _cost;

    public int Cost { get { return _cost; } }
    public bool Blocked { get { return _blocked; } }

    public void Initialize(Grid grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        _cost = 1;
        textCost.text = _cost + "";
    }

    public List<Node> GetNeighbors
    {
        get 
        {
            if (_neighbors.Count > 0)
                return _neighbors;

            var nodeRight = _grid.GetNode(_x + 1, _y);

            if(nodeRight != null)
                _neighbors.Add(nodeRight);

            var nodeLeft = _grid.GetNode(_x - 1, _y);

            if (nodeLeft != null)
                _neighbors.Add(nodeLeft);

            var nodeUp = _grid.GetNode(_x, _y + 1);

            if (nodeUp != null)
                _neighbors.Add(nodeUp);

            var nodeDown = _grid.GetNode(_x, _y - 1);

            if (nodeDown != null)
                _neighbors.Add(nodeDown);

            return _neighbors;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _blocked = false;
            GameManager.instance.SetStartingNode(this);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _blocked = false;
            GameManager.instance.SetGoalNode(this);
        }

        if (Input.GetMouseButtonDown(2))
        {
            _blocked = !_blocked;

            if (_blocked)
            {
                gameObject.layer = 5;
                GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                gameObject.layer = 0;
                GetComponent<Renderer>().material.color = Color.white;
            }

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _cost++;
            if (_cost > 50)
                _cost = 50;
            textCost.text = _cost.ToString();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _cost--;
            if (_cost < 1)
                _cost = 1;

            textCost.text = _cost.ToString();
        }

    }
}
