using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private float accelerateInput;
    // public TrailRenderer tRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // tRenderer = GetComponent<TrailRenderer>();
        // tRenderer.emitting = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        accelerateInput = Input.GetAxis("Fire1");
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        /*
        if (accelerateInput > 0.0f) {
            tRenderer.emitting = true;
        } else {
            tRenderer.emitting = false;
        }
        */
        transform.Translate(Vector3.forward*-0.3f);
        transform.Translate(Vector3.right*4.7f*offset);
        transform.Translate(Vector3.up*1.75f);
    }
}
