using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    public float bounds = 10;
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if ((transform.position.x < -bounds || transform.position.z < -bounds) || (transform.position.x > bounds || transform.position.z > bounds))
        {
            Destroy(gameObject);
        }
    }
}
