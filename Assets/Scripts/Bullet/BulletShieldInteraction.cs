using UnityEngine;

public class BulletShieldInteraction : MonoBehaviour
{
    [SerializeField] float _projectileSpeed, _lifeTime;
    [SerializeField] GameObject _hitEffect;
    Vector3 _hitNormal, _hitPos;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        Vector3 delta = (transform.forward * _projectileSpeed * Time.deltaTime);
        transform.position += delta;
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            _hitNormal = hit.normal;
            _hitPos = hit.point;
            float distance = (transform.position - _hitPos).magnitude;
            if (distance < delta.magnitude)
            {
                Hit(hit.collider);
            }
        }
    }

    void Hit(Collider collider)
    {
        ShieldShader shield = collider.GetComponent<ShieldShader>();
        if (shield != null)
        {
            shield.HitShield(_hitPos);
        }
        hit.transform.forward = _hitNormal;
        Destroy(gameObject);
    }
}
