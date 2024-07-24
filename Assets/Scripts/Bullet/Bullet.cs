using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] public float projectileSpeed, lifeTime;
    Vector3 _hitNormal, _hitPos;
    Ray ray;
    RaycastHit hit;
    float _currentLifeTime;

    [Header("Damage Settings")]
    [SerializeField] float _bulletDamage;
    [SerializeField] float _dotDamage, _dotDuration;
    [SerializeField] float _speedReduction;

    public BulletType bulletType;
    public LayerMask BulletOrigin;

    void Update()
    {
        Vector3 delta = (transform.forward * projectileSpeed * Time.deltaTime);
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
        if (delta != Vector3.zero) { Death(); }
        else { _currentLifeTime = lifeTime; }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable obj;
        if (other.TryGetComponent<IDamageable>(out obj))
        {
            if (BulletOrigin.Equals(other.gameObject.layer))
            {
                return;
            }
            else
            {
                if (bulletType == BulletType.Fire)
                {
                    obj.TakeDamage(_bulletDamage * gameObject.transform.localScale.x);
                    obj.DOT(_dotDamage * gameObject.transform.localScale.x, _dotDuration);
                }
                else if (bulletType == BulletType.Water)
                {
                    obj.TakeDamage(_bulletDamage * gameObject.transform.localScale.x);
                }
            }
        }
        gameObject.SetActive(false);
    }

    void Hit(Collider collider)
    {
        ShieldShader shield = collider.GetComponent<ShieldShader>();
        if (shield != null)
        {
            shield.HitShield(_hitPos);
        }
        hit.transform.forward = _hitNormal;
        gameObject.SetActive(false);
    }

    void Death()
    {
        _currentLifeTime -= Time.deltaTime;
        if (lifeTime <= 0.1f)
        {
            gameObject.SetActive(false);
            _currentLifeTime = lifeTime;
        }
    }
}

public enum BulletType
{
    Fire,
    Water
}
