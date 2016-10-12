using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerComponent : NetworkBehaviour {
    
    [SerializeField]
    SpriteRenderer m_renderer;

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

    public PlayerState CurrentState;
    public IdleState CurrentIdleState;
    public EnemyState CurrentEnemyState;
    public KickState CurrentKickState;

    [SyncVar]
    PlayerDataAsset playerData;

    void Start()
    {
        CurrentState = PlayerState.Idle;
        CurrentIdleState = IdleState.Static;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        var spawnManager = Component.FindObjectOfType<PlayerSpawnManager>();
        if (spawnManager != null)
        {
            if (spawnManager.IsMainTeam)
            {
                m_renderer.sprite = playerData.imageA;
            }
            else
            {
                m_renderer.sprite = playerData.imageB;
            }
        }
    }


    public PlayerDataAsset PlayerData
    {
        get
        {
            return playerData;
        }

        set
        {
            playerData = value;
        }
    }
}
