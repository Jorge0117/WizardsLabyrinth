using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class EnemyController : MonoBehaviour
{
    //Controlador de 2d
    Controller2D controller;
    
    //Vida inicial de enemigo
    public int maxHealth = 5;
    
    //Vida del enemigo
    public int currentHealth;

    //Se esta recibiendo daño?
    public bool isTakingDamage = false;
    
    //Tiempo en que no se recibe mas daño
    public float invencibilitySeconds = 0.5f;
    
    //Velocidad de retroceso al recibir daño
    public float recoilVelocity = 0.9f;
    
    //Transform del enemigo
    Transform enemyTransform;
    
    //Jugador
    GameObject jugador;
    
    //Velocidad de movimiento
    public float moveSpeed = 6;

    public int dropChance = 15;
    
    public Vector3 velocity;

    public GameObject drop;
    
    //tag para saber que es aabbeell, si es otro solo dice enemy
    public string enemyName = "Enemy";

    //sonidos
    private SFXController sfx;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        jugador = GameObject.Find("Chawa");
        enemyTransform = GetComponent<Transform> ();
        
        currentHealth = maxHealth;

        sfx = GameObject.Find("SFX Controller").GetComponent<SFXController>();
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
            velocity.y = 13;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void takeDamage(int damage)
    {
        if(!isTakingDamage)
        {
            isTakingDamage = true;
            sfx.PlayEnemyTakeDamage(gameObject.transform.position);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber < 15)
                {
                    Instantiate(drop, transform.position, Quaternion.identity);
                }
                sfx.PlayEnemyDie(gameObject.transform.position);
                Destroy(gameObject);
            }
            StartCoroutine(wait(invencibilitySeconds));
        }
    }
    
    //typeShield:
    //             1: fire
    //             2: ice
    //             3: wind
    public void takeDamage(int damage, int typeShield)
    {
        Enemy_Aabbeell controllerAabbell = gameObject.GetComponent<Enemy_Aabbeell>();
        int shield = controllerAabbell.shield;
        //Debug.Log("TAKEDAMAGE AABBEELL");
        //Debug.Log(shield);
        //Debug.Log(typeShield);
        if( shield == typeShield && !isTakingDamage)
        {
            //Debug.Log("TAKEDAMAGE AABBEELL");
            isTakingDamage = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("Win Scene");
                sfx.PlayEnemyDie(gameObject.transform.position);
                //Destroy(gameObject);
            }
            StartCoroutine(wait(invencibilitySeconds));
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTakingDamage = false;
    }
}
