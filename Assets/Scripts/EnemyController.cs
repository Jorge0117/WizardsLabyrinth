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

    public float distanciaVisivilidadChawa = 8;

    float izq_visible;
    float der_visible;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;

    Controller2D controller;

    GameObject jugador;

    Transform enemigoTransform;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        jugador = GameObject.Find("Chawa");

        enemigoTransform = GetComponent<Transform> ();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        izq_visible = distanciaVisivilidadChawa / -2;
        der_visible = distanciaVisivilidadChawa / -2;
    }

    // Update is called once per frame
    void Update()
    {
        bool visibleChawa = false;
        float distanciachawaenemigo = jugador.transform.position.x - enemigoTransform.position.x;


        if( izq_visible <=  distanciachawaenemigo && der_visible <= distanciachawaenemigo){
            visibleChawa = true;
            Debug.Log("Lo estoy viendo");
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0.0f;
        }

        //Moverse random
        Vector2 impulso = new Vector2(Random.Range(-1.0f, 1f), 0.0f);
        float anterior = this.x_impulso;
        this.x_impulso = impulso.x;
        this.y_impulso = impulso.y;

        // 10% de proba que salte cuando el jugador presiona saltar, 10% de que salte por su cuenta
        if ( (Input.GetButtonDown("Jump") && controller.collisions.below && Random.Range(0f, 1f) < 0.10f) ||
             (controller.collisions.below && Random.Range(0f, 1f) < 0.10f) )
        {
            velocity.y = jumpVelocity;
        }

        // 25% de proba que intente cambiar la direccion para donde va
        if(Random.Range(0f, 1f) > 0.75f && !visibleChawa){
	        velocity.x = impulso.x * moveSpeed;
	        velocity.y += gravity * Time.deltaTime;
	        controller.Move(velocity * Time.deltaTime);
	    }else{
	        if(visibleChawa){
	            if( jugador.transform.position.x < enemigoTransform.position.x ){
	                //mover hacia la izq
	                impulso.x = Random.Range(-1.0f, 0f);
	                velocity.x = impulso.x * moveSpeed;
	                this.x_impulso = impulso.x;
	            }else{
	                //mover hacia la der
	                impulso.x = Random.Range(0f, 1f);
                    velocity.x = impulso.x * moveSpeed;
                    this.x_impulso = impulso.x;
	            }
	        }
	    	velocity.y += gravity * Time.deltaTime;
	        controller.Move(velocity * Time.deltaTime);
	    }

    }
}
