using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    
    // GameObject of Chawa
    GameObject chawa;
    // Code section that contains Chawa's health
    private PlayerAttack chawaAttackController;
    
    // Scrollbar to be changed the values.
    private Scrollbar spellCooldownBar;
    
    // Has the cooldown of all the spells.
    private float spellCooldownTimer;
    
    // Has the value of the current Cooldown of each spell.
    private float fireTimer, airTimer, iceTimer;

    // Keeps the time of the cooldown of the spells. Are reseted to 0 when the spell is used, and each second are added 1.
    private float currentFireTimer, currentAirTimer, currentIceTimer;

    // Start is called before the first frame update
    void Start()
    {
        chawa = GameObject.Find("Chawa");
        chawaAttackController = chawa.GetComponent<PlayerAttack>();
        
        //At the start all cooldowns are available.
        fireTimer = chawaAttackController.fireballCoolDown;
        currentFireTimer = fireTimer;
        
        airTimer = chawaAttackController.dashCoolDown;
        currentAirTimer = airTimer;
        
        iceTimer = chawaAttackController.iceCoolDown;
        currentIceTimer = iceTimer;
        
        spellCooldownBar = gameObject.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        // Need to get which spell is currently being displayed
        // If there is a timer that is not the same as the spellCooldownTimer each second should add 1 to the bar and move it.
        
        // Checks whether any of the timers are under their normal time and adds each second.
        string currentSpell = chawaAttackController.getCurrentSpell();
        float currentPercentage;

        switch (currentSpell)
        {
            case "fire":
                currentPercentage = currentFireTimer / fireTimer;
                break;
            case "air":
                currentPercentage = currentAirTimer / airTimer;
                break;
            case "ice":
                currentPercentage = currentIceTimer / iceTimer;
                break;
            default:
                currentPercentage = 1.0f;
                break;
        }

        spellCooldownBar.size = currentPercentage;
    }
    
    private IEnumerator IncreaseTimer(string  spellType) {
         
        while (true) {
            yield return new WaitForSeconds(1);
            if (spellType == "fire" && currentFireTimer < fireTimer)
            {
                currentFireTimer += 1.0f;
            } else if (spellType == "ice" && currentIceTimer < iceTimer)
            {
                currentIceTimer += 1.0f;
            } else if (spellType == "air" && currentAirTimer < airTimer)
            {
                currentAirTimer += 1.0f;
            }
        }
    }

    /// <summary>
    ///     Method that starts counter for the spell, changes the values of the spells timers.
    ///     This method is called each time a spell si triggered.
    /// </summary>
    /// <param name="spell"> name of the spell to change the values of the timer for that spell</param>
    public void triggerSpell(string spell)
    {
        if (spell == "fire")
        {
            currentFireTimer = 0.0f;
            StartCoroutine(IncreaseTimer("fire"));
        } else if (spell == "ice")
        {
            currentIceTimer = 0.0f;
            StartCoroutine(IncreaseTimer("ice"));
        } else if (spell == "air")
        {
            currentAirTimer = 0.0f;
            StartCoroutine(IncreaseTimer("air"));
        }
        
    }
    
    
}
