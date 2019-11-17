using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassWall_ChangeSprite : MonoBehaviour
{

    private SpriteRenderer rend;
    private Sprite normal, burn1, burn2, burn3, burn4, burn5;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        normal = Resources.Load<Sprite>("Prefabs/Scenary/GrassWall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
