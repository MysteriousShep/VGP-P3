using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30,0,0);
    public float startDelay = 2;
    public float repeatRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle",startDelay,repeatRate);
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab,spawnPos,transform.rotation);
    }
}
