using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerComponent : NetworkBehaviour {
    
    [SerializeField]
    SpriteRenderer m_renderer;
    
    [SyncVar]
    PlayerDataAsset playerData;
    
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
