using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private float verticalInput;
    private float horizontalInput;
    public float turnSpeed;
    public float rotationCorrection;
    private float accelerateInput;
    private bool jumpInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        accelerateInput = Input.GetAxis("Fire1");
        jumpInput = Input.GetButtonDown("Jump");
        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * Time.deltaTime*(speed+speed*accelerateInput*0.75f));
        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput*(1.0f+accelerateInput*0.75f));
        // move & tilt the plane based on left/right arrow keys
        transform.Rotate(Vector3.forward, ((-turnSpeed * Time.deltaTime * horizontalInput)+180-(((transform.rotation.z+(transform.rotation.z/rotationCorrection))*Time.deltaTime)+180))*(1.0f+accelerateInput*0.75f));
        transform.Rotate(Vector3.up,turnSpeed*Time.deltaTime*horizontalInput*(1.0f+accelerateInput*0.75f));
    }
}
