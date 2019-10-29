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

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().takeDamage(3);
        }
    }
}
