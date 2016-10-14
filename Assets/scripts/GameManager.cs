using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {

    List<Team> m_teams;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        m_teams = new List<Team>() { new Team(), new Team() };
    }

    public void AddPlayer(PlayerComponent player)
    {
        int index = player.IsMainTeam ? 0 : 1;
        m_teams[index].AddPlayer(player);
    }

    public void AddScore(bool mainTeam)
    {
        int index = mainTeam ? 0 : 1;
        m_teams[index].Score += 1;
        RpcScoreChanged(m_teams[0].Score, m_teams[1].Score);
        //ResetField();
    }

    [ClientRpc]
    public void RpcScoreChanged(int scoreMain, int scoreOther)
    {
        var scoreComp = FindObjectOfType<ScoreComponent>();
        scoreComp.SetScore(scoreMain, scoreOther);
        ResetField();
    }

    public void ResetField()
    {
        var players = FindObjectsOfType<PlayerComponent>();
        foreach(var player in players)
        {
            if (player == null || player.playerStateMachine == null)
            {

            }
            else
            {
                player.playerStateMachine.ChangeState(PlayerStateMachineComponent.PlayerState.Dead);
            }
        }

        var ball = FindObjectOfType<Ball>();
        if(ball != null)
        {
            ball.StopBallMovement();
            ball.transform.position = Vector3.zero;
        }
    }

    public class Team
    {
        public string Name;
        public List<PlayerComponent> players;
        public int Score;

        public void AddPlayer(PlayerComponent player)
        {
            if (players == null)
                players = new List<PlayerComponent>();
            players.Add(player);
        }
    }

}
