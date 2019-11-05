using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Aabbeell_Fireballs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Se destruye después de 4 segundos
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
