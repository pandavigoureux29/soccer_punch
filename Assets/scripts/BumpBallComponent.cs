using UnityEngine;
using System.Collections;

public class BumpBallComponent : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb;
    private Vector3 oldVelocity;

    // Use this for initialization
    void Start ()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (rb != null)
            oldVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (rb == null || oldVelocity == null)
                return;

            ContactPoint2D cp = col.contacts[0];
            rb.velocity = Vector3.Reflect(oldVelocity, cp.normal);

            // bumper effect to speed up ball
            rb.velocity -= cp.normal * speed;
        }
    }
    
}
