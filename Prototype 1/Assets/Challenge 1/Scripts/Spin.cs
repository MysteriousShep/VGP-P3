using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed;
    public float accelerateInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        accelerateInput = Input.GetAxis("Fire1");
        transform.Rotate(Vector3.forward,Time.deltaTime*spinSpeed*(accelerateInput*0.75f+1));
    }
}
