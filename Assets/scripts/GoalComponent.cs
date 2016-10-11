using UnityEngine;

public class GoalComponent : MonoBehaviour {

    //private GameObject ball;
    private ScoreComponent scoreComponent;

    void Start()
    {
        scoreComponent = FindObjectOfType<ScoreComponent>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (scoreComponent == null)
                return;

            scoreComponent.ScoreGoal(gameObject.tag);            
        }
    }
}
