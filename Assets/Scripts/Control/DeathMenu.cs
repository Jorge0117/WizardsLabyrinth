using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Continue()
    {
        string scene = PlayerPrefs.GetString("currentScene");
        GameObject.Find("Music Controller").GetComponent<MusicController>().PlayMainTheme();
        SceneManager.LoadScene(scene);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
