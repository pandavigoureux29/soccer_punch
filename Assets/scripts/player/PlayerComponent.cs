using UnityEngine;
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
    public bool IsRunning = false;
    private Vector3 runDestination = Vector3.zero;
    
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
        if (IsRunning)
        {
            runStep();
        }
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
        runDestination = destination;
        IsRunning = true;
    }

    private void runStep()
    {
        if (isServer)
        {
            var movement = runDestination - transform.position;
            if (movement.magnitude == 0f || DistanceLeftToRun <= 0f)
            {
                IsRunning = false;
            }
            else
            {
                movement *= Time.deltaTime * PlayerData.Speed / 100f;
                transform.Translate(movement);
                DistanceLeftToRun -= movement.sqrMagnitude;
            }
        }
    }

    public GameObject FindOpposingTeamGoal()
    {
        var goalComp = FindObjectsOfType<GoalComponent>().First(g => g.mainTeam == mainTeam);
        if (goalComp != null)
            return goalComp.gameObject;
        return null;
    }
}
