using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 10;
    public float bounds = 100;
    public ParticleSystem destroyFx;
    private bool playedParticles = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<DeleteOutOfBounds>() != null)
        {
            bounds = GetComponent<DeleteOutOfBounds>().bounds;
        }
        transform.position = new Vector3(transform.position.x,transform.position.y+2.0f,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!((transform.position.x < -bounds || transform.position.z < -bounds) || (transform.position.x > bounds || transform.position.z > bounds)) && (transform.position.y > -0.5f))
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
        else
        {
            if (!playedParticles)
            {
                destroyFx.Play();
                GetComponent<MeshRenderer>().enabled = false;
                playedParticles = true;
            }
        }
    }
}
