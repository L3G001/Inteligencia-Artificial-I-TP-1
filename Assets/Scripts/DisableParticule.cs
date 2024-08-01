using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticule : MonoBehaviour
{ 
    public List<ParticleSystem> _particules;

    private void OnTriggerEnter(Collider other)
    {
        foreach (ParticleSystem particule in _particules)
        {
            particule.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (ParticleSystem particule in _particules)
        {
            particule.Stop();
        }
    }
}
