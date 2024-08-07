using UnityEngine;

public class MouseController : MonoBehaviour
{
    public static Vector3 leftClickCoordenates = Vector3.zero;
    public static Vector3 rightClickCoordenates = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftClickCoordenates = HandleMouseClick(leftClickCoordenates);
            GameManagerIA.instance.blueLeaderTarget.transform.position = leftClickCoordenates;
            GameManagerIA.instance.grid.UpdateNeighbords();
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightClickCoordenates = HandleMouseClick(rightClickCoordenates);
            GameManagerIA.instance.redLeaderTarget.transform.position = rightClickCoordenates;
            GameManagerIA.instance.grid.UpdateNeighbords();
        }

    }

    Vector3 HandleMouseClick(Vector3 coordenates)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameManagerIA.instance.floorLayerMask) && !Physics.Raycast(ray, Mathf.Infinity, GameManagerIA.instance.obstacleLayerMask))
        {
            Vector3 hitPoint = hit.point;

            coordenates = new Vector3(hitPoint.x, 0f, hitPoint.z);

        }
        return coordenates;

    }
}
