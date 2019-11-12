using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("unlockedKeys"))
        {
            string unlockedKeys = PlayerPrefs.GetString("unlockedKeys");
            Debug.Log(unlockedKeys);
            if (unlockedKeys[id] == '1')
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
}
