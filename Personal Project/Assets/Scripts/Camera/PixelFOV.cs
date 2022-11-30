using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelFOV : MonoBehaviour
{
    public GameObject sprite;
    void Update()
    {
        transform.LookAt(sprite.transform);
        GetComponent<Camera>().fieldOfView = Mathf.Atan2(1f,(transform.position - sprite.transform.position).magnitude)*2f*Mathf.Rad2Deg;
    }
}
