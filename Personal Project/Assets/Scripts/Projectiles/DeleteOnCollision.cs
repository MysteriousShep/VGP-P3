using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnCollision : MonoBehaviour
{
    
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
    
}
