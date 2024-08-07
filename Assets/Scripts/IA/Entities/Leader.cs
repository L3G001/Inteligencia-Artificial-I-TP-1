using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leader : SteeringAgent, IEntity, IDamageable
{
    FSM<StatesEnums.LeaderStateID> _fsm;
    public AttackType attackType;
    public bool redLeader;
    public List<Node> path;
    public GameObject bulletSpawner;
    public Material mat;
    public Image lifeBar;

    public float currentlife { get; set; }
    public float speedModifier { get; set; }
    public float speed { get; set; }

    public ObjectPool<Bullet> _currentPool
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
        _fsm = new FSM<StatesEnums.LeaderStateID>();
        speedModifier = 1;
        currentlife = GameManagerIA.instance.leaderConfig.maxLife;

        _fsm.AddState(StatesEnums.LeaderStateID.Idle, new IdleStateLeader(this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Pathfind, new PathfindLeaderState(this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Follow, new FollowPathLeaderState(this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Escape, new EscapeLeaderState(this, redLeader ? GameManagerIA.instance.npcConfig.redBase : GameManagerIA.instance.npcConfig.blueBase));
        _fsm.ChangeState(StatesEnums.LeaderStateID.Idle);
    }

    void Update()
    {
        _fsm.OnUpdate();
        lifeBar.fillAmount = currentlife / GameManagerIA.instance.leaderConfig.maxLife;
        if (currentlife <= 0) 
        { 
            gameObject.transform.position = redLeader ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position; 
            currentlife = GameManagerIA.instance.leaderConfig.maxLife;
        }
        if (Vector3.Distance(transform.position, redLeader ? GameManagerIA.instance.npcConfig.redBase.transform.position : GameManagerIA.instance.npcConfig.blueBase.transform.position) < 0.5f)
        {
            currentlife = GameManagerIA.instance.leaderConfig.maxLife;
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

    IEnumerator DOTTimer(float dmg, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            TakeDamage(dmg);
            yield return new WaitForSeconds(1);
        }
    }
}
