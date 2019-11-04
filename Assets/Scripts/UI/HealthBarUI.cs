using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    // GameObject of Chawa
    GameObject chawa;
    
    // Code section that contains Chawa's health
    private PlayerController chawaController;
    
    // Scrollbar to be changed the values.
    private Scrollbar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        chawa = GameObject.Find("Chawa");
        chawaController = chawa.GetComponent<PlayerController>();
        healthBar = gameObject.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    { 
        // Obtains Chawa's MaxHealth and CurrentHealth.
        int chawaMaxHealth = chawaController.currentHealth;
        int chawaCurrentHealth = chawaController.maxHealth;
        
        // Sets the size of the bar.
        healthBar.size = chawaMaxHealth / chawaCurrentHealth;
    }
}
