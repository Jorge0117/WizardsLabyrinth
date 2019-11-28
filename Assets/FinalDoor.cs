using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    private GameObject wKeySprite, key1, key1D, key2, key2D, key3, key3D;
    private bool inArea = false;
    private int numKeys = 0;
    void Start()
    {
        wKeySprite = gameObject.transform.GetChild(0).gameObject;
        wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
        
        key1 = gameObject.transform.GetChild(1).gameObject;
        key1D = gameObject.transform.GetChild(2).gameObject;
        key2 = gameObject.transform.GetChild(3).gameObject;
        key2D = gameObject.transform.GetChild(4).gameObject;
        key3 = gameObject.transform.GetChild(5).gameObject;
        key3D = gameObject.transform.GetChild(6).gameObject;
        
        key1.GetComponent<SpriteRenderer>().enabled = false;
        key1D.GetComponent<SpriteRenderer>().enabled = false;
        key2.GetComponent<SpriteRenderer>().enabled = false;
        key2D.GetComponent<SpriteRenderer>().enabled = false;
        key3.GetComponent<SpriteRenderer>().enabled = false;
        key3D.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("w") && inArea && numKeys == 3)
        {
            PlayerPrefs.SetFloat("checkpointPositionX", 0);
            PlayerPrefs.SetFloat("checkpointPositionY", 0);
            
            PlayerPrefs.SetString("currentScene", "Final");
            SceneManager.LoadScene("Final");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            
            if (PlayerPrefs.HasKey("unlockedKeys"))
            {
                string unlockedKeys = PlayerPrefs.GetString("unlockedKeys");
                if (unlockedKeys[0] == '1')
                {
                    key1.GetComponent<SpriteRenderer>().enabled = true;
                    key1D.GetComponent<SpriteRenderer>().enabled = false;
                    numKeys += 1;
                }
                else
                {
                    key1.GetComponent<SpriteRenderer>().enabled = false;
                    key1D.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (unlockedKeys[1] == '1')
                {
                    key2.GetComponent<SpriteRenderer>().enabled = true;
                    key2D.GetComponent<SpriteRenderer>().enabled = false;
                    numKeys += 1;
                }
                else
                {
                    key2.GetComponent<SpriteRenderer>().enabled = false;
                    key2D.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (unlockedKeys[2] == '1')
                {
                    key3.GetComponent<SpriteRenderer>().enabled = true;
                    key3D.GetComponent<SpriteRenderer>().enabled = false;
                    numKeys += 1;
                }
                else
                {
                    key3.GetComponent<SpriteRenderer>().enabled = false;
                    key3D.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                key1.GetComponent<SpriteRenderer>().enabled = false;
                key1D.GetComponent<SpriteRenderer>().enabled = true;
                key2.GetComponent<SpriteRenderer>().enabled = false;
                key2D.GetComponent<SpriteRenderer>().enabled = true;
                key3.GetComponent<SpriteRenderer>().enabled = false;
                key3D.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (numKeys == 3)
        {
            wKeySprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
            key1.GetComponent<SpriteRenderer>().enabled = false;
            key1D.GetComponent<SpriteRenderer>().enabled = false;
            key2.GetComponent<SpriteRenderer>().enabled = false;
            key2D.GetComponent<SpriteRenderer>().enabled = false;
            key3.GetComponent<SpriteRenderer>().enabled = false;
            key3D.GetComponent<SpriteRenderer>().enabled = false;
            inArea = false;
            numKeys = 0;
        }
    }
}
