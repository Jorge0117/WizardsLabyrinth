using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("unlockedHearts"))
        {
            string unlockedHearts = PlayerPrefs.GetString("unlockedHearts");
            if (unlockedHearts[id] == '1')
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
