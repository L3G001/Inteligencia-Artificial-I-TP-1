using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour 
{




	//TODO: 
	//1. Colocar el nodo raíz.
	//2. Hacer lo que sea necesario para updatear las desiciones
	//del aldeano.
	[Range(0,100)]
	public float Hungry;
    [Range(0, 100)]
    public float Coold;

	public ActualAction ActualAction = ActualAction.Idle;
	public bool Bored;
	int SleepHour;



#region DONT TOUCH THIS
	public ParticleSystem sleepPos;
	public ParticleSystem getWoodPos;
	public ParticleSystem farmingPos;
	public ParticleSystem buildPos;

	public void GetFood()
	{
		Debug.Log("Decision: Get Food!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(farmingPos);
		EnviromentData.Instance.StartCoroutine("GetFoodCorrutine");
	}

	public void GetWood()
	{
		Debug.Log("Decision: Get Wood!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(getWoodPos);
        EnviromentData.Instance.StartCoroutine("GetWoodCorruine");
    }

	public void BuildHouses()
	{
		Debug.Log("Decision: Build!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(buildPos);
	}

	public void GoToSleep()
	{
		Debug.Log("Decision: Go to Sleep!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(sleepPos);
		StartCoroutine("RestTime");
		
		
	}

	private void DeactivateAllParticles()
	{
		sleepPos.Stop();
		getWoodPos.Stop();
		farmingPos.Stop();
		buildPos.Stop();
	}

	private void SetPosAndPlayParticle(ParticleSystem target)
	{
		transform.position = target.transform.position;
		target.Play();
	}

    #region Corrutine

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
			if(SleepHours < 0)
			{
				SleepHours += 24;
			}


			if (SleepHours >= 7)
			{
				ActualAction = ActualAction.Idle;
			}
			else
			{
                yield return new WaitForEndOfFrame();
                StartCoroutine("RestTime");
            }
        }

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
	Mitosis,
    BuildHouses,
	GetFood,
	GetWood,
	Idle

}
