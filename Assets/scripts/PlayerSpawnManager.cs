using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawnManager : NetworkBehaviour {

    List<Transform> m_teams;
    
	// Use this for initialization
	void Start () {
        m_teams = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    public bool SpawnPlayer(string _pitchPlayerPath)
    {
        CmdSpawnPlayer(_pitchPlayerPath);
        return true;
    }

    [Command]
    void CmdSpawnPlayer(string _playerPrefab)
    {
       var player = Instantiate( Resources.Load("prefabs/players/"+_playerPrefab ) ) as GameObject;
       NetworkServer.Spawn(player);
    }
}
