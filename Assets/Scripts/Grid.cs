using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Node prefab;
    public int width, height;
    public float offset;
    Node[,] _grid;
    private void Start()
    {
        _grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var go = Instantiate(prefab);
                _grid[x, y] = go;
                go.transform.position = new Vector2(x * go.transform.localScale.x, y * go.transform.localScale.y) * offset;
                go.Initialize(this, x, y);
            }
        }       
    }

    public Node GetNode(int x, int y)
    {
        if (x >= width || y >= height || x < 0 || y < 0)
            return null;

        return _grid[x, y];
    }
}
