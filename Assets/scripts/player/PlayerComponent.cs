using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerComponent : NetworkBehaviour {
    
    [SerializeField]
    SpriteRenderer m_renderer;
    
    PlayerDataAsset playerData;

    [SyncVar]
    protected string playerDataName;
    [SyncVar]
    private bool mainTeam;

    [SyncVar]
    public float CurrentHealth;

    PlayerStateMachineComponent playerStateMachine;

    protected PlayerDataAsset m_playerData;
    
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
            CurrentHealth = m_playerData.MaxLife;
            playerStateMachine = GetComponent<PlayerStateMachineComponent>();
        }
    }

    void Update()
    {
        TakeDamage(m_playerData.LifeCost);
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
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0f)
        {
            playerStateMachine.OnDead();
        }
    }
}
