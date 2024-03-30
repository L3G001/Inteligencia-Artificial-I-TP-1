using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private static DayNightCycle _instance;
    public static DayNightCycle instance {  get { return _instance; } }

    [SerializeField] LengthOfDay ChooseDayLength;

    private float _degrees;
    private float totalDegreesRotated;

    [Header("How many real life hours last a in-game day.")]
    [Range(1, 24)]
    [SerializeField] int Hours;

    [Header("How many real life minutes last a in-game day.")]
    [Range(1, 60)]
    [SerializeField] int Minutes;

    [Header("How many real life seconds last a in-game day.")]
    [Range(1, 60)]
    [SerializeField] int Seconds;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        switch (ChooseDayLength)
        {
            case LengthOfDay.Hour:
                _degrees = 360f / (Hours * 3600f);
                break;
            case LengthOfDay.Minute:
                _degrees = 360f / (Minutes * 60f);
                break;
            case LengthOfDay.Second:
                _degrees = 360f / Seconds;
                break;
        }
        gameObject.transform.Rotate(0, 0, _degrees * Time.deltaTime);

        float rotationThisFrame = _degrees * Time.deltaTime;
        totalDegreesRotated += rotationThisFrame;
        
    }

    public (int CurrentHour, int CurrentMinute) CurrentHourAndMinute()
    {
        float hoursPassed = ((totalDegreesRotated + 180) % 360 / 360f) * 24;
        int currentHour = (int)hoursPassed % 24;
        int currentMinute = (int)((hoursPassed - (int)hoursPassed) * 60);
        return (currentHour, currentMinute);
    }
}
public enum LengthOfDay
{
    Hour,
    Minute, 
    Second
}
