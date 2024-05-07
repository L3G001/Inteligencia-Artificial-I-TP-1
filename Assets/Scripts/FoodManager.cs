using System.Collections;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;
    public bool _oneTimeSpawn = false;
    private bool _oneTimeFood = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false)
        {
            FoodSpawn();
        }
    }

    void Update()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false && !_oneTimeFood)
        {
            _oneTimeFood = true;
            FoodSpawn();
        }
        
    }

    void FoodSpawn()
    {
        StartCoroutine(FoodSpawnDelay());
    }

   
    IEnumerator FoodSpawnDelay()
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.boidConfig.food.transform.position = GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints[Random.Range(0, GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints.Count)].Position;
        GameManager.Instance.boidConfig.food.gameObject.SetActive(true);
        _oneTimeFood = false;
    }
}
