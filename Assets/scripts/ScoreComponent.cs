using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreComponent : MonoBehaviour
{
    public Text ScoreTextTeam1;
    public Text ScoreTextTeam2;

    private Team team1;
    private Team team2;

    void Start ()
    {
        team1 = new Team("Team1", "Team 1");
        team2 = new Team("Team2", "Team 2");
        SetScoreText();
    }
    
    public void ScoreGoal(string goalTag)
    {
        if(team1.Id == goalTag)
        {
            team2.Score++;
        }
        else
        {
            team1.Score++;
        }
        SetScoreText();
    }

    public void SetScoreText()
    {
        ScoreTextTeam1.text = team1.Score.ToString();
        ScoreTextTeam2.text = team2.Score.ToString();
    }
}

public class Team
{
    public string DisplayName;
    public string Id;
    public int Score;
    public List<string> Players;

    public Team(string id, string displayName, int score = 0)
    {
        Id = id;
        DisplayName = displayName;
        Score = score;
    }
}
