using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 movement;
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
    private Vector3 grapplePoint;
    public float grappleLength = 2.5f;
    private float maxGrappleLength = 2.5f;
    private bool grappling = false;
    public GameObject grappleLine;
    public float minGrappleLength = 3.0f;
    private bool grappleShrink = false;
    public int jumps = 2;
    private int maxJumps = 2;
    private float gravityMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        maxGrappleLength = grappleLength;
        maxJumps = jumps;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Jump");
        sit = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Fire1") || (Input.GetButton("Fire1") && !grappling))
        {
            grappling = true;
            grappleShrink = false;
            grappleLength = maxGrappleLength;
            grapplePoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
            grapplePoint = ((grapplePoint-transform.position).normalized*maxGrappleLength);
            LayerMask filter = LayerMask.GetMask("Grappleable");
            RaycastHit2D grappleTarget = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),grapplePoint,maxGrappleLength,filter);
            if (grappleTarget.collider != null && grappleTarget.point.y > transform.position.y+2)
            {
                grapplePoint = grappleTarget.point;
                grappleLength = grappleTarget.distance;
            }
            else
            {
                grappling = false;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            grappling = false;
            grappleLine.SetActive(false);
        }
        if (Input.GetButton("Fire2"))
        {
            grappleShrink = true;
        }
    }
    void FixedUpdate()
    {
        
        if (grappling)
        {
            
            grappleLine.SetActive(true);
            Grapple(grapplePoint.x,grapplePoint.y,grappleLength);
            if ((grappleLength > minGrappleLength && grappleShrink)) 
            {
                grappleLength -= 0.35f;
                if (grappleLength <= 0.5f)
                {
                    grappling = false;
                }
            }
            else if (grappleShrink)
            {
                grappleLength = minGrappleLength;
            }
            
            if (GetHitBoxAtPosition(transform.position.x+xVelocity+xSpeed,transform.position.y+yVelocity,0.9f,1f))
            {
                grappleLength -= 0.35f;
                if (grappleLength < minGrappleLength)
                {
                    grappleLength = minGrappleLength;
                }
            }
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles*0.9f);
        }

        if (sit <= -1 && !grounded)
        {
            gravityMultiplier = 2;
        }
        else
        {
            gravityMultiplier = 1;
        }

        if (jumpFrame < jumpDuration && movement.y > 0 && coyoteFrame < coyoteTime) 
        {
            jumpFrame += 1;
            coyoteFrame = 0;
            yVelocity = jumpSpeed;
            playerAnimator.SetTrigger("Jump");
            grounded = false;
        } 
        else if ((jumpFrame > 0 || coyoteFrame >= coyoteTime) && !grounded && jumpFrame < jumpDuration+5) 
        {
            jumpFrame += 99;
        }
        else if (jumpFrame < jumpDuration+5 && coyoteFrame >= coyoteTime)
        {
            jumpFrame += 1;
        }
        else if (jumpFrame >= jumpDuration+5 && movement.y > 0 && coyoteFrame >= coyoteTime && !grounded && jumps > 0 && !grappling)
        {
            jumpFrame = jumpDuration/2;
            coyoteFrame = 0;
            jumps -= 1;
            yVelocity = jumpSpeed*2f;
            playerAnimator.SetTrigger("Jump");
        }
        if (!grounded && coyoteFrame < coyoteTime)
        {
            coyoteFrame += 1;
        } 
        yVelocity -= gravity*gravityMultiplier;

        grounded = false;
        if (GetHitBoxAtPosition(transform.position.x,transform.position.y+yVelocity,0.8f)) 
        {
            
            
            GetHitBoxAtPosition(transform.position.x,transform.position.y,0.8f);
            for (int i = 0; i < 30; i++)
            {
                if (!GetHitBoxAtPosition(transform.position.x,transform.position.y,0.8f)) 
                {
                    transform.Translate(new Vector3(0,0.01f*Mathf.Sign(yVelocity),0),Space.World);
                    
                }
            }
            yVelocity = 0;
            if (GetHitBoxAtPosition(transform.position.x,transform.position.y,0.9f,0.1f))
            {
                grounded = true;
            }
        }
        
        
        if (grounded) 
        {
            jumpFrame = 0;
            jumps = maxJumps;
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
        
        transform.Translate(new Vector3(0,yVelocity,0),Space.World);
        
        xVelocity -= xSpeed;
        
        if (movement.x != 0 && !(sit <= -1 && grounded))
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
        
        if (xSpeed < 0 && !grappling)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xSpeed > 0 && !grappling)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        playerAnimator.SetFloat("xSpeed",Mathf.Abs(xSpeed/speed));
        
        xVelocity += xSpeed;
        
        if (GetHitBoxAtPosition(transform.position.x+xVelocity,transform.position.y,0.7f,0.9f)) 
        {
            
            
            
            for (int i = 0; i < 30; i++)
            {
                if (!GetHitBoxAtPosition(transform.position.x,transform.position.y,0.7f,0.9f)) 
                {
                    transform.Translate(new Vector3(0.01f*Mathf.Sign(xVelocity),0,0),Space.World);
                    
                    
                }
            }
            transform.Translate(new Vector3(-0.01f*Mathf.Sign(xVelocity),0,0));
            xVelocity = 0;
            xSpeed = 0;
        }
        transform.Translate(new Vector3(xVelocity,0,0),Space.World);
    }

    bool GetHitBoxAtPosition(float x, float y,float minHeight = 0.7f, float maxHeight = 1.0f)
    {
        Vector2 max = new Vector2(x+0.45f,y+maxHeight);
        Vector2 min = new Vector2(x-0.45f,y-minHeight);
        Collider2D[] hit = Physics2D.OverlapAreaAll(max,min);
        Debug.DrawLine(new Vector3(max.x,max.y,0),new Vector3(min.x,max.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,max.y,0),new Vector3(min.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,min.y,0),new Vector3(max.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(max.x,min.y,0),new Vector3(max.x,max.y,0),Color.red);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.CompareTag("Ground") || hit[i].gameObject.CompareTag("Grappleable"))
            {
                return true;
            }
        }
        

        return false;
    }

    void LaunchTowards(float x, float y, float speed = 0.0f)
    {
        Vector3 targetPosition = new Vector3(x,y,transform.position.z);
        Vector3 newVelocity = (targetPosition - transform.position).normalized * speed;
        xVelocity = newVelocity.x;
        yVelocity = newVelocity.y;
    }

    void Grapple(float x, float y, float tetherLength = 1.0f)
    {
        Vector3 oldPos = transform.position;
        Vector3 nextPos = transform.position-new Vector3(xVelocity,yVelocity,0);
        Debug.DrawLine(new Vector3(x,y,0),transform.position,Color.white);
        if (Vector3.Distance(nextPos,new Vector3(x,y,nextPos.z)) > tetherLength)
        {
            xVelocity = (nextPos.x-oldPos.x);
            yVelocity = (nextPos.y-oldPos.y);
            nextPos = transform.position-new Vector3(xVelocity,yVelocity,0);
            nextPos = new Vector3(x,y,transform.position.z)+((nextPos-new Vector3(x,y,transform.position.z)).normalized*tetherLength);
            
            xVelocity = (nextPos.x-oldPos.x);
            yVelocity = (nextPos.y-oldPos.y);
        }
        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(transform.position.x,transform.position.y,transform.position.z+0.1f);
        points[1] = new Vector3(grapplePoint.x,grapplePoint.y,transform.position.z+0.1f);
        grappleLine.GetComponent<LineRenderer>().SetPositions(points);
        
        float angle = Vector2.SignedAngle(Vector2.up, new Vector3(x,y,transform.position.z) - transform.position);

        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(targetRotation);
    }

}
