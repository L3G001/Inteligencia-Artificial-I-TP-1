using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] BillboardType _billboardType = default;

    [Header("Lock Rotation")]
    [SerializeField] bool _lockX, _lockY, _lockZ;

    private Vector3 _originalRotation;
    
    public enum BillboardType { LookAtCamera, CameraForward }

    private void Awake()
    {
        _originalRotation = transform.rotation.eulerAngles;
    }

    void LateUpdate()
    {
        switch (_billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
                break;
        }
        Vector3 rotation = transform.rotation.eulerAngles;
        if (_lockX) { rotation.x = _originalRotation.x; }
        if (_lockY) { rotation.y = _originalRotation.y; }
        if (_lockZ) { rotation.z = _originalRotation.z; }
        transform.rotation = Quaternion.Euler(rotation);
    }
}

