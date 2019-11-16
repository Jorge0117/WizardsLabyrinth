using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MusicController musicController;

    private void Awake()
    {
        musicController = GameObject.Find("Music Controller").GetComponent<MusicController>();
    }

    public void NewGame()
    {
        musicController.PlayMainTheme();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Jungle");
    }

    public void ContinueGame()
    {
        musicController.PlayMainTheme();
        string scene = PlayerPrefs.GetString("currentScene");
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
