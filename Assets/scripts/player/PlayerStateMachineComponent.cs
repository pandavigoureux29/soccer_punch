﻿using UnityEngine;
using System.Collections;

public class PlayerStateMachineComponent : MonoBehaviour
{
    public PlayerDataAsset PlayerData;
    private PlayerComponent playerComp;

    public enum PlayerState
    {
        JustSpawned = 0,
        Idle,
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

    public enum BallState
    {
        RunTo = 0,
        Block
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

    public enum DeadState
    {
        Fade = 0,
        Explode
    }

    public PlayerState CurrentState;
    public IdleState CurrentIdleState;
    public BallState CurrentBallState;
    public EnemyState CurrentEnemyState;
    public KickState CurrentKickState;
    public DeadState CurrentDeadState;

    void Start()
    {
        CurrentState = PlayerState.JustSpawned;
        playerComp = gameObject.GetComponent<PlayerComponent>();
    }

    void Update()
    {
        switch (CurrentState)
        {
            case PlayerState.JustSpawned:
                HandleJustSpawnedState();
                break;
            case PlayerState.Idle:
                HandleIdleState();
                break;
            case PlayerState.Ball:
                HandleBallState();
                break;
            case PlayerState.Enemy:
                HandleEnemyState();
                break;
            case PlayerState.Kick:
                HandleKickState();
                break;
            case PlayerState.Dead:
                HandleDeadState();
                break;
            default:
                return;
        }
    }

    public void HandleJustSpawnedState()
    {
    }

    public void HandleIdleState()
    {
        switch (CurrentIdleState)
        {
            case IdleState.Static:
                break;
            case IdleState.Patrolling:
                break;
            case IdleState.Running:
                break;
            case IdleState.BallAware:
                break;
            case IdleState.EnemyAware:
                break;
            default:
                return;
        }
    }

    public void HandleBallState()
    {
        switch (CurrentBallState)
        {
            case BallState.RunTo:
                break;
            case BallState.Block:
                break;
            default:
                return;
        }
    }

    public void HandleEnemyState()
    {
        switch (CurrentEnemyState)
        {
            case EnemyState.Fight:
                break;
            case EnemyState.Evade:
                break;
            default:
                return;
        }
    }

    public void HandleKickState()
    {
        switch (CurrentKickState)
        {
            case KickState.ToGoal:
                break;
            case KickState.Pass:
                break;
            case KickState.ToEnemy:
                break;
            default:
                return;
        }
    }
    public void HandleDeadState()
    {
        switch (CurrentDeadState)
        {
            case DeadState.Fade:
                Destroy(gameObject);
                break;
            case DeadState.Explode:
                break;
            default:
                return;
        }
    }
}