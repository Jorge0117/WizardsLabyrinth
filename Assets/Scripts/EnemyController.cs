using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class EnemyController : MonoBehaviour
{
    //Controlador de 2d
    Controller2D controller;
    
    //Vida inicial de enemigo
    public int maxHealth = 6;
    
    //Vida del enemigo
    private int currentHealth;

    //Se esta recibiendo daño?
    public bool isTakingDamage = false;
    
    //Tiempo en que no se recibe mas daño
    public int invencibilitySeconds = 1;
    
    //Velocidad de retroceso al recibir daño
    public float recoilVelocity = 0.9f;
    
    //Transform del enemigo
    Transform enemyTransform;
    
    //Jugador
    GameObject jugador;
    
    //Velocidad de movimiento
    public float moveSpeed = 6;


    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        jugador = GameObject.Find("Chawa");
        enemyTransform = GetComponent<Transform> ();
        
        currentHealth = maxHealth;
    }

    void Update()
    {
        //Velocidad del enemigo

        //Mover el enemigo cuando esta recibiendo daño, para simular que retrocede
        if (isTakingDamage)
        {
            velocity.x = (jugador.transform.position.x < enemyTransform.position.x
                             ? recoilVelocity
                             : recoilVelocity * -1) * moveSpeed;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void takeDamage(int damage)
    {
        if(!isTakingDamage)
        {
            isTakingDamage = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(wait(invencibilitySeconds));
        }
    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTakingDamage = false;
    }
}
