using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Continue()
    {
        string scene = PlayerPrefs.GetString("currentScene");
        if (scene == "Final")
        {
            scene = "Jungle";
            PlayerPrefs.SetString("currentScene", "Jungle");
            PlayerPrefs.SetFloat("checkpointPositionX", 239);
            PlayerPrefs.SetFloat("checkpointPositionY", 0);
        }
        GameObject.Find("Music Controller").GetComponent<MusicController>().PlayMainTheme();
        SceneManager.LoadScene(scene);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
