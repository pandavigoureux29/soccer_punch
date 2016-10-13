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
            if (statemachine.CurrentState == PlayerStateMachineComponent.PlayerState.Idle)
                statemachine.onBallAware(_coll.gameObject);
        }
        else if (LayerMask.LayerToName(_coll.gameObject.layer) == "player")
        {
            var otherPlayerComp = _coll.GetComponent<PlayerComponent>();
            if (otherPlayerComp != null && otherPlayerComp.IsMainTeam != player.IsMainTeam)
            {
                var statemachine = player.GetComponent<PlayerStateMachineComponent>();
                if (statemachine.CurrentState == PlayerStateMachineComponent.PlayerState.Idle)
                    statemachine.onEnemyAware(_coll.gameObject);
            }
        }
        else if (_coll.gameObject.name.Contains("goal"))
        {
            var goalComp = _coll.gameObject.GetComponent<GoalComponent>();
            if (goalComp.mainTeam != player.IsMainTeam)
            {
                var statemachine = player.GetComponent<PlayerStateMachineComponent>();
                if(statemachine.CurrentState == PlayerStateMachineComponent.PlayerState.Ball)
                {
                    statemachine.ChangeState(PlayerStateMachineComponent.PlayerState.Kick);
                }
                else if (statemachine.CurrentState == PlayerStateMachineComponent.PlayerState.Idle && statemachine.CurrentIdleState == PlayerStateMachineComponent.IdleState.Running)
                {
                    statemachine.ChangeState(PlayerStateMachineComponent.IdleState.BallAware);
                }
            }
        }
    }
}
