using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        transform.Translate(Vector3.forward*-Time.deltaTime);
    }
}
