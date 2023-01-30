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
    public int accelleration = 3;
    public int deccelleration = 3;
    private float xVelocity = 0;
    private float xSpeed = 0;
    private float sit = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Jump");
        sit = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        Vector3 max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
        Vector3 min = new Vector2(transform.position.x-0.5f,transform.position.y-0.8f);
        Vector2 corner1 = new Vector2(max.x-0.05f,min.y-0.0f+yVelocity);
        Vector2 corner2 = new Vector2(min.x+0.05f,min.y+1.0f+yVelocity);
        Collider2D hit = Physics2D.OverlapArea(corner1,corner2);

        

        grounded = false;
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
            hit = Physics2D.OverlapArea(corner1,corner2);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0,0.01f*Mathf.Sign(yVelocity),0));
                    max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
                    min = new Vector2(transform.position.x-0.5f,transform.position.y-0.8f);
                    corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
                    corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
                    hit = Physics2D.OverlapArea(corner1,corner2);
                }
            }
            yVelocity = 0;
            max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
            min = new Vector2(transform.position.x-0.5f,transform.position.y-0.9f);
            corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
            hit = Physics2D.OverlapArea(corner1,corner2);
            if (hit != null && hit.gameObject != gameObject)
            {
                grounded = true;
            }
        }
        Debug.DrawLine(new Vector3(corner1.x,corner1.y,0),new Vector3(corner2.x,corner1.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner2.x,corner1.y,0),new Vector3(corner2.x,corner2.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner2.x,corner2.y,0),new Vector3(corner1.x,corner2.y,0),Color.red);
        Debug.DrawLine(new Vector3(corner1.x,corner2.y,0),new Vector3(corner1.x,corner1.y,0),Color.red);
        yVelocity -= gravity*Time.deltaTime;
        if (grounded) 
        {
            jumpFrame = 0;
            coyoteFrame = 0;
            yVelocity = 0;
            if (sit != -1)
            {
                if (xSpeed != 0)
                {
                    playerAnimator.SetTrigger("Run");
                    
                }
                else
                {
                    playerAnimator.SetTrigger("Idle");
                }
            }
            else
            {
                playerAnimator.SetTrigger("Sit");
            }
        }
        else
        {
            if (yVelocity < -0.05f)
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
        if (jumpFrame < jumpDuration && movement.y > 0 && coyoteFrame < coyoteTime) 
        {
            jumpFrame += 1;
            yVelocity = jumpSpeed;
            playerAnimator.SetTrigger("Jump");
            grounded = false;
        } 
        else if (jumpFrame < jumpDuration && (jumpFrame > 0 || coyoteFrame >= coyoteTime) && !grounded) 
        {
            jumpFrame += 2;
        }
        else if (!grounded && coyoteFrame < coyoteTime)
        {
            coyoteFrame += 1;
        }
        transform.Translate(new Vector3(0,yVelocity,0));
        if (movement.x != 0)
        {
            if (movement.x < 0)
            {
                if (xSpeed > 0)
                {
                    xSpeed -= speed/accelleration;
                }
                if (xSpeed > -speed)
                {
                    xSpeed -= speed/accelleration;
                }
            }
            else
            {
                if (xSpeed < 0)
                {
                    xSpeed += speed/accelleration;
                }
                if (xSpeed < speed)
                {
                    xSpeed += speed/accelleration;
                }
            }
        }
        else
        {
            if (xSpeed < 0)
            {
                xSpeed += speed/deccelleration;
                if (xSpeed > 0)
                {
                    xSpeed = 0;
                }
            }
            if (xSpeed > 0)
            {
                xSpeed -= speed/deccelleration;
                if (xSpeed < 0)
                {
                    xSpeed = 0;
                }
            }
        }
        if (sit <= -1 && grounded)
        {
            xSpeed *= 0.75f;
            if (Mathf.Abs(xSpeed) < speed/deccelleration)
            {
                xSpeed = 0;
            }
        }
        if (xSpeed < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xSpeed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        playerAnimator.SetFloat("xSpeed",Mathf.Abs(xSpeed/speed));
        xVelocity = xSpeed;
        max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
        min = new Vector2(transform.position.x-0.5f,transform.position.y-0.7f);
        corner1 = new Vector2(max.x-0.05f+xSpeed,min.y-0.0f);
        corner2 = new Vector2(min.x+0.05f+xSpeed,min.y+1.0f);
        hit = Physics2D.OverlapArea(corner1,corner2);
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
            corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
            hit = Physics2D.OverlapArea(corner1,corner2);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0.01f*Mathf.Sign(xVelocity),0,0));
                    max = new Vector2(transform.position.x+0.5f,transform.position.y+0.5f);
                    min = new Vector2(transform.position.x-0.5f,transform.position.y-0.7f);
                    corner1 = new Vector2(max.x-0.05f,min.y-0.0f);
                    corner2 = new Vector2(min.x+0.05f,min.y+1.0f);
                    hit = Physics2D.OverlapArea(corner1,corner2);
                }
            }
            transform.Translate(new Vector3(-0.01f*Mathf.Sign(xVelocity),0,0));
            xVelocity = 0;
            xSpeed = 0;
        }
        transform.Translate(new Vector3(xVelocity,0,0));
    }
}
