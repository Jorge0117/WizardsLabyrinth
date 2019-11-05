using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIce : MonoBehaviour
{
    public LayerMask collisionLayer;

    public float velocity = 0.2f;
    Transform transform;
    public int dir = 1;

    private float angle;
    public int damage = 3;
    
    public string enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        angle = transform.eulerAngles.z;
        
        // Se destruye después de 3 segundos
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        float velocityX = Mathf.Cos(Mathf.PI * angle / 180) * velocity;
        //Debug.Log(velocityX);
        float velocityY = Mathf.Sin(Mathf.PI * angle / 180) * velocity;
        var position = transform.position;
        position = new Vector2(position.x + velocityX * dir, position.y + velocityY);
        transform.position = position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Obstacles")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag(enemy))
        {
            if (String.Compare(enemy, "Enemy", StringComparison.Ordinal) == 0)
            {
                other.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            }
            if (enemy.CompareTo("Player") == 0)
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
}
