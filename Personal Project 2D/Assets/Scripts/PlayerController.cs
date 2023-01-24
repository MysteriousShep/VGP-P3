using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 movement;
    public Animator playerAnimator;
    private float yVelocity;
    private int coyoteFrame = 0;
    private bool grounded = false;
    private int jumpFrame = 0;
    public float jumpSpeed = 10.0f;
    public int jumpDuration = 20;
    public float gravity;
    public int coyoteTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Jump");
    }
    void FixedUpdate()
    {
        Vector3 max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
        Vector3 min = new Vector2(transform.position.x-0.5f,transform.position.y-0.8f);
        Vector2 corner1 = new Vector2(max.x-0.05f,min.y-0.0f+yVelocity);
        Vector2 corner2 = new Vector2(min.x+0.05f,min.y+1.0f+yVelocity);
        if (grounded)
        {
            corner1 = new Vector2(max.x-0.05f,min.y-0.05f);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
        }
        Collider2D hit = Physics2D.OverlapArea(corner1,corner2);

        Debug.DrawLine(new Vector3(corner1.x,corner1.y,0),new Vector3(corner2.x,corner1.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner2.x,corner1.y,0),new Vector3(corner2.x,corner2.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner2.x,corner2.y,0),new Vector3(corner1.x,corner2.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner1.x,corner2.y,0),new Vector3(corner1.x,corner1.y,0),Color.red);

        grounded = false;
        if (hit != null && hit.gameObject != gameObject) {
            grounded = true;
            transform.Translate(new Vector3(0,yVelocity,0));
            corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
            hit = Physics2D.OverlapArea(corner1,corner2);
            for (int i = 0; i < 30; i++)
            {
                if (hit != null && hit.gameObject != gameObject) {
                    transform.Translate(new Vector3(0,0.01f,0));
                    max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
                    min = new Vector2(transform.position.x-0.5f,transform.position.y-0.8f);
                    corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
                    corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
                    hit = Physics2D.OverlapArea(corner1,corner2);
                }
            }
        }
        yVelocity -= gravity*Time.deltaTime;
        if (grounded) {
            jumpFrame = 0;
            coyoteFrame = 0;
            yVelocity = 0;
            if (movement.x != 0)
            {
                playerAnimator.SetTrigger("Run");
                if (movement.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else
            {
                playerAnimator.SetTrigger("Idle");
            }
        }
        if (jumpFrame < jumpDuration && movement.y > 0 && coyoteFrame < coyoteTime) {
            jumpFrame += 1;
            yVelocity = jumpSpeed;
            playerAnimator.SetTrigger("Jump");
            max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
            min = new Vector2(transform.position.x-0.5f,transform.position.y-0.4f);
            corner1 = new Vector2(max.x-0.05f,min.y-0.0f+yVelocity);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f+yVelocity);
            hit = Physics2D.OverlapArea(corner1,corner2);

            Debug.DrawLine(new Vector3(corner1.x,corner1.y,0),new Vector3(corner2.x,corner1.y,0),Color.blue);
            Debug.DrawLine(new Vector3(corner2.x,corner1.y,0),new Vector3(corner2.x,corner2.y,0),Color.blue);
            Debug.DrawLine(new Vector3(corner2.x,corner2.y,0),new Vector3(corner1.x,corner2.y,0),Color.blue);
            Debug.DrawLine(new Vector3(corner1.x,corner2.y,0),new Vector3(corner1.x,corner1.y,0),Color.blue);

            
            if (hit != null && hit.gameObject != gameObject) {
                yVelocity = 0;
                Debug.Log(hit.ToString());
            }
        } else if (jumpFrame < jumpDuration && (jumpFrame > 0 || coyoteFrame >= coyoteTime) && !grounded) {
            jumpFrame += 2;
        }
        else if (!grounded && coyoteFrame < coyoteTime)
        {
            coyoteFrame += 1;
        }
        if (!grounded)
        {
            if (yVelocity < -0.0f)
            {
                playerAnimator.SetTrigger("Fall");
            }
            else if (yVelocity > 0.05f)
            {
                playerAnimator.SetTrigger("Jump");
            }
            else
            {
                playerAnimator.SetTrigger("Hang");
            }
        }
        transform.Translate(new Vector3(movement.x * speed,yVelocity,0));
    }
}
