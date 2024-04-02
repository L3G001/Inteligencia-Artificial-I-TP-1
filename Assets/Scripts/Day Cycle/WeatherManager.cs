using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool _rain, _morning, _day, _afternoon, _night, _oneTime;

    public GameObject Rain, Morning, Day, Afternoon, Night;

    private int _currentHour;

    private void Update()
    {
        _currentHour = DayNightCycle.instance.CurrentHourAndMinute().CurrentHour;

        if (_currentHour == 0 && !_oneTime)
        {
            int rainy = Random.Range(0, 101);
            if (rainy <= 10) { _rain = true; }
            else { _rain = false; }
            _oneTime = true;
        }
        else if (_currentHour != 0 && _oneTime) { _oneTime = false; }

        if (_rain)
        {
            Rain.SetActive(true);
            _morning = false;
            _day = false;
            _afternoon = false;
            _night = false;
        }
        else
        {
            if (_currentHour >= 5 && _currentHour < 8)
            {
                Morning.SetActive(true); _morning = true;
                _day = false;
                _afternoon = false;
                _night = false;
            }
            else if (_currentHour >= 8 && _currentHour < 17)
            {
                _morning = false;
                Day.SetActive(true); _day = true;
                _afternoon = false;
                _night = false;
            }
            else if (_currentHour >= 17 && _currentHour < 20)
            {
                _morning = false;
                _day = false;
                Afternoon.SetActive(true); _afternoon = true;
                _night = false;
            }
            else if (_currentHour >= 20 || _currentHour < 5)
            {
                _morning = false;
                _day = false;
                _afternoon = false;
                Night.SetActive(true); _night = true;
            }
        }
        EnviromentData.Instance.desicionData.rain = _rain;
    }
}