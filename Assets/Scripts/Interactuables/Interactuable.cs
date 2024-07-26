using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable: MonoBehaviour
{
    [SerializeField] protected List<Pedestal> _linkedObjects;

    public virtual void Execute() { }
    public virtual void StopExecute() { }
}
