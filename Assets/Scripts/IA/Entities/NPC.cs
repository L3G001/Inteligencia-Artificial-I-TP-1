using System.Collections;
using UnityEngine;

public class NPC : SteeringAgent, IEntity, IDamageable
{
    FSM<StatesEnums.NPCStateID> _fsm;
    public AttackType attackType;
    public bool redNPC;
    public GameObject bulletSpawner;

    public float currentlife { get; set; }
    public float speedModifier { get; set; }
    public float speed { get; set; }

    private ObjectPool<Bullet> _currentPool
    {
        get
        {
            if (attackType == AttackType.Water) { return GameManager.instance.waterBulletPool; }
            else if (attackType == AttackType.Fire) { return GameManager.instance.fireBulletPool; }
            else { return null; }
        }
    }

    void Start()
    {
        if (redNPC) { GameManagerIA.instance.npcConfig.redAgents.Add(this); }
        else { GameManagerIA.instance.npcConfig.blueAgents.Add(this); }
        speedModifier = 1;
        currentlife = GameManagerIA.instance.npcConfig.maxLife;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentlife -= damage;
    }

    public void DOT(float damage, float duration)
    {
        StartCoroutine(DOTTimer(damage, duration));
    }

    IEnumerator DOTTimer(float dmg, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(dmg);
            yield return new WaitForSeconds(1);
        }
    }
}

public enum AttackType
{
    Fire,
    Water
}
