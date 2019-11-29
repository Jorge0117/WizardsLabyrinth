using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMenuFromCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(22);
        SceneManager.LoadScene("MainMenu");
    }
}
