using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreComponent : MonoBehaviour
{
    public Text ScoreText;

    private Team team1;
    private Team team2;

    void Start ()
    {
        team1 = new Team("Team1", "Team 1");
        team2 = new Team("Team2", "Team 2");
        SetScoreText();
    }
    
    public void ScoreGoal(string teamId)
    {
        if(team1.Id == teamId)
        {
            team1.Score++;
        }
        else
        {
            team2.Score++;
        }
        SetScoreText();
    }

    public void SetScoreText()
    {
        ScoreText.text = team1.DisplayName + ": " + team1.Score.ToString() + "\n" + team2.DisplayName + ": " + team2.Score.ToString();
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
