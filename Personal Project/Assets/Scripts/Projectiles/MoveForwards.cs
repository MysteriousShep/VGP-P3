using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 10;
    public float bounds = 100;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<DeleteOutOfBounds>() != null)
        {
            bounds = GetComponent<DeleteOutOfBounds>().bounds;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!((transform.position.x < -bounds || transform.position.z < -bounds) || (transform.position.x > bounds || transform.position.z > bounds)))
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
    }
}
