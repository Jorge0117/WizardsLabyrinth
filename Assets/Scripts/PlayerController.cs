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
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;

    Controller2D controller;
    SpriteRenderer spriteRenderer;
    Animator animator;

    GameObject chawa;
    float chawaXScale;
    float chawaYScale;

    Vector2 positionBeforeJump;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpHeight = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        chawa = GameObject.Find("Chawa");
        chawaXScale = chawa.transform.localScale.x;
        chawaYScale = chawa.transform.localScale.y;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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
}
