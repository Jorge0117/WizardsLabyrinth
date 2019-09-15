using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    BoxCollider2D collider;
    public LayerMask grassWallLayer;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(collider.IsTouchingLayers(grassWallLayer));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("pls");
    }
}
