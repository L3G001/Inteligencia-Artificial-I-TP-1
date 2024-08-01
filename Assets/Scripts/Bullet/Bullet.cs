using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float projectileSpeed;
    public float lifeTime;
    private Vector3 _hitNormal;
    private Vector3 _hitPos;
    private Ray _ray;
    private RaycastHit _hit;
    private float _currentLifeTime;

    [Header("Damage Settings")]
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _dotDamage;
    [SerializeField] private float _dotDuration;
    [SerializeField] private float _speedReduction;

    public BulletType bulletType;
    public LayerMask BulletOrigin;

    void OnEnable()
    {
        _currentLifeTime = lifeTime;
    }

    void Update()
    {
        Vector3 delta = (transform.forward * projectileSpeed * Time.deltaTime);
        transform.position += delta;
        _ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(_ray, out _hit, delta.magnitude))
        {
            _hitNormal = _hit.normal;
            _hitPos = _hit.point;
            Hit(_hit.collider);
        }

        Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BulletOrigin == (BulletOrigin | (1 << other.gameObject.layer)))
        {
            return;
        }

        IDamageable obj;
        if (other.TryGetComponent<IDamageable>(out obj))
        {
            float scaleModifier = gameObject.transform.localScale.x;
            obj.TakeDamage(_bulletDamage);

            if (bulletType == BulletType.Fire)
            {
                obj.DOT(_dotDamage, _dotDuration);
            }
        }

        gameObject.SetActive(false);
    }

    private void Hit(Collider collider)
    {
        ShieldShader shield = collider.GetComponent<ShieldShader>();
        if (shield != null)
        {
            shield.HitShield(_hitPos);
        }
        // hit.transform.forward = _hitNormal;
    }

    private void Death()
    {
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0f)
        {
            gameObject.transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}

public enum BulletType
{
    Fire,
    Water
}