using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public GameObject fireParticles;
    
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().takeDamage(3);
        }
    }
    
}
