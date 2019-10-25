using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;

    public float moveSpeed = 6;
    public float dashSpeed = 2;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;

    Controller2D controller;
    Animator animator;

    GameObject chawa;
    float chawaXScale;
    float chawaYScale;

    Vector2 positionBeforeJump;
    
    int maxHealth = 5;
    int currentHealth;

    [HideInInspector]
    public bool isDashing = false;

    private float dashAngle;

    public float dashDuration = 0.5f;

    private float stopDashTime;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        chawa = GameObject.Find("Chawa");
        chawaXScale = chawa.transform.localScale.x;
        chawaYScale = chawa.transform.localScale.y;

        animator = GetComponent<Animator>();
        currentHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!animator.GetBool("isDashing"))
        {
            if(controller.collisions.below)
            {
                animator.SetBool("isFalling", false);
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y = maxJumpVelocity;
                }
            }
            else
            {
                if (!animator.GetBool("isFalling"))
                {
                    animator.SetBool("isFalling", true);
                    positionBeforeJump = chawa.transform.position;
                }
            
            }
        
            if(Input.GetButtonUp("Jump"))
            {
                if(velocity.y > minJumpVelocity)
                    velocity.y = minJumpVelocity;
            }

            velocity.x = input.x * moveSpeed;
            velocity.y += gravity * Time.deltaTime;
            if(velocity.x > 0)
            {
                chawa.transform.localScale = new Vector3(chawaXScale, chawaYScale, 1);
                animator.SetBool("isWalking", true);
            }
            else if(velocity.x < 0)
            {
                chawa.transform.localScale = new Vector3(-chawaXScale, chawaYScale, 1);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            controller.Move(velocity * Time.deltaTime, input);

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
        }
        else
        {
            if (!isDashing)
            {
                isDashing = true;
                if (controller.collisions.below)
                {
                    dashAngle = 0;
                }

                if (input.x != 0 && input.y > 0)
                {
                    dashAngle = 45;
                    //gameObject.transform.rotation = Quaternion.Euler(0,0,45);
                }
                else if (input.x != 0 && input.y < 0)
                {
                    dashAngle = -45;
                }
                else if (input.x == 0 && input.y > 0)
                {
                    dashAngle = 90;
                }
                else if (input.x == 0 && input.y < 0)
                {
                    dashAngle = 270;
                }
                else if (input.x != 0 && input.y == 0)
                {
                    dashAngle = 0;
                }

                stopDashTime = Time.time + dashDuration;
            }
            velocity.x = Mathf.Cos(Mathf.PI * dashAngle / 180) * dashSpeed * Mathf.Sign(gameObject.transform.localScale.x);
            velocity.y = Mathf.Sin(Mathf.PI * dashAngle / 180) * dashSpeed;
            controller.Move(velocity);

            if (Time.time > stopDashTime)
            {
                isDashing = false;
                animator.SetBool("isDashing", false);
                gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    public void returnToGround()
    {
        gameObject.transform.position = positionBeforeJump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "1")
        {
            transform.parent = collision.transform;
            Debug.Log("Llama al metodo");
        }
        Debug.Log("Llama al metodo");
    }
}
