using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private MusicController musicController;
    public Button continueButton;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentScene"))
        {
            continueButton.enabled = false;
        }
    }

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
        if (scene == "Final")
        {
            scene = "Jungle";
            PlayerPrefs.SetString("currentScene", "Jungle");
            PlayerPrefs.SetFloat("checkpointPositionX", 239);
            PlayerPrefs.SetFloat("checkpointPositionY", 0);
        }
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
