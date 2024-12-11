using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pedestal : Interactuable
{
    public PedestalInteractionType pedestalInteractionType;

    public bool completed;

    private bool _waterBallActive;
    private bool _fireBallActive;
    private bool _interacted;

    public PedestalType pedestalType;
    [SerializeField] GameObject _waterBall, _fireBall, _potion, _pedestal, _lowerPedTarget, _raisedPedTarget;
    [SerializeField] List<Light> _lights;

    private void Update()
    {
        if (_waterBallActive)
        {
            _waterBall.SetActive(true);
            _fireBall.SetActive(false);
        }
        else if (_fireBallActive)
        {
            _fireBall.SetActive(true);
            _waterBall.SetActive(false);
        }
        if (pedestalInteractionType == PedestalInteractionType.Interactuable && _interacted)
        {
            Lower();
            foreach (Pedestal ped in _linkedObjects)
            {
                ped.Raise();
            }
        }
    }

    public override void Execute()
    {
        if (pedestalInteractionType == PedestalInteractionType.Interactuable)
        {
            _potion.SetActive(false);
            foreach (Light light in _lights)
            {
                light.gameObject.SetActive(false);
            }
            _pedestal.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            _interacted = true;
        }
        else if (pedestalInteractionType == PedestalInteractionType.BulletCollision)
        {
            foreach (Light light in _lights)
            {
                light.gameObject.SetActive(true);
            }
            _pedestal.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            completed = true;
        }
    }

    public override void StopExecute()
    {
        foreach (Light light in _lights)
        {
            light.gameObject.SetActive(false);
        }
        _pedestal.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pedestalInteractionType == PedestalInteractionType.Interactuable) return;
        if (completed) return;
        if ((pedestalType == PedestalType.Fire && other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Fire) || (pedestalType == PedestalType.Water && other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Water))
        {
            Execute();
        }
        if (other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Fire) { _fireBallActive = true; _waterBallActive = false; }
        else if (other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Water) { _waterBallActive = true; _fireBallActive = false; }
    }

    public void Raise()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, _raisedPedTarget.transform.position.y, gameObject.transform.position.z), 1f * Time.deltaTime);
    }

    public void Lower()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, _lowerPedTarget.transform.position.y, gameObject.transform.position.z), 1f * Time.deltaTime);
    }
}

public enum PedestalType
{
    Fire,
    Water
}

public enum PedestalInteractionType
{
    Interactuable,
    BulletCollision
}
