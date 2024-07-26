using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pedestal : Interactuable
{
    public bool firstActivation, secondActivation, completed;
    private bool _waterBallActive, _fireBallActive, _interacted;
    public PedestalType _pedestalType;
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
        if (firstActivation && _interacted)
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
        if (firstActivation)
        {
            _potion.SetActive(false);
            foreach (Light light in _lights)
            {
                light.gameObject.SetActive(false);
            }
            _pedestal.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            _interacted = true;
        }
        else if (secondActivation)
        {
            foreach (Light light in _lights)
            {
                light.gameObject.SetActive(true);
            }
            _pedestal.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            completed = true;
            GameManager.instance.puzflag += 1;
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
        if (firstActivation) return;
        if (completed) return;
        if ((_pedestalType == PedestalType.Fire && other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Fire) || (_pedestalType == PedestalType.Water && other.gameObject.GetComponent<Bullet>().bulletType == BulletType.Water))
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
