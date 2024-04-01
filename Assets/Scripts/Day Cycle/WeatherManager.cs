using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private bool _rain, _morning, _day, _afternoon, _night, _oneTime;

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
            Morning.SetActive(false); _morning = false;
            Day.SetActive(false); _day = false;
            Afternoon.SetActive(false); _afternoon = false;
            Night.SetActive(false); _night = false;
        }
        else
        {
            if (_currentHour >= 5 && _currentHour < 8)
            {
                Rain.SetActive(false);
                Morning.SetActive(true); _morning = true;
                Day.SetActive(false); _day = false;
                Afternoon.SetActive(false); _afternoon = false;
                Night.SetActive(false); _night = false;
            }
            else if (_currentHour >= 8 && _currentHour < 17)
            {
                Rain.SetActive(false);
                Morning.SetActive(false); _morning = false;
                Day.SetActive(true); _day = true;
                Afternoon.SetActive(false); _afternoon = false;
                Night.SetActive(false); _night = false;
            }
            else if (_currentHour >= 17 && _currentHour < 20)
            {
                Rain.SetActive(false);
                Morning.SetActive(false); _morning = false;
                Day.SetActive(false); _day = false;
                Afternoon.SetActive(true); _afternoon = true;
                Night.SetActive(false); _night = false;
            }
            else if (_currentHour >= 20 || _currentHour < 5)
            {
                Rain.SetActive(false);
                Morning.SetActive(false); _morning = false;
                Day.SetActive(false); _day = false;
                Afternoon.SetActive(false); _afternoon = false;
                Night.SetActive(true); _night = true;
            }
        }
    }
}