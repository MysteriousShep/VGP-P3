using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float turnSpeed;
    public float horizontalInput;
    public float forwardInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get vertical input
        forwardInput = Input.GetAxis("Vertical");
        // Move vehicle forwards or backwards
        transform.Translate(Vector3.forward*Time.deltaTime*speed*forwardInput);
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        // Turn vehicle
        transform.Rotate(Vector3.up,Time.deltaTime*turnSpeed*horizontalInput);

    }
}
