using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    bool _interactuableInRange = false;
    bool _interacted = false;

    [Header("Raycast Settings")]
    [SerializeField] float _radius = default;
    [SerializeField] float _range = default;
    private RaycastHit _hit;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        RaycastHit hit2;
        Physics.SphereCast(transform.position, _radius, transform.TransformDirection(Vector3.forward), out hit2, _range);
        Gizmos.DrawWireSphere(hit2.point, _radius);
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _range);
    }

    void Start()
    {
        GameManager.instance._inputReader.InteractEvent += HandleInteract;
    }

    void Update()
    {
        if ((Physics.SphereCast(transform.position, _radius, transform.TransformDirection(Vector3.forward), out _hit, _range) && _hit.collider.gameObject.GetComponent<Pedestal>() != null))
        {
            var obj = _hit.collider.gameObject.GetComponent<Pedestal>();
            _interactuableInRange = true;
            if (obj.firstActivation) { PlayerUIManager.Instance._interactUI.SetActive(true); }
            if (_interacted && obj.firstActivation)
            {
                obj.Execute();
                if(obj._pedestalType == PedestalType.Fire)
                {
                    GameManager.instance.staffAnimator.SetBool("ChangeToFire",true);
                    GameManager.instance.staffAnimator.SetBool("ChangeToWater",false);
                }
                else if (obj._pedestalType == PedestalType.Water)
                {
                    GameManager.instance.staffAnimator.SetBool("ChangeToWater",true);
                    GameManager.instance.staffAnimator.SetBool("ChangeToFire",false);
                }
                _interacted = false;
            }
        }
        else
        {
            _interactuableInRange = false;
            PlayerUIManager.Instance._interactUI.SetActive(false);
        }
    }

    void HandleInteract() { _interacted = true; }
}
