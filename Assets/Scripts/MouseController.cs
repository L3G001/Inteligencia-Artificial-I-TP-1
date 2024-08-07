using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public  Vector3 leftClickCoordenates = Vector3.zero;
    public  Vector3 rightClickCoordenates = Vector3.zero;
    private void Start()
    {
     
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.leaderConfig.blueLeaderNode.transform.position = HandleMouseClick(leftClickCoordenates) != Vector3.zero? HandleMouseClick(leftClickCoordenates): GameManager.Instance.leaderConfig.blueLeaderNode.transform.position;
            GameManager.Instance.grid.UpdateNeighbords();
        }

        // Detecta clic derecho del mouse
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.leaderConfig.redLeaderNode.transform.position = HandleMouseClick(leftClickCoordenates) != Vector3.zero? HandleMouseClick(leftClickCoordenates): GameManager.Instance.leaderConfig.redLeaderNode.transform.position;
            GameManager.Instance.grid.UpdateNeighbords();
        }

    }

    Vector3 HandleMouseClick (Vector3 coordenates)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameManager.Instance.FloorMask) && !Physics.Raycast(ray, Mathf.Infinity, GameManager.Instance.layerMask))
        {
            Vector3 hitPoint = hit.point;
 
            return new Vector3(hitPoint.x, hitPoint.y, 0);
            
        }
        return Vector3.zero;
        
        
    }
}
