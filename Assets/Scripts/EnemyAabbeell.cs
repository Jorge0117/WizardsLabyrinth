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
    public float distanceChawaVisivility = 20;
    
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
    
    //Velocidad del enemigo
    Vector3 velocity;
    
    //Siguiente ataque
    private float nextBasicAttackTime;
    
    //Tiempo de daño
    public float basicAttackCoolDown = 1.5f;
    
    //proba de ataque basico
    public float probaBasicAttack = .9f;
    
    //daño basico
    public int basicAttackDamage = 1;

    //Para las animaciones
    private Animator enemyAnimator;
    
    //animacion de ataque
    private static readonly int Attack = Animator.StringToHash("Attack");

    //Controlador del jugador(chawa)
    private PlayerController jugadorPlayerController;
    
    //Posicion de ataque
    public Transform attackPos;
    
    //rango de ataque
    public float attackRange = 1.4f;
    
    //enemigos
    public LayerMask whatIsEnemy;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        enemyController = GetComponent<EnemyController> ();
        moveSpeed = enemyController.moveSpeed;
        jugador = GameObject.Find("Chawa");
        jugadorPlayerController = jugador.GetComponent<PlayerController>();
        enemyTransform = GetComponent<Transform> ();
        enemyAnimator = GetComponent<Animator>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        left_visible = distanceChawaVisivility / -2;
        right_visible = distanceChawaVisivility / -2;
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando no esta recibiendo daño, 
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
                    //atacar si esta chawa al alcance
                    if (distanciachawaenemigo >= (attackRange + .1f) * -1 && distanciachawaenemigo <= attackRange + .1f)
                    {
                        //Ataque
                        if ( Random.Range(0f, 1f) < probaBasicAttack && Time.time >= nextBasicAttackTime)
                        {
                            nextBasicAttackTime = Time.time + basicAttackCoolDown;
                            enemyAnimator.SetTrigger(Attack);
                            //Mandar a hacer daño
                            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                            for (int i = 0; i < enemiesToDamage.Length; ++i)
                            {
                                enemiesToDamage[i].GetComponent<PlayerController>().takeDamage(basicAttackDamage);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //Este caso se pone para que no agarre la velocidad x que tenia antes,
            //porque entonces puede hacer que aabbeell no retroceda lo suficiente
            velocity.x = 0;
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
