using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    Vector2 floatY;
    public float originalY;

    public float floatStrength;

    void Start()
    {
        this.originalY = gameObject.transform.position.y;
    }

    void Update()
    {
        floatY = transform.position;
        floatY.y = (Mathf.Sin(Time.time) * floatStrength) + originalY;
        transform.position = floatY;
    }
}
