using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerBodyColliderComponent : NetworkBehaviour
{

    [SerializeField]
    PlayerComponent player;

    // Use this for initialization
    public override void OnStartServer()
    {
        base.OnStartServer();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.gameObject.name == "field")
        {
            if (player != null && player.IsPatrolling)
            {
                player.ReversePatrolOrientation();
            }
        }
    }
}

