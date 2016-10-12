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
        FindObjectOfType<PlayerGenerator>().StartGeneration();
    }
    
    // Update is called once per frame
    void Update () {
    }
        
    public bool SpawnPlayer(PlayerDataAsset _playerData)
    {
        var comp = Component.FindObjectOfType<UIDropZone>();
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool inDropZone = RectTransformUtility.RectangleContainsScreenPoint(comp.GetComponent<RectTransform>(), pos);
        if (!inDropZone)
            return false;
        //Spawn player

        CmdSpawnPlayer(_playerData.name, pos.x, pos.y, IsMainTeam);
        return true;
    }

    [Command]
    void CmdSpawnPlayer(string _playerDataName, float x, float y, bool _mainTeam)
    {
        PlayerDataAsset data = Instantiate(Resources.Load("data/" + _playerDataName)) as PlayerDataAsset;
        var player = Instantiate( Resources.Load("prefabs/players/"+ data.PrefabPath ) ) as GameObject;
        Utils.SetPositionX(player.transform, x);
        Utils.SetPositionY(player.transform, y);
        //Set values
        var playerComponent = player.GetComponent<PlayerComponent>();
        playerComponent.PlayerDataName = _playerDataName;
        playerComponent.IsMainTeam = _mainTeam;
        NetworkServer.Spawn(player);
    }

    [ClientRpc]
    public void RpcSetTeam(bool _isMainTeam)
    {
        if (isLocalPlayer)
        {
            isMainTeam = _isMainTeam;
            FindObjectOfType<CameraSwitcher>().SwitchCameras(_isMainTeam);
            FindObjectOfType<PlayerGenerator>().StartGeneration();
        }
    }

    public bool IsMainTeam
    {
        get { return isMainTeam; }
    }
}
