using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public GameObject fireParticles;

    public string enemy;
    public int damage = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fireParticles, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(collider.IsTouchingLayers(grassWallLayer));
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
                other.gameObject.GetComponent<EnemyController>().takeDamage(damage);
                EnemyController controllerEnemy= other.gameObject.GetComponent<EnemyController>();
                if (controllerEnemy.enemyName.CompareTo("Aabbeell") == 0)
                {
                    controllerEnemy.takeDamage(3, 1/*Fire*/);
                }
                else
                {
                    controllerEnemy.takeDamage(3);
                }
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
