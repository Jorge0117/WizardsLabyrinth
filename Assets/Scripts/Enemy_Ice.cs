﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ice : MonoBehaviour
{
    public Transform attackPos;
    
    public float iceballCoolDown = 2f;
    private float nextIceballTime = 0f;
    
    public GameObject iceball;
    
    public float iceAngle = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextIceballTime)
        {
            nextIceballTime = Time.time + iceballCoolDown;
            var position = attackPos.position;
            //position.x = position.x * -1;
            
            GameObject spell = Instantiate(iceball, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
            PlayerIce spell1Controller = spell.GetComponent<PlayerIce>();
            spell1Controller.enemy = "Player";
            GameObject spell2 = Instantiate(iceball, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
            PlayerIce spell2Controller = spell2.GetComponent<PlayerIce>();
            spell2Controller.enemy = "Player";
            GameObject spell3 = Instantiate(iceball, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
            PlayerIce spell3Controller = spell3.GetComponent<PlayerIce>();
            spell3Controller.enemy = "Player";
            
            if (Mathf.Sign(transform.localScale.x) > 0)
            {
                spell.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,180);
                spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 + iceAngle);
                spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 - iceAngle);
            }
            else
            {
                spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, iceAngle);
                spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 360 - iceAngle);
            }
            
            Vector3 spellScale = spell.transform.localScale;
            spell.transform.localScale = new Vector3(spellScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellScale.y, spellScale.z);
        }
    }
}