using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool rain, morning, day, afternoon, night;
    
    public GameObject Rain, Morning, Day, Afternoon, Night;

    private int _currentHour;

    private void Update()
    {
        _currentHour = DayNightCycle.instance.CurrentHourAndMinute().CurrentHour;

        if (rain)
        {
            Rain.SetActive(true);
            Morning.SetActive(false);
            Day.SetActive(false);
            Afternoon.SetActive(false);
            Night.SetActive(false);
        }
        else
        {
            if(_currentHour >= 5 && _currentHour < 8)
            {
                Rain.SetActive(false);
                Morning.SetActive(true);
                Day.SetActive(false);
                Afternoon.SetActive(false);
                Night.SetActive(false);
            }
            else if (_currentHour >= 8 && _currentHour < 17)
            {
                Rain.SetActive(false);
                Morning.SetActive(false);
                Day.SetActive(true);
                Afternoon.SetActive(false);
                Night.SetActive(false);
            }
            else if (_currentHour >= 17 && _currentHour < 20)
            {
                Rain.SetActive(false);
                Morning.SetActive(false);
                Day.SetActive(false);
                Afternoon.SetActive(true);
                Night.SetActive(false);
            }
            else if (_currentHour >= 20 || _currentHour < 5)
            {
                Rain.SetActive(false);
                Morning.SetActive(false);
                Day.SetActive(false);
                Afternoon.SetActive(false);
                Night.SetActive(true);
            }
        }
    }
}