using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerComponent : NetworkBehaviour {
    
    [SerializeField]
    SpriteRenderer m_renderer;
    
    [SyncVar]
    PlayerDataAsset playerData;
    
    protected string playerDataName;
    [SyncVar]
    private bool mainTeam;

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
}
