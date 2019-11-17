using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x - parallaxEffect, position.y, position.z);
        

        
        transform.Translate(new Vector3(-parallaxEffect * Time.deltaTime, 0, 0));
        if (transform.position.x <= -20)
        {
            transform.position = new Vector3(2, 0, 1);
        }
        //transform.position = position;
    }
}
