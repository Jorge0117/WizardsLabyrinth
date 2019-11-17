using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;

    public float moveSpeed = 6;
    public float dashSpeed = 2;
    private bool isDrawning;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;

    Controller2D controller;
    Animator animator;

    GameObject chawa;
    float chawaXScale;
    float chawaYScale;
    
    [HideInInspector]
    public Vector2 checkpointPosition;

    public GameObject angry_fish;
    private bool acercarse = false;
    private float firstGravityValue;
    public bool enableMoving;

    //Se esta recibiendo daño?
    private bool isTakingDamage = false;
    
    //Tiempo en que no se recibe mas daño
    public float invencibilitySeconds = 0.5f;

    public int maxHealth = 5;
    public int currentHealth;
    public float knockback = 0.5f;
    private int knockbackDir;

    [HideInInspector]
    public bool isDashing = false;

    private float dashAngle;

    public float dashDuration = 0.5f;

    private float stopDashTime;

    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    
    // Start is called before the first frame update
    void Start()
    {
        isDrawning = false;
        enableMoving = true;
        controller = GetComponent<Controller2D> ();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        chawa = GameObject.Find("Chawa");
        chawaXScale = chawa.transform.localScale.x;
        chawaYScale = chawa.transform.localScale.y;

        animator = GetComponent<Animator>();
        firstGravityValue = gravity;
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("maxHealth"))
        {
            maxHealth = PlayerPrefs.GetInt("maxHealth");
        }
        else
        {
            maxHealth = 5;
        }

        if (PlayerPrefs.HasKey("checkpointPositionX") && PlayerPrefs.HasKey("checkpointPositionY"))
        {
            checkpointPosition = new Vector2(PlayerPrefs.GetFloat("checkpointPositionX"), PlayerPrefs.GetFloat("checkpointPositionY"));
        }
        else
        {
            checkpointPosition = new Vector2(0,0);
        }

        if (PlayerPrefs.HasKey("currentScene"))
        {
            if (PlayerPrefs.GetString("currentScene") != SceneManager.GetActiveScene().name)
            {
                PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
                setCheckpoint(new Vector2(0,0));
            }
        }
        else
        {
            PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
        }

        gameObject.transform.position = checkpointPosition;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!animator.GetBool("isDashing") && !isTakingDamage)
        {
            if(controller.collisions.below)
            {
                animator.SetBool("isFalling", false);
                if (Input.GetButtonDown("Jump") && enableMoving)
                {
                    velocity.y = maxJumpVelocity;
                }
            }
            else
            {
                if (!animator.GetBool(IsFalling))
                {
                    animator.SetBool(IsFalling, true);
                }
            
            }
        
            if(Input.GetButtonUp("Jump") && !isDrawning && enableMoving)
            {
                if(velocity.y > minJumpVelocity)
                    velocity.y = minJumpVelocity;
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                animator.SetBool("isFrontSide", true);
                enableMoving = false;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                animator.SetBool("isFrontSide", false);
                enableMoving = true;
            }

            velocity.x = input.x * moveSpeed;
            velocity.y += gravity * Time.deltaTime;
            if(velocity.x > 0 && enableMoving)
            {
                chawa.transform.localScale = new Vector3(chawaXScale, chawaYScale, 1);
                animator.SetBool(IsWalking, true);
            }
            else if(velocity.x < 0 && enableMoving)
            {
                chawa.transform.localScale = new Vector3(-chawaXScale, chawaYScale, 1);
                animator.SetBool(IsWalking, true);
            }
            else
            {
                animator.SetBool(IsWalking, false);
            }
            if (enableMoving)
            {
                controller.Move(velocity * Time.deltaTime, input);
            }

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
        }
        else if (animator.GetBool("isDashing"))
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
                animator.SetBool(IsDashing, false);
                gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
        
        else if (isTakingDamage)
        {
            velocity = new Vector3(knockback * knockbackDir, knockback/2);
            controller.Move(velocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "1")
        {
            transform.parent = collision.transform;
        }
    }
    
    public void takeDamage(int damage, int dir)
    {
        if(!isTakingDamage)
        {
            animator.SetBool("isHit", true);
            isTakingDamage = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                GameObject.Find("Music Controller").GetComponent<MusicController>().Stop();
                SceneManager.LoadScene("Death");
            }

            knockbackDir = dir;
            StartCoroutine(wait(invencibilitySeconds));
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTakingDamage = false;
        animator.SetBool("isHit", false);
    }

    IEnumerator showObject(float seconds, GameObject objeto)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(objeto);
        animator.SetBool("isFrontSide", false);
        enableMoving = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isDashing)
        {
            //other.gameObject.GetComponent<EnemyController>().takeDamage(5);
            
            EnemyController controllerEnemy= other.gameObject.GetComponent<EnemyController>();
            if (controllerEnemy.enemyName.CompareTo("Aabbeell") == 0)
            {
                controllerEnemy.takeDamage(5, 3/*Wind*/);
            }
            else
            {
                controllerEnemy.takeDamage(5);
            }
        }
        if (other.gameObject.CompareTag("Water") && !isDrawning) // Si colisiona con agua, se hunde
        {
            velocity.y = 0;
            angry_fish.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 8);
            angry_fish.SetActive(true);
            acercarse = true;
            isDrawning = true;
            gravity = -4;
        }
        if (other.gameObject.name == "Angry-fish") // Si colisiona con pez, muere
        {
            gravity = firstGravityValue;
            angry_fish.SetActive(false);
            acercarse = false;
            isDrawning = false;
            respawnAfterFall();
        }
        if (other.gameObject.CompareTag("Heart"))
        {
            animator.SetBool("isFrontSide", true);
            enableMoving = false;
            other.gameObject.transform.position = gameObject.transform.position;

            int heartId = other.gameObject.GetComponent<HeartController>().id;
            if (PlayerPrefs.HasKey("unlockedHearts"))
            {
                string unlockedHearts = PlayerPrefs.GetString("unlockedHearts");
                char[] heartArray = unlockedHearts.ToCharArray();
                heartArray[heartId] = '1';
                unlockedHearts = new string(heartArray);
                PlayerPrefs.SetString("unlockedHearts", unlockedHearts);
            }
            else
            {
                char[] hearts = new char[20];
                for (int i = 0; i < hearts.Length; ++i)
                {
                    hearts[i] = '0';
                }
                hearts[heartId] = '1';
                string unlockedHearts = new string(hearts);
                PlayerPrefs.SetString("unlockedHearts", unlockedHearts);
            }

            maxHealth += 2;
            currentHealth = maxHealth;
            PlayerPrefs.SetInt("maxHealth", maxHealth);

            StartCoroutine(showObject(1, other.gameObject));
        }
        if (other.gameObject.CompareTag("Potion"))
        {
            if (currentHealth == maxHealth)
            {

            }
            else if (currentHealth == (maxHealth - 1))
            {
                currentHealth += 1;
            }
            else
            {
                currentHealth += 2;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            animator.SetBool("isFrontSide", true);
            enableMoving = false;
            other.gameObject.transform.position = gameObject.transform.position;

            int keyId = other.gameObject.GetComponent<KeyController>().id;
            if (PlayerPrefs.HasKey("unlockedKeys"))
            {
                string unlockedKeys = PlayerPrefs.GetString("unlockedKeys");
                char[] keyArray = unlockedKeys.ToCharArray();
                keyArray[keyId] = '1';
                unlockedKeys = new string(keyArray);
                PlayerPrefs.SetString("unlockedKeys", unlockedKeys);
            }
            else
            {
                char[] keys = new char[3];
                for (int i = 0; i < keys.Length; ++i)
                {
                    keys[i] = '0';
                }
                keys[keyId] = '1';
                string unlockedKeys = new string(keys);
                PlayerPrefs.SetString("unlockedKeys", unlockedKeys);
            }
            StartCoroutine(showObject(1, other.gameObject));
        }
    }

    public int getHealth()
    {
        return this.currentHealth;
    }

    public void setCheckpoint(Vector2 checkpoint)
    {
        PlayerPrefs.SetFloat("checkpointPositionX", checkpoint.x);
        PlayerPrefs.SetFloat("checkpointPositionY", checkpoint.y);
        checkpointPosition = checkpoint;
    }

    public void respawnAfterFall()
    {
        takeDamage(1, 0);
        transform.position = checkpointPosition;
    }
}
