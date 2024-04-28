using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text totalBoids, huntedBoids, hunterGold;
    [SerializeField] private Slider hunterFuel;
    [SerializeField] private Image Fuel;
    [SerializeField] private Color criticalFuel, completeFuel;

    void Update()
    {
        totalBoids.text = "Total ducks: " + GameManager.Instance.boidConfig.allAgents.Count;
        hunterGold.text = "Hunter gold: " + GameManager.Instance.hunterConfig.hunterGold;
        huntedBoids.text = "Hunted ducks: " + GameManager.Instance.hunterConfig.huntedBoids;
        hunterFuel.value = GameManager.Instance.hunterConfig.hunterCurrentFuel /GameManager.Instance.hunterConfig.hunterMaxFuel;
        Fuel.color = Color.Lerp(criticalFuel, completeFuel, GameManager.Instance.hunterConfig.hunterCurrentFuel /GameManager.Instance.hunterConfig.hunterMaxFuel);
    }
}
