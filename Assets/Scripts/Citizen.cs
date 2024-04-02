using System.Collections;
using UnityEngine;

public class Citizen : MonoBehaviour
{

    public Node RootNode;

    //TODO: 
    //1. Colocar el nodo raíz.
    //2. Hacer lo que sea necesario para updatear las desiciones
    //del aldeano.
    [Range(0, 100)]
    public float Hungry;
    [Range(0, 100)]
    public float Coold;

    public ActualAction _actualAction = ActualAction.Idle;

    public ActualAction ActualAction
    {
        get
        {
            return _actualAction;
        }

        set
        {
            if (value != _actualAction && value == ActualAction.Idle)
            {
                _actualAction = value;
                RootNode.ExecuteNode();
            }
            else
            {
                _actualAction = value;
            }

        }
    }

    public bool Bored;
    int SleepHour;


    private void Start()
    {
        RootNode.ExecuteNode();
    }


    #region DONT TOUCH THIS
    public ParticleSystem sleepPos;
    public ParticleSystem getWoodPos;
    public ParticleSystem farmingPos;
    public ParticleSystem buildPos;
    public GameObject EatPart;
    public GameObject WarmUpPart;
    public GameObject DancePart;
    public GameObject SolitariePart;

    [ContextMenu("GetFood")]
    public void GetFood()
    {
        Debug.Log("Decision: Get Food!");
        DeactivateAllParticles();
        SetPosAndPlayParticle(farmingPos);
        EnviromentData.Instance.StartCoroutine("GetFoodCorrutine");
    }
    [ContextMenu("GetWood")]
    public void GetWood()
    {
        Debug.Log("Decision: Get Wood!");
        DeactivateAllParticles();
        SetPosAndPlayParticle(getWoodPos);
        EnviromentData.Instance.StartCoroutine("GetWoodCorruine");
    }
    [ContextMenu("BuildHouses")]
    public void BuildHouses()
    {
        Debug.Log("Decision: Build!");
        DeactivateAllParticles();
        SetPosAndPlayParticle(buildPos);
        StartCoroutine("BuildHousesCorrutine");
    }
    [ContextMenu("GoToSleep")]
    public void GoToSleep()
    {
        Debug.Log("Decision: Go to Sleep!");
        DeactivateAllParticles();
        SetPosAndPlayParticle(sleepPos);
        StartCoroutine("RestTime");
    }
    [ContextMenu("Eat")]
    public void Eat()
    {
        Debug.Log("Decision: Eating!");
        SetPosAndPlayParticle(sleepPos);
        DeactivateAllParticles();
        EatPart.SetActive(true);
        StartCoroutine("EatingCorrutine");
    }
    [ContextMenu("Warm Up")]
    public void WarmUp()
    {
        Debug.Log("Decision: Warming Up!");
        SetPosAndPlayParticle(sleepPos);
        DeactivateAllParticles();
        WarmUpPart.SetActive(true);
        StartCoroutine("WarmUpCorrutine");
    }
    [ContextMenu("Dance")]
    public void Dance()
    {
        Debug.Log("Decision: Dancing!");
        SetPosAndPlayParticle(sleepPos);
        DeactivateAllParticles();
        DancePart.SetActive(true);
        StartCoroutine("DancingCorrutine");
    }
    [ContextMenu("PlayCards")]
    public void PlayCards()
    {
        Debug.Log("Decision: Playing solitary!");
        SetPosAndPlayParticle(sleepPos);
        DeactivateAllParticles();
        SolitariePart.SetActive(true);
        StartCoroutine("PlayCardsCorrutine");
    }

    private void DeactivateAllParticles()
    {
        sleepPos.Stop();
        getWoodPos.Stop();
        farmingPos.Stop();
        buildPos.Stop();
        EatPart.SetActive(false);
        WarmUpPart.SetActive(false);
        DancePart.SetActive(false);
        SolitariePart.SetActive(false);

    }

    private void SetPosAndPlayParticle(ParticleSystem target)
    {
        transform.position = target.transform.position;
        target.Play();
    }

    #region Corrutine
    public IEnumerator BuildHousesCorrutine()
    {
        ActualAction = ActualAction.BuildHouses;
        yield return new WaitForSeconds(EnviromentData.Instance.cooldownData.BuildHouseCooldown);
        EnviromentData.Instance.houseManager.BuildHouse();
        EnviromentData.Instance.desicionData.HouseAmount++;
        EnviromentData.Instance.desicionData.wood -= 30;
        EnviromentData.Instance.citizen.Hungry += 20;
        ActualAction = ActualAction.Idle;
    }
    public IEnumerator RestTime()
    {
        if (ActualAction != ActualAction.Rest)
        {
            ActualAction = ActualAction.Rest;
            SleepHour = DayNightCycle.instance.CurrentHourAndMinute().CurrentHour;
            yield return new WaitForEndOfFrame();
            StartCoroutine("RestTime");
        }
        else
        {
            int SleepHours = DayNightCycle.instance.CurrentHourAndMinute().CurrentHour - SleepHour;
            if (SleepHours < 0)
            {
                SleepHours += 24;
            }


            if (SleepHours >= 7)
            {
                EnviromentData.Instance.citizen.Hungry += 10;
                ActualAction = ActualAction.Idle;
            }
            else
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine("RestTime");
            }
        }

    }
    public IEnumerator EatingCorrutine()
    {
        ActualAction = ActualAction.Eat;
        yield return new WaitForSeconds(4);
        Hungry = 0;
        EnviromentData.Instance.desicionData.food -= 10;
        ActualAction = ActualAction.Idle;
    }
    public IEnumerator WarmUpCorrutine()
    {
        ActualAction = ActualAction.WarmUp;
        while (Coold >= 10)
        {
            yield return new WaitForEndOfFrame();
            Coold -= 10f * Time.deltaTime;
        }
        EnviromentData.Instance.desicionData.wood -= 10;
        EnviromentData.Instance.citizen.Hungry += 10;
        ActualAction = ActualAction.Idle;
    }
    public IEnumerator DancingCorrutine()
    {
        ActualAction = ActualAction.Dance;
        yield return new WaitForSeconds(4);
        EnviromentData.Instance.citizen.Hungry += 15;
        ActualAction = ActualAction.Idle;
    }
    public IEnumerator PlayCardsCorrutine()
    {
        ActualAction = ActualAction.PlayCards;
        yield return new WaitForSeconds(4);
        EnviromentData.Instance.citizen.Hungry += 5;
        ActualAction = ActualAction.Idle;
    }
    #endregion
    #endregion
}
public enum ActualAction
{
    Eat,
    Rest,
    WarmUp,
    Dance,
    BuildHouses,
    GetFood,
    GetWood,
    Idle,
    PlayCards

}
