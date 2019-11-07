using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("unlockedSpells"))
        {
            string unlockedSpells = PlayerPrefs.GetString("unlockedSpells");
            if (unlockedSpells[id] == '1')
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
