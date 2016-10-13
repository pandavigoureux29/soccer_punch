using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerBodyColliderComponent : MonoBehaviour
{

    [SerializeField]
    PlayerComponent player;
    
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
        if (LayerMask.LayerToName(_coll.gameObject.layer) == "ball")
        {
            var ballComp = _coll.gameObject.GetComponent<Ball>();
            if(ballComp != null && player != null && ballComp.GetOwner() == null)
            {
                ballComp.CatchBall(player);
                //player.playerStateMachine.ChangeState(PlayerStateMachineComponent.PlayerState.Ball);
                player.playerStateMachine.ChangeState(PlayerStateMachineComponent.PlayerState.Kick);
            }
        }
    }
}

