using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public GameObject fireParticles;

    public string enemy;
    
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
                other.gameObject.GetComponent<EnemyController>().takeDamage(3);
            }
            if (enemy.CompareTo("Player") == 0)
            {
                Debug.Log("llegue aqui");
                other.gameObject.GetComponent<PlayerController>().takeDamage(3);
            }
        }
    }
    
}
