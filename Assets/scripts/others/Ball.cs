using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    PlayerComponent m_owner;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetBall(PlayerComponent player)
    {
        m_owner = player;
    }

    public void Releaseball()
    {
        m_owner = null;
    }

}
