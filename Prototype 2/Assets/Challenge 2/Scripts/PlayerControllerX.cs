using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public GameObject[] balls;
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int ballIndex = Random.Range(0,balls.Length)
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
