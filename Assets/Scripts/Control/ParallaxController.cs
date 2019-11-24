using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{

    private float lengthx, startposx;
    public GameObject cam;
    public float parallaxEffect;
    
    void Start()
    {
        startposx = transform.position.x;
        lengthx = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distancex = cam.transform.position.x * parallaxEffect;
        
        transform.position = new Vector3(startposx + distancex, transform.position.y, transform.position.z);

        if (temp > startposx + lengthx) startposx += lengthx;
        else if (temp < startposx - lengthx) startposx -= lengthx;
    }
}
