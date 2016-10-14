using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;

public class Ball : NetworkBehaviour
{
    [SyncVar]
    public int OwnerId;
    private PlayerComponent m_owner;


    // Use this for initialization
    public override void OnStartServer()
    {
        base.OnStartServer();
        OwnerId = -1;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if( !isServer )
            Destroy(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update () {
        if (OwnerId != -1)
        {
            m_owner = FindObjectsOfType<PlayerComponent>().FirstOrDefault(p => p.PlayerId == OwnerId);
        }
        else
        {
            m_owner = null;
        }
	}

    public void CatchBall(PlayerComponent player)
    {
        if (isServer)
        {
            m_owner = player;
            OwnerId = player.PlayerId;
            
            StopBallMovement();
        }
    }

    public PlayerComponent GetOwner()
    {
        return m_owner;
    }

    public void Releaseball()
    {
        if (isServer)
        {
            m_owner = null;
            OwnerId = -1;
        }
    }

    public void StopBallMovement()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;
    }

}
