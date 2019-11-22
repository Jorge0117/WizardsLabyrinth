using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyController))]
public class EnemyBat : MonoBehaviour
{
    public int damage = 2;
    //Controlador de 2d
    Controller2D controller;
    
    //Controlador de enemigo
    private EnemyController enemyController;
    
    //Transform del enemigo
    Transform enemyTransform;
    
    //Jugador
    GameObject jugador;

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

    //Probabilidad de cambiar la direccion a la que va aabbeell
    public float ProbaChangeDirection = 0.25f;
    
    //Probabilidad de cambiar la direccion de aabbeell si chawa esta visible
    public float ProbaChangeDirectionIfChawaVisible = 0.1f;
    
    //Velocidad del enemigo
    Vector3 velocity;
    
    //Minimo impulso en X
    public float minImpulseX = 0f;
        
    //Maximo impulso en X
    public float maxImpulseX = 2f;
    
    //Minimo impulso en Y
    public float minImpulseY = 0f;
        
    //Maximo impulso en Y
    public float maxImpulseY = 2f;
    
    //rango de ataque
    public float attackRange = 1f;
    
    //enemigos
    public LayerMask whatIsEnemy;
    
    //Siguiente ataque
    public float nextBasicAttackTime = .5f;
    
    //Tiempo de daño
    public float basicAttackCoolDown = 1f;
    
    //Posicion de ataque
    public Transform attackPos;
    
    //daño basico
    public int basicAttackDamage = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        enemyController = GetComponent<EnemyController> ();
        moveSpeed = enemyController.moveSpeed;
        jugador = GameObject.Find("Chawa");
        enemyTransform = GetComponent<Transform> ();
        left_visible = distanceChawaVisivility / -2;
        right_visible = distanceChawaVisivility / -2;
        frame = 0;
    }

    /*
    // Update is called once per frame
    void Update()
    {
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
            Vector2 impulso = new Vector2(
                Random.Range(maxImpulseX * -1, maxImpulseX), Random.Range(maxImpulseY * -1, maxImpulseY));

            // Proba de que intente cambiar la direccion sin chawa visible
            // Proba de que intente cambiar la direccion con chawa visible
            if ((Random.Range(0f, 1f) < ProbaChangeDirection && !visibleChawa) ||
                (Random.Range(0f, 1f) < ProbaChangeDirectionIfChawaVisible && visibleChawa))
            {
                velocity.x = impulso.x * moveSpeed;
                velocity.y = impulso.y * moveSpeed;
            }
            else
            {
                if (visibleChawa)
                {
                    //Acercarlo en el eje X
                    if (jugador.transform.position.x < enemyTransform.position.x)
                    {
                        //mover hacia la izq
                        impulso.x = Random.Range(maxImpulseX * -1, minImpulseX);
                        velocity.x = impulso.x * moveSpeed;
                    }
                    else
                    {
                        //mover hacia la der
                        impulso.x = Random.Range(minImpulseX, maxImpulseX);
                        velocity.x = impulso.x * moveSpeed;
                    }
                    //Acercarlo en el eje Y
                    if (jugador.transform.position.y < enemyTransform.position.y)
                    {
                        //mover hacia la abajo
                        impulso.y = Random.Range(maxImpulseY * -1, minImpulseY);
                        velocity.y = impulso.y * moveSpeed;
                    }
                    else
                    {
                        //mover hacia la arriba
                        impulso.y = Random.Range(minImpulseY, maxImpulseY);
                        velocity.y = impulso.y * moveSpeed;
                    }
                    //atacar si choca con chawa
                    if (distanciachawaenemigo >= attackRange * -1 && distanciachawaenemigo <= attackRange)
                    {
                        //Ataque
                        if (Time.time >= nextBasicAttackTime)
                        {
                            nextBasicAttackTime = Time.time + basicAttackCoolDown;
                            //Mandar a hacer daño
                            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                            for (int i = 0; i < enemiesToDamage.Length; ++i)
                            {
                                int dir;
                                if (transform.position.x > enemiesToDamage[i].transform.position.x)
                                {
                                    dir = -1;
                                }
                                else
                                {
                                    dir = 1;
                                }
                                enemiesToDamage[i].GetComponent<PlayerController>().takeDamage(basicAttackDamage, dir);
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
            velocity.y = 0;
            //Cuando recibe daño lo hago subir como para evitar que lo sigan atacando
            velocity.y = Random.Range(2 * maxImpulseY, 4 * maxImpulseY);
        }

        //Realiza el movimiento
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
    */

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int dir;
            if (transform.position.x > other.transform.position.x)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            other.gameObject.GetComponent<PlayerController>().takeDamage(damage, dir);
        }
    }
}
