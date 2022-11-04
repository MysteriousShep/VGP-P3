using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public float speed = 1f;
    //public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        transform.Translate((Vector3.right)*movement.x*speed);
        transform.Translate((Vector3.forward)*movement.y*speed);
        //rb.velocity = new Vector3(movement.x,0,movement.y);
    }
    
}
