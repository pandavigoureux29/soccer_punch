using UnityEngine;
using UnityEngine.Networking;

public class GoalComponent : NetworkBehaviour {

    [SerializeField] bool mainTeam;

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isServer)
        {
            if (col.gameObject.tag == "Ball")
            {
                FindObjectOfType<GameManager>().AddScore(mainTeam);
            }
        }
    }
}
