using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrail : MonoBehaviour
{
    public GameObject player;
    public TrailRenderer tRenderer;
    private bool jumpInput;
    private float emittionTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        tRenderer = GetComponent<TrailRenderer>();
        tRenderer.emitting = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        jumpInput = Input.GetButtonDown("Jump");
        
        if (jumpInput) {
            emittionTime = 1.0f;
        }
        if (emittionTime > 0.0f) {
            tRenderer.emitting = true;
            emittionTime -= Time.deltaTime;
        } else {
            tRenderer.emitting = false;
        }
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        transform.Translate(Vector3.up*1.0f);
    }
}
