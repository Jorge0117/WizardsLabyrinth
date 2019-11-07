using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Continue()
    {
        string scene = PlayerPrefs.GetString("currentScene");
        SceneManager.LoadScene(scene);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
