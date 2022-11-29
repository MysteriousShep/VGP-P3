using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public float speed = 1f;
    public float jumpSpeedBoost = 1.5f;
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //transform.Translate((Vector3.right)*movement.x*speed);
        //transform.Translate((Vector3.forward)*movement.y*speed);
        float yVel = rb.velocity.y;
        if (Input.GetButtonDown("Jump") && transform.position.y <= 0.5f)
        {
            yVel = 15.0f;

        }

        rb.velocity = new Vector3((movement.x+movement.y)*speed,yVel,(movement.y-movement.x)*speed);
        if (yVel != 0)
        {
            rb.velocity = new Vector3((movement.x+movement.y)*speed*jumpSpeedBoost,yVel,(movement.y-movement.x)*speed*jumpSpeedBoost);
        }
    }
    
}
