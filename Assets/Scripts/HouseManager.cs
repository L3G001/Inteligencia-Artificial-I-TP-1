using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public List<GameObject> HousesSubComplex1;
    public List<GameObject> HousesSubComplex2;
    public List<GameObject> HousesSubComplex3;
    public List<GameObject> HousesSubComplex4;
    public List<GameObject> HousesSubComplex5;
    public List<GameObject> HousesSubComplex6;
    public List<GameObject> HousesSubComplex7;
    public List<GameObject> HousesSubComplex8;
    public List<GameObject> HousesSubComplex9;

    public List<List<GameObject>> HousesComplex;
    private void Start()
    {
        foreach (var subComplex in HousesComplex)
        {
            for (int i = 0; i < subComplex.Count;) 
            {
                if(subComplex[i] != null && subComplex[i].activeSelf)
                {
                    subComplex.Remove(subComplex[i]);
                }
                else
                {
                    i++;
                }
            }
        }
    }
    public void BuildHouse()
    {
        List<GameObject> subComplex = HousesComplex[Random.Range(0, HousesComplex.Count)];
        subComplex[0].SetActive(true);
        subComplex.Remove(subComplex[0]);
    }


}
