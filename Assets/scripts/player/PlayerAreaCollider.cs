using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerAreaCollider : NetworkBehaviour {

    [SerializeField] PlayerComponent player;

    // Use this for initialization
    public override void OnStartServer()
    {
        base.OnStartServer();

    }
    	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D _coll)
    {
        if( LayerMask.LayerToName( _coll.gameObject.layer ) == "ball")
        {
            var statemachine = player.GetComponent<PlayerStateMachineComponent>();
            statemachine.onBallAware(_coll.gameObject);
        }else if( LayerMask.LayerToName( _coll.gameObject.layer) == "player")
        {
            var otherPlayerComp = _coll.GetComponent<PlayerComponent>();
            if (otherPlayerComp != null &&  otherPlayerComp.IsMainTeam == player.IsMainTeam)
            {
                var statemachine = player.GetComponent<PlayerStateMachineComponent>();
                statemachine.onBallAware(_coll.gameObject);
            }
        }
    }
}
