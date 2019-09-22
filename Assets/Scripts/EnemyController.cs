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

    public float probaSaltarSiJugadorSalta = 0.1f;
    public float probaSaltar = 0.05f;

    public float probaCambiarDireccion = 0.25f;
    public float probaCambiarDireccionChawaVisible = 0.1f;

    float izq_visible;
    float der_visible;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;

    Controller2D controller;

    GameObject jugador;

    Transform enemigoTransform;

    public int maxHealth = 6;
    public float recoilVelocity = 0.9f;
    private int currentHealth;

    private bool isTakingDamage = false;
    public int invencibilitySeconds = 1;

    int x;

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

        currentHealth = maxHealth;

        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Mover el enemigo cuando esta recibiendo daño, para simular que retrocede
        if (isTakingDamage)
        {
            velocity.x = (jugador.transform.position.x < enemigoTransform.position.x ? recoilVelocity: recoilVelocity * -1)  * moveSpeed;
        }
        else
        {
            bool visibleChawa = false;
            float distanciachawaenemigo = jugador.transform.position.x - enemigoTransform.position.x;


            if (izq_visible <= distanciachawaenemigo && der_visible <= distanciachawaenemigo)
            {
                visibleChawa = true;
                //Debug.Log("Lo estoy viendo");
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

            // proba que salte cuando el jugador presiona saltar
            // proba que salte por su cuenta
            if ((Input.GetButtonDown("Jump") && controller.collisions.below &&
                 Random.Range(0f, 1f) < probaSaltarSiJugadorSalta) ||
                (controller.collisions.below && Random.Range(0f, 1f) < probaSaltar))
            {
                velocity.y = jumpVelocity;
            }

            // Proba de que intente cambiar la direccion sin chawa visible
            // Proba de que intente cambiar la direccion con chawa visible
            if ((Random.Range(0f, 1f) < probaCambiarDireccion && !visibleChawa) ||
                (Random.Range(0f, 1f) < probaCambiarDireccionChawaVisible && visibleChawa))
            {
                velocity.x = impulso.x * moveSpeed;
            }
            else
            {
                if (visibleChawa)
                {
                    if (jugador.transform.position.x < enemigoTransform.position.x)
                    {
                        //mover hacia la izq
                        impulso.x = Random.Range(-1.0f, 0f);
                        velocity.x = impulso.x * moveSpeed;
                        this.x_impulso = impulso.x;
                    }
                    else
                    {
                        //mover hacia la der
                        impulso.x = Random.Range(0f, 1f);
                        velocity.x = impulso.x * moveSpeed;
                        this.x_impulso = impulso.x;
                    }
                }
            }
        }

        //Realiza el movimiento
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //Para girar el dibujo cada 10 frames
        if ( x == 10 && !isTakingDamage)
        {
            x = 0;
            //girar el dibujo
            if ( velocity.x < 0 && enemigoTransform.localScale.x < 0 || velocity.x > 0 && enemigoTransform.localScale.x > 0 ){
                enemigoTransform.localScale = new Vector3(enemigoTransform.localScale.x * -1, enemigoTransform.localScale.y, 1);
            }
        }


        x++;
    }

    public void takeDamage(int damage)
    {
        if(!isTakingDamage)
        {
            Debug.Log("Damage: " + damage);
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
