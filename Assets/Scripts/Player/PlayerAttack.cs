using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking = default;

    void Start()
    {
        GameManager.instance._inputReader.AttackEvent += AttackStartHandle;
        GameManager.instance._inputReader.AttackCancelledEvent += AttackCancelledHandle;
    }

    void Update()
    {
        
    }

    void AttackStartHandle()
    {
        _isAttacking = true;
    }

    void AttackCancelledHandle()
    {
        _isAttacking = false;
    }
}
