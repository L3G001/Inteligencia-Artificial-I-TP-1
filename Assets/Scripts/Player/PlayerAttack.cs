using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking, _canAttack;
    public bool waterUnlocked = false;
    public bool fireUnlocked = false;
    [SerializeField] private GameObject _bulletSpawn, _waterBullet, _fireBullet;
    private Bullet _currentBullet;
    private ObjectPool<Bullet> _currentPool
    {
        get
        {
            if (_waterBullet.activeSelf) { return GameManager.instance.waterBulletPool; }
            else if (_fireBullet.activeSelf) { return GameManager.instance.fireBulletPool; }
            else { return null; }
        }
    }
    private float _projectileSpeed;
    private Transform _ogParent;

    private void OnDisable()
    {
        GameManager.instance.inputReader.AttackEvent -= AttackStartHandle;
        GameManager.instance.inputReader.AttackCancelledEvent -= AttackCancelledHandle;
        GameManager.instance.inputReader.ChangeWeaponEvent -= ChangeBulletHandle;
    }

    void Start()
    {
        _canAttack = true;
        GameManager.instance.inputReader.AttackEvent += AttackStartHandle;
        GameManager.instance.inputReader.AttackCancelledEvent += AttackCancelledHandle;
        GameManager.instance.inputReader.ChangeWeaponEvent += ChangeBulletHandle;
    }

    void Update()
    {
        if (_currentPool == null) { return; }
        if (_isAttacking && _canAttack)
        {
            ChargeBullet();
        }
        else if (!_isAttacking && _currentBullet != null)
        {
            ShootBullet();
            _canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    void AttackStartHandle()
    {
        _isAttacking = true;
        if (_canAttack && _currentPool != null)
        {
            _currentBullet = _currentPool.GetObject();
            _projectileSpeed = _currentBullet.projectileSpeed;
            _currentBullet.projectileSpeed = 0;
            _currentBullet.transform.position = _bulletSpawn.transform.position;
            _currentBullet.transform.rotation = _bulletSpawn.transform.rotation;
            _currentBullet.gameObject.transform.localScale = Vector3.zero;
            _ogParent = _currentBullet.transform.parent;
            _currentBullet.transform.parent = _bulletSpawn.transform;
        }
    }

    void AttackCancelledHandle()
    {
        _isAttacking = false;
    }

    void ChangeBulletHandle()
    {
        ChangeBullet();
    }

    void ChargeBullet()
    {
        if (_currentBullet != null)
        {
            _currentBullet.gameObject.transform.localScale = Vector3.Lerp(_currentBullet.gameObject.transform.localScale, new Vector3(1, 1, 1), 50 * Time.deltaTime);
            if (_currentBullet.gameObject.transform.localScale.x >= 0.9f)
            {
                _isAttacking = false;
            }
        }
    }

    void ShootBullet()
    {
        if (_currentBullet != null)
        {
            _currentBullet.transform.parent = _ogParent;
            _currentBullet.projectileSpeed = _projectileSpeed;
            _currentBullet = null;
        }
    }

    void ChangeBullet()
    {
        if(_waterBullet.activeSelf && fireUnlocked)
        {
            GameManager.instance.staffAnimator.SetBool("ChangeToWater", false);
            GameManager.instance.staffAnimator.SetBool("ChangeToFire", true);
        }
        else if(_fireBullet.activeSelf && waterUnlocked)
        {
            GameManager.instance.staffAnimator.SetBool("ChangeToWater", true);
            GameManager.instance.staffAnimator.SetBool("ChangeToFire", false);
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
}
