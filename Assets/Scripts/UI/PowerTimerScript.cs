using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerTimerScript : MonoBehaviour
{
    
    
    // GameObject of Chawa
    GameObject chawa;
    private PlayerAttack chawaControllerAttack
    private 

    // Saves the length of the timer 
    private float maxTimerSpell;
    private float currentTimerFire;
    private float currentTimerIce;
    private float currentTimerAir;

    // Scrollbar of the timer for the spells to be changed the values.
    private Scrollbar spellTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        chawa = GameObject.Find("Chawa");
        chawaControllerAttack = chawa.GetComponent<PlayerAttack>();
        spellTimer = gameObject.GetComponent<Scrollbar>();
        this.maxTimerSpell = chawaControllerAttack.getSpellCooldown();
        
        // At the beginning every cooldown must be active.
        this.currentTimerFire, this.currentTimerIce, this.currentTimerAir = this.maxTimerSpell;
    }

    // Update is called once per frame
    void Update()
    {
        // Ver metodo que mandó Araya y meter ahí que llame un evento de acá. 
        // Esto hace que active el timer y el update se encarga de ir aumentando el tiempo hasta 5.
        // 
        
        // We get first which spell is currently using and calculate the values.
        string currentSpell = chawaControllerAttack.getCurrentSpell();
        
        if (currentSpell == "fire")
        {
            
        } else if (currentSpell == "air")
        {
            
        }
        else if (currentSpell == "ice")
        {
            
        }
        spellTimer.size = ;
    }
}
