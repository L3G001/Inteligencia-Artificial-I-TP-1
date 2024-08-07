using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : SteeringAgent, IEntity, IDamageable
{
    FSM<StatesEnums.LeaderStateID> _fsm;
    public AttackType attackType;
    public bool redLeader;
    public List<Node> path;
    public GameObject bulletSpawner;
    public Material mat;

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

        _fsm.AddState(StatesEnums.LeaderStateID.Idle, new IdleStateLeader(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Pathfind, new PathfindLeaderState(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Follow, new FollowPathLeaderState(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.OnSight, new OnSightLeaderState(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Escape, new EscapeLeaderState(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.AddState(StatesEnums.LeaderStateID.Attack, new AttackLeaderState(_fsm, this, redLeader ? GameManagerIA.instance.redLeaderTarget : GameManagerIA.instance.blueLeaderTarget));
        _fsm.ChangeState(StatesEnums.LeaderStateID.Idle);
    }

    void Update()
    {
        speed = _maxSpeed * speedModifier;
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
