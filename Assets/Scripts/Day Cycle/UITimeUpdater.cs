using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class UITimeUpdater : MonoBehaviour
{
    [SerializeField] DayNightCycle _dayNightCycle;
    private int _dayTracker = 1;
    [SerializeField] TMP_Text _days, _time;
    private int _hours = 0, _minutes = 0;
    private bool _oneTimer;

    void Start()
    {
        
    }

    void Update()
    {
        _hours = _dayNightCycle.CurrentHourAndMinute().CurrentHour;
        _minutes = _dayNightCycle.CurrentHourAndMinute().CurrentMinute;
        if (_hours == 23 && _minutes == 59 && !_oneTimer) { _dayTracker += 1; _oneTimer = true; }
        if (_hours != 23 && _minutes != 59 && _oneTimer) { _oneTimer = false; }
        _days.text = "Dia " + _dayTracker;
        string Hours = _hours < 10 ? "0" + _hours:_hours.ToString();
        string Minutes = _minutes < 10 ? "0"+_minutes:_minutes.ToString();
        _time.text = Hours + ":" + Minutes;
    }
}
