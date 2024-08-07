using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : SteeringAgent, IEntity, IDamageable
{
    FSM<StatesEnums.NPCStateID> _fsm;
    public AttackType attackType;
    public bool redNPC;
    public GameObject bulletSpawner;
    public Image lifeBar;
    public List<Node> path;

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

        _fsm = new FSM<StatesEnums.NPCStateID>();

        _fsm.AddState(StatesEnums.NPCStateID.Pathfind, new PathFindStateNPC(this, redNPC ? GameManagerIA.instance.leaderConfig.redLeader : GameManagerIA.instance.leaderConfig.blueLeader));
        _fsm.AddState(StatesEnums.NPCStateID.Chase, new ChaseStateNPC(this));
        _fsm.AddState(StatesEnums.NPCStateID.Follow, new FollowStateNPC(this));
        _fsm.AddState(StatesEnums.NPCStateID.Escape, new EscapeStateNPC(this, redNPC ? GameManagerIA.instance.npcConfig.redBase : GameManagerIA.instance.npcConfig.blueBase));
        _fsm.ChangeState(StatesEnums.NPCStateID.Chase);

        speedModifier = 1;
        currentlife = GameManagerIA.instance.npcConfig.maxLife;
        if (currentlife <= 0)
        {
            gameObject.transform.position = redNPC ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position;
            currentlife = GameManagerIA.instance.npcConfig.maxLife;
        }
        if (Vector3.Distance(transform.position, redNPC ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position) < 0.5f)
        {
            currentlife += 10 * Time.deltaTime;
            if (currentlife >= GameManagerIA.instance.npcConfig.maxLife)
            {
                currentlife = GameManagerIA.instance.npcConfig.maxLife;
            }
        }
    }

    void Update()
    {
        currentlife = GameManagerIA.instance.npcConfig.maxLife;
        lifeBar.fillAmount = currentlife / GameManagerIA.instance.npcConfig.maxLife;
        _fsm.OnUpdate();
        foreach (var agent in redNPC ? GameManagerIA.instance.npcConfig.blueAgents : GameManagerIA.instance.npcConfig.redAgents)
        {
            if (InFOV(agent.transform))
            {
                Attack();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentlife -= damage;
    }

    public void DOT(float damage, float duration)
    {
        StartCoroutine(DOTTimer(damage, duration));
    }

    public void Attack()
    {
        var currentBullet = _currentPool.GetObject();
        currentBullet.transform.position = bulletSpawner.transform.position;
        currentBullet.transform.rotation = bulletSpawner.transform.rotation;
        StartCoroutine(Cooldown());
    }

    public bool InFOV(Transform obj)
    {
        var dir = obj.position - transform.position;

        if (dir.magnitude <= GameManagerIA.instance.npcConfig.viewRadius)
        {
            if (Vector3.Angle(transform.right, dir) <= GameManagerIA.instance.npcConfig.viewAngle * 0.5f)
            {
                return GameManagerIA.instance.InLineOfSight(transform.position, obj.position);
            }
        }

        return false;
    }

    IEnumerator DOTTimer(float dmg, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(dmg);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Cooldown() { yield return new WaitForSeconds(2); }
}

public enum AttackType
{
    Fire,
    Water
}
