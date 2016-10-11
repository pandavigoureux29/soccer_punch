using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0,
        Ball,
        Enemy,
        Kick,
        Dead
    }

    public enum IdleState
    {
        Static = 0,
        Patrolling,
        Running,
        BallAware,
        EnemyAware
    }

    public enum EnemyState
    {
        Fight = 0,
        Evade
    }

    public enum KickState
    {
        ToGoal = 0,
        Pass,
        ToEnemy
    }

    public string PrefabPath;
    public float Cooldown;
    public float MaxLife;
    public float LifeCost;
    public float Speed;
    public float TravelDistance;
    public float ActionRadius;
    public Time AwarenessDuration;
    public float KickSpeed;
    public float KickPrecision;
    public float BallCatchThreshold;
    public Quaternion ActionAngle;
    public float OffensiveStrength;
    public float DefensiveStrength;
    public PlayerState CurrentState;
    public IdleState CurrentIdleState;
    public EnemyState CurrentEnemyState;
    public KickState CurrentKickState;

    void Start()
    {
        CurrentState = PlayerState.Idle;
        CurrentIdleState = IdleState.Static;
    }
}
