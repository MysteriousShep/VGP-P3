using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 100;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up*jumpForce);
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isOnGround = false;
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }
    }

    void OnCollisionEnter()
    {
        isOnGround = true;
    }
}
