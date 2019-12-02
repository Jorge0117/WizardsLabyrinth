using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToTransition;
    public Vector2 spawnPosition = new Vector2(0, 0);

    private GameObject wKeySprite;

    private bool inArea = false;
    // Start is called before the first frame update
    void Start()
    {
        wKeySprite = gameObject.transform.GetChild(0).gameObject;
        wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        
        if (Input.GetAxisRaw("Vertical") >= 1 && inArea)
        {
            PlayerPrefs.SetFloat("checkpointPositionX", spawnPosition.x);
            PlayerPrefs.SetFloat("checkpointPositionY", spawnPosition.y);
            
            PlayerPrefs.SetString("currentScene", sceneToTransition);
            SceneManager.LoadScene(sceneToTransition);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wKeySprite.GetComponent<SpriteRenderer>().enabled = true;
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
            inArea = false;
        }
    }
}
