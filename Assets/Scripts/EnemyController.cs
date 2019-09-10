using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class EnemyController : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    public float moveSpeed = 6;

    public float x_impulso = 0;

    public float y_impulso = 0;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;

    Controller2D controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0.0f;
        }

        //Moverse random
        Vector2 impulso = new Vector2(Random.Range(-1.0f, 1f), 0.0f);
        this.x_impulso = impulso.x;
        this.y_impulso = impulso.y;

        // 25% de proba que intente cambiar la direccion para donde va
        if(Random.Range(0f, 1f) > 0.75f){
	        // 25% de proba que salte cuando el jugador presiona saltar, 10% de que salte por su cuenta
	        if ( (Input.GetButtonDown("Jump") && controller.collisions.below && Random.Range(0f, 1f) < 0.10f) ||
	        	 (controller.collisions.below && Random.Range(0f, 1f) < 0.10f) )
	        {
	            velocity.y = jumpVelocity;
	        }
	        velocity.x = impulso.x * moveSpeed;
	        velocity.y += gravity * Time.deltaTime;
	        controller.Move(velocity * Time.deltaTime);
	    }else{
	    	velocity.y += gravity * Time.deltaTime;
	        controller.Move(velocity * Time.deltaTime);
	    }

    }
}
