using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsScript : MonoBehaviour
{
    private Text healthPointsText;
    
    // GameObject of Chawa
    GameObject chawa;
    
    // Code section that contains Chawa's health
    private PlayerController chawaController;
    
    // Start is called before the first frame update
    void Start()
    {
        chawa = GameObject.Find("Chawa");
        chawaController = chawa.GetComponent<PlayerController>();
        healthPointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtains Chawa's MaxHealth and CurrentHealth.
        int chawaCurrentHealth = chawaController.currentHealth;
        int chawaMaxHealth = chawaController.maxHealth;
        healthPointsText.text = chawaCurrentHealth.ToString() +"/"+chawaMaxHealth.ToString();
    }
}
