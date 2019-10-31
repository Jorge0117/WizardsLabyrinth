using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent (typeof (Controller2D))]
public class Enemy_Aabbeell_Fireball : MonoBehaviour
{
    //Controlador de 2d
    Controller2D controller;
    
    //Transform del fireball
    private Transform fireballTransform;
    
    //Velocidad en x
    public float velocityX = 6;
    
    //Particulas
    //public GameObject fireParticles;

    //Enemigo
    public string enemy;
    
    //Velocidad
    Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D> ();
        //Instantiate(fireParticles, gameObject.transform.position, Quaternion.identity);
        fireballTransform = GetComponent<Transform> ();
        Vector3 scale = fireballTransform.localScale;
        fireballTransform.localScale = new Vector3(scale.x  * -1, scale.y, scale.z);
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y = 0.0f;
        velocity.x = velocityX * -1;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GrassWall"))
        {
            other.gameObject.GetComponent<GrassWall>().burn();
        }

        if (other.gameObject.CompareTag(enemy))
        {
            if (enemy.CompareTo("Enemy") == 0)
            {
                other.gameObject.GetComponent<EnemyController>().takeDamage(3);
            }
            if (enemy.CompareTo("Player") == 0)
            {
                other.gameObject.GetComponent<PlayerController>().takeDamage(3);
            }
        }
    }
}
