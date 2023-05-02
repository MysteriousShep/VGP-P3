using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    private float powerupStrength = 15;
    private int powerupType = 0;
    public GameObject powerupProjectilePrefab;
    private bool smashing = false;
    private float floorY;
    public float hangTime = 2;
    public float smashSpeed = 5;
    public float explosionForce = 10;
    public float explosionRadius = 3;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
        if (hasPowerup && powerupType == 1)
        {
            Instantiate(powerupProjectilePrefab,transform.position,transform.rotation);
        }
        if (hasPowerup && powerupType == 2 && Input.GetButtonDown("Jump") && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            powerupType = 0;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        else if (other.CompareTag("ProjectilePowerUp"))
        {
            hasPowerup = true;
            powerupType = 1;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        else if (other.CompareTag("SmashPowerUp"))
        {
            hasPowerup = true;
            powerupType = 2;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup && powerupType == 0)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.gameObject.transform.position-transform.position)*powerupStrength,ForceMode.Impulse);
        }
    }
    
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        floorY = transform.position.y;
        float jumpTime = Time.time+hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,smashSpeed);
            yield return null;
        }
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,-smashSpeed*2);
            yield return null;
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,transform.position,explosionRadius,0.0f,ForceMode.Impulse);
            }
        }
        smashing = false;
    }
}
