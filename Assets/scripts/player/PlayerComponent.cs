﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerComponent : NetworkBehaviour {
    
    [SerializeField]
    SpriteRenderer m_renderer;
    
    [SyncVar]
    protected string playerDataName;
    [SyncVar]
    private bool mainTeam;

    [SyncVar]
    public float CurrentHealth;
    [SyncVar]
    public float DistanceLeftToRun;

    public PlayerStateMachineComponent playerStateMachine;

    protected PlayerDataAsset m_playerData;

    //Run
    private Vector3 oldDestination = new Vector3(999, 999, 999);
    private Vector3 oldMovement;

    //Patrol
    public bool IsPatrolling = false;
    private List<Vector3> patrollingLimitPositions = new List<Vector3>();
    public int GoingTowardsPosIndex;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        var spawnManager = new List<PlayerSpawnManager>(Component.FindObjectsOfType<PlayerSpawnManager>()) {}.Find(x=>x.isLocalPlayer);
        if (spawnManager != null)
        {
            m_playerData = Instantiate(Resources.Load("data/" + playerDataName)) as PlayerDataAsset;
            //if the same team
            if ( spawnManager.IsMainTeam == IsMainTeam )
            {
                m_renderer.sprite = m_playerData.imageA;
            }
            else
            {
                m_renderer.sprite = m_playerData.imageB;
            }
        }
    }
    

    void Update()
    {
        TakeDamage(Time.deltaTime * m_playerData.LifeCost);

        if (IsPatrolling)
            patrolStep();
    }

    public string PlayerDataName
    {
        get
        {
            return playerDataName;
        }

        set
        {
            playerDataName = value;
        }
    }

    public PlayerDataAsset PlayerData
    {
        get
        {
            return m_playerData;
        }
        set
        {
            m_playerData = value;
        }
        
    }

    public bool IsMainTeam
    {
        get
        {
            return mainTeam;
        }

        set
        {
            mainTeam = value;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isServer)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0f)
            {
                playerStateMachine.OnDead();
            }
        }
    }
    

    public void RunTo(Vector3 destination)
    {
        if (isServer)
        {
            var progress = destination - transform.position;
            if (progress.magnitude > 0.2f && DistanceLeftToRun > 0f)
            {
                Vector3 movement = Vector3.zero;
                if (destination == oldDestination)
                {
                    movement = oldMovement;
                }
                else
                {
                    movement = destination - transform.position;
                    movement *= Time.deltaTime * PlayerData.Speed / 100f;
                    oldMovement = movement;
                    oldDestination = destination;
                }
                transform.Translate(movement);
                DistanceLeftToRun -= movement.sqrMagnitude;
            }
            else if (IsPatrolling && DistanceLeftToRun > 0f)
            {
                ReversePatrolOrientation();
            }
        }
    }

    public void Patrol()
    {
        if (patrollingLimitPositions == null)
            patrollingLimitPositions = new List<Vector3>();
        patrollingLimitPositions.Clear();
        var position1 = transform.position;
        position1.x -= PlayerData.PatrollingDistance;
        var position2 = transform.position;
        position2.x += PlayerData.PatrollingDistance;
        patrollingLimitPositions.Add(position1);
        patrollingLimitPositions.Add(position2);

        GoingTowardsPosIndex = 0;
        IsPatrolling = true;
    }

    private void patrolStep()
    {
        var destination = patrollingLimitPositions[GoingTowardsPosIndex];
        RunTo(destination);
    }

    public void ReversePatrolOrientation()
    {
        if (isServer)
        {
            GoingTowardsPosIndex = GoingTowardsPosIndex == 0 ? 1 : 0;
        }
    }

    public void StopPatrol()
    {
        IsPatrolling = false;
    }

    public GameObject FindOpposingTeamGoal()
    {
        var goalComp = FindObjectsOfType<GoalComponent>().First(g => g.mainTeam == mainTeam);
        if (goalComp != null)
            return goalComp.gameObject;
        return null;
    }
}
