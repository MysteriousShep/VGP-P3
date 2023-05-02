using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90,Random.Range(0,360),90));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(transform.forward*speed*Time.fixedDeltaTime);
        if (Vector3.Distance(new Vector3(0,0,0),transform.position) > 30)
        {
            Destroy(gameObject);
        }
    }
}
