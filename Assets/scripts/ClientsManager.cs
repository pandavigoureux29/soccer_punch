using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ClientsManager : NetworkBehaviour {
    
    //Server
    List<int> m_teamsConnectionIds = new List<int>();
    public bool m_teamInfosSent = false;

    //Client
    public bool m_mainTeam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if( isServer && !m_teamInfosSent && m_teamsConnectionIds.Count > 1)
        {
            bool everybodyReady = true;
            foreach( var conn in NetworkServer.connections)
            {
                everybodyReady &= conn.isReady;
            }
            if( everybodyReady)
            {
                SendClientTeams();
            }
        }
    }

    #region Server
    public override void OnStartServer()
    { 
        base.OnStartServer();
        NetworkServer.RegisterHandler(MsgType.Connect, OnNewConnection);
    }


    void OnNewConnection(NetworkMessage netMsg)
    {
        m_teamsConnectionIds.Add(netMsg.conn.connectionId);
    }

    void SendClientTeams()
    {
        m_teamInfosSent = true;
        foreach (var conn in NetworkServer.connections)
        {
            var playerController = conn.playerControllers[0];
            if( playerController != null)
                playerController.gameObject.GetComponent<PlayerSpawnManager>().RpcSetTeam(conn.connectionId == m_teamsConnectionIds[0]);
        }
    }
    #endregion

    public override void OnStartClient()
    {
        base.OnStartClient();
        //NetworkServer.RegisterHandler(200, OnGetTeamInfo);
    }    
    
}
