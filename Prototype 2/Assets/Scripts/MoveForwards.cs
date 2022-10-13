using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float maxSpeed = 40.0f;
    public float minSpeed = 40.0f;
    public float speed = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed,maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
