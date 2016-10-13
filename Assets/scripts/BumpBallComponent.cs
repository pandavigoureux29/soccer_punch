using UnityEngine;
using System.Collections;

public class BumpBallComponent : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb;
    private Vector3 oldVelocity;

    // Use this for initialization
    void Start ()
    {
        SearchBall();
    }

    void SearchBall()
    {
        var ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
            rb = ball.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
            oldVelocity = rb.velocity;
        else
            SearchBall();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (rb == null)
                return;

            ContactPoint2D cp = col.contacts[0];
            rb.velocity = Vector3.Reflect(oldVelocity, cp.normal);

            // bumper effect to speed up ball
            rb.velocity -= cp.normal * speed;
        }
    }
    
}
