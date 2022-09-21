using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    private Vector3 offset = new Vector3(0,3,-7);
    public float accelerateInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        accelerateInput = Input.GetAxis("Fire1");
        transform.Rotate(Vector3.up,(transform.rotation.y+180-(plane.transform.rotation.y+180))*-2);
        transform.position = plane.transform.position;
        Camera.main.fieldOfView = 60.0f+(accelerateInput*20.0f);
        transform.Translate(Vector3.forward*-9.0f);
        transform.Translate(Vector3.up*2.0f);
    }
}
