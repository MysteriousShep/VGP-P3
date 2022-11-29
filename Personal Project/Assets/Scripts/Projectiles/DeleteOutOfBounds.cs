using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOutOfBounds : MonoBehaviour
{
    public float bounds = 10;
    void Update()
    {
        if ((transform.position.x < -bounds || transform.position.z < -bounds) || (transform.position.x > bounds || transform.position.z > bounds) || (transform.position.y <= -0.5f))
        {
            Destroy(gameObject,0.25f);
        }
    }
}
