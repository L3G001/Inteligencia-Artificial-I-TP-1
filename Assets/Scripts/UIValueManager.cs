using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIValueManager : MonoBehaviour
{
    [SerializeField] Slider _hunger, _cold;
    [SerializeField] TMP_Text _foodValue, _woodValue;

    [SerializeField] GameObject _dancing, _warmUp, _eating, _buildingHouse, _sleeping, _playingCards, _gatherFood, _gatherWood;

    private void Update()
    {
        _foodValue.text = EnviromentData.Instance.desicionData.food.ToString();
        _woodValue.text = EnviromentData.Instance.desicionData.wood.ToString();
        
        _hunger.maxValue = 100;
        _hunger.minValue = 0;
        _hunger.value = EnviromentData.Instance.citizen.Hungry;

        _cold.maxValue = 100;
        _cold.minValue = 0;
        _cold.value = EnviromentData.Instance.citizen.Coold;

        switch (EnviromentData.Instance.citizen.ActualAction)
        {
            case ActualAction.Dance:
                _dancing.SetActive(true);
                break;
            case ActualAction.Eat:
                _eating.SetActive(true);
                break;
            case ActualAction.Rest:
                _sleeping.SetActive(true);
                break;
            case ActualAction.WarmUp:
                _warmUp.SetActive(true);
                break;
            case ActualAction.BuildHouses:
                _buildingHouse.SetActive(true);
                break;
            case ActualAction.GetFood:
                _gatherFood.SetActive(true);
                break;
            case ActualAction.GetWood:
                _gatherWood.SetActive(true);
                break;
            case ActualAction.PlayCards:
                _playingCards.SetActive(true);
                break;
        }
    }
}
