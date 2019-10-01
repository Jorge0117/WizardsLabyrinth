using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIce : MonoBehaviour
{
    public float velocity = 0.2f;
    Transform transform;
    public int dir = 1;

    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        angle = transform.eulerAngles.z;
        Debug.Log(angle);
        Debug.Log(Mathf.Round(Mathf.Cos(Mathf.PI * angle / 180)));
    }

    // Update is called once per frame
    void Update()
    {
        float velocityX = Mathf.Round(Mathf.Cos(Mathf.PI * angle / 180)) * velocity;
        //Debug.Log(velocityX);
        float velocityY = Mathf.Round(Mathf.Sin(Mathf.PI * angle / 180)) * velocity;
        transform.position = new Vector2(transform.position.x + velocityX * dir, transform.position.y + velocityY);
    }
}
