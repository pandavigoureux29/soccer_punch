﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class PlayerStateMachineComponent : NetworkBehaviour
{
    private PlayerDataAsset playerData;
    private PlayerComponent playerComp;
    public event Action<GameObject> BallAwareEvent;
    public event Action<GameObject> BallCaughtEvent;
    public event Action<GameObject> EnemyAwareEvent;
    public event Action DeadEvent;

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

    public override void OnStartServer()
    {
        base.OnStartServer();
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
        initStates();
    }

    private void initStates()
    {
        var preference = playerData.PreferredPlayerState;
        var states = preference.GetSortedStates();
        CurrentState = ParseEnum<PlayerState>(states[0]);

        preference = playerData.PreferredIdleState;
        states = preference.GetSortedStates();
        CurrentIdleState = ParseEnum<IdleState>(states[0]);

        preference = playerData.PreferredBallState;
        states = preference.GetSortedStates();
        CurrentBallState = ParseEnum<BallState>(states[0]);

        preference = playerData.PreferredEnemyState;
        states = preference.GetSortedStates();
        CurrentEnemyState = ParseEnum<EnemyState>(states[0]);

        preference = playerData.PreferredKickState;
        states = preference.GetSortedStates();
        CurrentKickState = ParseEnum<KickState>(states[0]);

        preference = playerData.PreferredDeadState;
        states = preference.GetSortedStates();
        CurrentDeadState = ParseEnum<DeadState>(states[0]);
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
                //Destroy(gameObject);
                break;
            case DeadState.Explode:
                break;
            default:
                return;
        }
    }
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    #region EVENTS HANDLERS

    public void OnDead()
    {
        CurrentState = PlayerState.Dead;
    }

    public void onBallAware(GameObject _ballGO)
    {
        if (BallAwareEvent != null)
            BallAwareEvent.Invoke(_ballGO);
    }

    public void onPlayerAware(GameObject _player)
    {
        if (EnemyAwareEvent != null)
            EnemyAwareEvent.Invoke(_player);
    }


    #endregion
}
