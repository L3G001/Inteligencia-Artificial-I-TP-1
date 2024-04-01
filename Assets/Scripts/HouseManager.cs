using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public bool Construible = true;

    public List<GameObject> HousesSubComplex1;
    public List<GameObject> HousesSubComplex2;
    public List<GameObject> HousesSubComplex3;
    public List<GameObject> HousesSubComplex4;
    public List<GameObject> HousesSubComplex5;
    public List<GameObject> HousesSubComplex6;
    public List<GameObject> HousesSubComplex7;
    public List<GameObject> HousesSubComplex8;
    public List<GameObject> HousesSubComplex9;

    public List<List<GameObject>> OriginalHousesComplex = new List<List<GameObject>>();

    public List<List<GameObject>> HousesComplex = new List<List<GameObject>>();
    
    private void Start()
    {
        OriginalHousesComplex.Add(HousesSubComplex1);
        OriginalHousesComplex.Add(HousesSubComplex2);
        OriginalHousesComplex.Add(HousesSubComplex3);
        OriginalHousesComplex.Add(HousesSubComplex4);
        OriginalHousesComplex.Add(HousesSubComplex5);
        OriginalHousesComplex.Add(HousesSubComplex6);
        OriginalHousesComplex.Add(HousesSubComplex7);
        OriginalHousesComplex.Add(HousesSubComplex8);
        OriginalHousesComplex.Add(HousesSubComplex9);
        HousesComplex.Add(HousesSubComplex1);
        HousesComplex.Add(HousesSubComplex2);
        HousesComplex.Add(HousesSubComplex3);
        HousesComplex.Add(HousesSubComplex4);
        HousesComplex.Add(HousesSubComplex5);
        HousesComplex.Add(HousesSubComplex6);
        HousesComplex.Add(HousesSubComplex7);
        HousesComplex.Add(HousesSubComplex8);
        HousesComplex.Add(HousesSubComplex9);

        foreach (var subComplex in HousesComplex)
        {
            for (int i = 0; i < subComplex.Count;)
            {
                if (subComplex[i] != null && subComplex[i].activeSelf)
                {
                    subComplex.Remove(subComplex[i]);
                }
                else
                {
                    i++;
                }
            }
        }
        HousesComplex.Remove(HousesSubComplex1);
        HousesComplex.Remove(HousesSubComplex2);
        HousesComplex.Remove(HousesSubComplex3);
        HousesComplex.Remove(HousesSubComplex4);
        HousesComplex.Remove(HousesSubComplex5);
        HousesComplex.Remove(HousesSubComplex6);
        HousesComplex.Remove(HousesSubComplex7);
        HousesComplex.Remove(HousesSubComplex8);
    }
    [ContextMenu("BuildHouse")]
    public void BuildHouse()
    {
        if (HousesComplex.Count == 0)
        {
            foreach (var complex in OriginalHousesComplex)
            {
                HousesComplex.Add(complex);
            }
        }
        Construible = CheckConstruible();
        if (Construible)
        {

            List<GameObject> subComplex = HousesComplex[Random.Range(0, HousesComplex.Count)];
            subComplex[0].SetActive(true);
            subComplex.Remove(subComplex[0]);
            HousesComplex.Remove(subComplex);
        }
    }
    public bool CheckConstruible()
    {
        foreach (var complex in OriginalHousesComplex)
        {
            if (complex.Count != 0)
            {
                return true;
            }
        }
        return false;
    }


}
