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
    // Start is called before the first frame update
    void Start()
    {
        wKeySprite = gameObject.transform.GetChild(0).gameObject;
        wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Show");
        wKeySprite.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown("w"))
        {
            PlayerPrefs.SetFloat("checkpointPositionX", spawnPosition.x);
            PlayerPrefs.SetFloat("checkpointPositionY", spawnPosition.y);
            
            Scene scene = SceneManager.GetSceneByName(sceneToTransition);
            SceneManager.LoadScene(sceneToTransition);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        wKeySprite.GetComponent<SpriteRenderer>().enabled = false;
    }
}
