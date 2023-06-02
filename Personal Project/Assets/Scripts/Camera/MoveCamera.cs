using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 startRotation;
    public float rotateSpeed = 35;
    public float dragXSpeed = 35;
    public float dragYSpeed = 59;
    public float zoomSpeed = 10;
    public GameObject pixelObject;
    private Camera pixelCamera;
    public GameObject playerObject;
    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (pixelObject != null) {
            pixelCamera = pixelObject.GetComponent<Camera>();
        }
        if (playerObject != null) {
            playerCamera = playerObject.GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetButton("Fire3"))
        {
            
            
            Quaternion prevRotation = transform.rotation;
            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.x = 0;
            Vector3 newPosition = Camera.main.ScreenToViewportPoint(startPos - Input.mousePosition);
            //transform.rotation = Quaternion.LookRotation(newRotation);
            transform.Rotate(Vector3.left,20,Space.Self);
            
            transform.Translate(new Vector3(newPosition.x*dragXSpeed*Camera.main.orthographicSize/10,0,newPosition.y*(dragYSpeed)*Camera.main.orthographicSize/10));
            transform.rotation = prevRotation;
            startPos = Input.mousePosition;
            

        }
        if (Input.GetButtonDown("Fire2"))
        {
            startRotation = Input.mousePosition;
        }
        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(new Vector3(0,Camera.main.ScreenToViewportPoint(startRotation-Input.mousePosition).x*rotateSpeed,0),Space.World);
            startRotation = Input.mousePosition;
        }
        Camera.main.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel")*zoomSpeed;
        if (pixelObject != null) {
            pixelCamera.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel")*zoomSpeed;
        }
        if (playerObject != null) {
            playerCamera.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel")*zoomSpeed;
        }
    }
}
