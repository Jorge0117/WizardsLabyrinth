using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip mainTheme;

    private static MusicController instance = null;
    
    public static MusicController Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        // QUITAR CUANDO NO PRUEBAS
        /*
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Death")
        {
            PlayMainTheme();
        }
        */
    }

    public void PlayMainTheme()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = mainTheme;
        GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        GetComponent<AudioSource>().Stop();
    }
}
