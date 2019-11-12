using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerScript : MonoBehaviour
{
    // GameObject of Chawa
    GameObject chawa;
    
    // Sprites for Chawa's abilities 
    public Sprite currentSprite, fireSprite, airSprite, iceSprite, nonSprite;

    // Code section that contains Chawa's power
    private PlayerAttack chawaPlayerAttack;
    
    // Image to display ability
    private Image ability;

    void Start()
    {
        chawa = GameObject.Find("Chawa");
        chawaPlayerAttack = chawa.GetComponent<PlayerAttack>();
    }

    private void checkCurrentAbility()
    {
         
        if (chawaPlayerAttack.getCurrentSpell() == "fire")
        {
            currentSprite = fireSprite;
        } else if (chawaPlayerAttack.getCurrentSpell() == "air")
        {
            currentSprite = airSprite;
        }
        else if (chawaPlayerAttack.getCurrentSpell() == "ice")
        {
            currentSprite = iceSprite;
        }
        else
        {
            currentSprite = nonSprite;
        }
        
        gameObject.GetComponent<Image>().sprite = currentSprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        this.checkCurrentAbility();
    }
}
