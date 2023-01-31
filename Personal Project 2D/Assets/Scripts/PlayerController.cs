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
    private Collider2D hit;

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
        GetHitBoxAtPosition(transform.position.x,transform.position.y+yVelocity,0.8f);

        

        grounded = false;
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            GetHitBoxAtPosition(transform.position.x,transform.position.y,0.8f);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0,0.01f*Mathf.Sign(yVelocity),0),Space.World);
                    GetHitBoxAtPosition(transform.position.x,transform.position.y,0.8f);
                }
            }
            yVelocity = 0;
            GetHitBoxAtPosition(transform.position.x,transform.position.y,0.9f);
            if (hit != null && hit.gameObject != gameObject)
            {
                grounded = true;
            }
        }
        
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
        transform.Translate(new Vector3(0,yVelocity,0),Space.World);
        xVelocity -= xSpeed;
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
        if (xVelocity < 0)
        {
            xVelocity += speed/deccelleration;
            if (xVelocity > 0)
            {
                xVelocity = 0;
            }
        }
        if (xVelocity > 0)
        {
            xVelocity -= speed/deccelleration;
            if (xVelocity < 0)
            {
                xVelocity = 0;
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
        xVelocity += xSpeed;
        GetHitBoxAtPosition(transform.position.x+xVelocity,transform.position.y);
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            GetHitBoxAtPosition(transform.position.x,transform.position.y);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0.01f*Mathf.Sign(xVelocity),0,0));
                    GetHitBoxAtPosition(transform.position.x,transform.position.y);
                    
                }
            }
            transform.Translate(new Vector3(-0.01f*Mathf.Sign(xVelocity),0,0));
            xVelocity = 0;
            xSpeed = 0;
        }
        transform.Translate(new Vector3(xVelocity,0,0));
        if (Input.GetButton("Fire1"))
        {
            LaunchTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0.5f);
        }
        
    }

    void GetHitBoxAtPosition(float x, float y,float height = 0.7f)
    {
        Vector2 max = new Vector2(x+0.45f,y+1f);
        Vector2 min = new Vector2(x-0.45f,y-height);
        hit = Physics2D.OverlapArea(max,min);
        Debug.DrawLine(new Vector3(max.x,max.y,0),new Vector3(min.x,max.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,max.y,0),new Vector3(min.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,min.y,0),new Vector3(max.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(max.x,min.y,0),new Vector3(max.x,max.y,0),Color.red);
    }

    void LaunchTowards(float x, float y, float speed = 0.0f)
    {
        Vector3 targetPosition = new Vector3(x,y,transform.position.z);
        Vector3 newVelocity = (targetPosition - transform.position).normalized * speed;
        xVelocity = newVelocity.x;
        yVelocity = newVelocity.y;
    }

    void Grapple(float x, float y, float speed = 1.0f)
    {
        Vector3 prevPosition = transform.position;
        Vector3 targetPosition = new Vector3(x,y,transform.position.z);
        float angle = Vector2.SignedAngle(Vector2.up, targetPosition - transform.position);

        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(targetRotation);
        transform.Translate(transform.up*speed*Time.deltaTime);
        yVelocity = 0;
        
    }

}
