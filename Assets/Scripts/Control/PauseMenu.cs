using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject PauseMenuUI;

    public GameObject FireSpellImage;
    public GameObject IceSpellImage;
    public GameObject WindSpellImage;

    public GameObject HeartsText;
    private MusicController musicController;

    private void Awake()
    {
        musicController = GameObject.Find("Music Controller").GetComponent<MusicController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("GO");
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        if (PlayerPrefs.HasKey("unlockedSpells"))
        {
            string spellArray = PlayerPrefs.GetString("unlockedSpells");
            if (spellArray[0] == '0')
            {
                FireSpellImage.SetActive(false);
            }
            else
            {
                FireSpellImage.SetActive(true);
            }
            if (spellArray[1] == '0')
            {
                IceSpellImage.SetActive(false);
            }
            else
            {
                IceSpellImage.SetActive(true);
            }
            if (spellArray[2] == '0')
            {
                WindSpellImage.SetActive(false);
            }
            else
            {
                WindSpellImage.SetActive(true);
            }
        }
        else
        {
            FireSpellImage.SetActive(false);
            IceSpellImage.SetActive(false);
            WindSpellImage.SetActive(false);
        }

        if (PlayerPrefs.HasKey("unlockedHearts"))
        {
            string unlockedHearts = PlayerPrefs.GetString("unlockedHearts");
            int numberOfHearts = unlockedHearts.Length - unlockedHearts.Replace("1", "").Length;
            HeartsText.GetComponent<TextMeshProUGUI>().text = numberOfHearts + "/20";
        }
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void MainMenu()
    {
        Resume();
        musicController.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
