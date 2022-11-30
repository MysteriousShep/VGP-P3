using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSphere : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,1000f,10000000))
        {
            transform.position = hit.point;
        }
        //transform.position = new Vector3(transform.position.x,player.transform.position.y,transform.position.z);
    }
}
