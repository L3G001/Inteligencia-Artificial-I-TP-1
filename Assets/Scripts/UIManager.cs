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
        totalBoids.text = "Total ducks: " + GameManager.Instance.allAgents.Count;
        hunterGold.text = "Hunter gold: " + GameManager.Instance.hunterGold;
        huntedBoids.text = "Hunted ducks: " + GameManager.Instance.huntedBoids;
        hunterFuel.value = GameManager.Instance.hunterCurrentFuel/GameManager.Instance.hunterMaxFuel;
        Fuel.color = Color.Lerp(criticalFuel, completeFuel, GameManager.Instance.hunterCurrentFuel/GameManager.Instance.hunterMaxFuel);
    }
}
