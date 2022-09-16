using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public float turnSpeed;
    public float rotationCorrection;
    public float accelerateInput;

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
        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * Time.deltaTime*(speed+speed*accelerateInput));

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);
        //transform.Rotate(Vector3.forward, (-turnSpeed * Time.deltaTime * horizontalInput)-((transform.rotation.z+(transform.rotation.z/rotationCorrection))*Time.deltaTime));
        transform.Rotate(Vector3.up,turnSpeed*Time.deltaTime*horizontalInput);
    }
}
