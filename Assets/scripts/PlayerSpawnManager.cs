using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawnManager : NetworkBehaviour {

    [SerializeField] Camera secondCamera;
     
    List<Transform> m_teams;

    bool isMainTeam = false;
    
	// Use this for initialization
	void Start () {
        m_teams = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
    }
        
    public bool SpawnPlayer(string _pitchPlayerPath)
    {
        var comp = Component.FindObjectOfType<UIDropZone>();
        bool inDropZone = RectTransformUtility.RectangleContainsScreenPoint(comp.GetComponent<RectTransform>(), Input.mousePosition);
        if (!inDropZone)
            return false;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Spawn player
        CmdSpawnPlayer(_pitchPlayerPath, pos.x, pos.y);
        return false;
    }

    [Command]
    void CmdSpawnPlayer(string _playerPrefab, float x, float y)
    {
        var player = Instantiate( Resources.Load("prefabs/players/"+_playerPrefab ) ) as GameObject;
        Utils.SetPositionX(player.transform, x);
        Utils.SetPositionY(player.transform, y);
        NetworkServer.Spawn(player);
    }

    [ClientRpc]
    public void RpcSetTeam(bool _isMainTeam)
    {
        if (isLocalPlayer)
        {
            isMainTeam = _isMainTeam;
            FindObjectOfType<CameraSwitcher>().SwitchCameras(_isMainTeam);
        }
    }
}
