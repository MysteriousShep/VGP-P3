using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion startRotation = transform.rotation;
        transform.LookAt(pointer.transform);
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile,transform.position,transform.rotation);
        }
        //transform.rotation = startRotation;
    }
}
