using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue_Manager : MonoBehaviour
{
    Queue<string> sentences;

    public GameObject dialoguePanel;
    public TextMeshProUGUI displayText;
    public Dialogue dialogue;

    string activeSentence;
    public float typingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && displayText.text == activeSentence)
        {
            DisplayNextSentence();
        }
    }

    void StartDialogue()
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentenceList)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            GameObject player = GameObject.Find("Chawa");
            StartCoroutine(player.GetComponent<PlayerController>().showObject(1, gameObject));
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;

        StartCoroutine(TypeSentence(activeSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        displayText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && displayText.text == activeSentence)
            {
                DisplayNextSentence();
            }
        }
    }
}
