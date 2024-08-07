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
        //_fsm.AddState(StatesEnums.NPCStateID.Idle, new IdleStateNPC(this, ));
        _fsm.AddState(StatesEnums.NPCStateID.Chase, new ChaseStateNPC(this));
        _fsm.AddState(StatesEnums.NPCStateID.Follow, new FollowStateNPC(this));
        //_fsm.AddState(StatesEnums.NPCStateID.Attack, new AttackStateNPC(this, ));
        _fsm.AddState(StatesEnums.NPCStateID.Pathfind, new PathFindStateNPC(this, redNPC ? GameManagerIA.instance.leaderConfig.redLeader : GameManagerIA.instance.leaderConfig.blueLeader));
        //_fsm.ChangeState(StatesEnums.NPCStateID.Escape, new EscapeStateNPC(this));
        _fsm.ChangeState(StatesEnums.NPCStateID.Pathfind);

        speedModifier = 1;
        currentlife = GameManagerIA.instance.npcConfig.maxLife;
        if (currentlife <= 0)
        {
            gameObject.transform.position = redNPC ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position;
            currentlife = GameManagerIA.instance.npcConfig.maxLife;
        }
        if (Vector3.Distance(transform.position, redNPC ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position) < 0.5f)
        {
            currentlife = GameManagerIA.instance.leaderConfig.maxLife;
        }
    }

    void Update()
    {
        lifeBar.fillAmount = currentlife / GameManagerIA.instance.npcConfig.maxLife;
        _fsm.OnUpdate();
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
