using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyController))]
public class EnemyAabbeell : MonoBehaviour
{
    //Controlador de 2d
    Controller2D controller;
    
    //Controlador de enemigo
    private EnemyController enemyController;
    
    //Transform del enemigo
    Transform enemyTransform;
    
    //Jugador
    GameObject jugador;
    
    //Gravedad
    float gravity;
    
    //Altura de salto
    public float jumpHeight = 4;
    
    //Tiempo del salto
    public float timeToJumpApex = .4f;

    //Velocidad de salto
    float jumpVelocity;
    
    //Distancia a la que chawa esta visible
    public float distanceChawaVisivility = 8;
    
    //Punto a la izquierda para ver si chawa está visible
    float left_visible;
    
    //Punto a la izquierda para ver si chawa está visible
    float right_visible;
    
    //Contador de frame
    int frame;
    
    //Velocidad de movimiento
    float moveSpeed;

    //Probabilidad de saltar si chawa salta
    public float ProbaJumpIfChawaJumps = 0.1f;
    
    //Probabilidad de saltar
    public float ProbaJump = 0.05f;

    //Probabilidad de cambiar la direccion a la que va aabbeell
    public float ProbaChangeDirection = 0.25f;
    
    //Probabilidad de cambiar la direccion de aabbeell si chawa esta visible
    public float ProbaChangeDirectionIfChawaVisible = 0.1f;
    
    
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        enemyController = GetComponent<EnemyController> ();
        moveSpeed = enemyController.moveSpeed;
        jugador = GameObject.Find("Chawa");
        enemyTransform = GetComponent<Transform> ();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        left_visible = distanceChawaVisivility / -2;
        right_visible = distanceChawaVisivility / -2;
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Velocidad del enemigo
        Vector3 velocity = new Vector3(0,0,1);
        
        //Mover el enemigo cuando esta recibiendo daño, para simular que retrocede
        if (!enemyController.isTakingDamage)
        {
            bool visibleChawa = false;
            float distanciachawaenemigo = jugador.transform.position.x - enemyTransform.position.x;

            //Vemos si chawa esta visible
            if (left_visible <= distanciachawaenemigo && right_visible <= distanciachawaenemigo)
            {
                visibleChawa = true;
            }
            //Se quita velocidad y en caso de haber choque arriba o abajo
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0.0f;
            }
            //Moverse random
            Vector2 impulso = new Vector2(Random.Range(-1.0f, 1f), 0.0f);

            // proba que salte cuando el jugador presiona saltar
            // proba que salte por su cuenta
            if ((Input.GetButtonDown("Jump") && controller.collisions.below &&
                 Random.Range(0f, 1f) < ProbaJumpIfChawaJumps) ||
                (controller.collisions.below && Random.Range(0f, 1f) < ProbaJump))
            {
                velocity.y = jumpVelocity;
                Debug.Log("Salte");
            }

            // Proba de que intente cambiar la direccion sin chawa visible
            // Proba de que intente cambiar la direccion con chawa visible
            if ((Random.Range(0f, 1f) < ProbaChangeDirection && !visibleChawa) ||
                (Random.Range(0f, 1f) < ProbaChangeDirectionIfChawaVisible && visibleChawa))
            {
                velocity.x = impulso.x * moveSpeed;
            }
            else
            {
                if (visibleChawa)
                {
                    if (jugador.transform.position.x < enemyTransform.position.x)
                    {
                        //mover hacia la izq
                        impulso.x = Random.Range(-1.0f, 0f);
                        velocity.x = impulso.x * moveSpeed;
                    }
                    else
                    {
                        //mover hacia la der
                        impulso.x = Random.Range(0f, 1f);
                        velocity.x = impulso.x * moveSpeed;
                    }
                }
            }
        }

        //Realiza el movimiento
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //Para girar el dibujo cada 10 frames
        if ( frame >= 10)
        {
            frame = 0;
            //girar el dibujo, SOLO  cuando no esta recibiendo daño
            if ( !enemyController.isTakingDamage && (velocity.x < 0 && enemyTransform.localScale.x < 0 || velocity.x > 0 && enemyTransform.localScale.x > 0) ){
                enemyTransform.localScale = new Vector3(enemyTransform.localScale.x * -1, enemyTransform.localScale.y, 1);                
            }
        }


        frame++;
    }
}
