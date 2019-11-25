using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fire : MonoBehaviour
{
    public Transform attackPos;
    
    public float fireballCoolDown = 2f;
    private float nextFireballTime = 0f;
    private SFXController sfx;
    
    public GameObject fireball;
    public int fireballDamage = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.Find("SFX Controller").GetComponent<SFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireballTime)
        {
            nextFireballTime = Time.time + fireballCoolDown;
            var position = attackPos.position;
            //position.x = position.x * -1;
            GameObject spell = Instantiate(fireball,
                new Vector2(position.x + 2 * Mathf.Sign(gameObject.transform.localScale.x) * -1, position.y),
                Quaternion.identity);
            sfx.PlayFireBallSFX(position);
            PlayerFireBall spellController = spell.GetComponent<PlayerFireBall>();
            spellController.enemy = "Player";
            spellController.damage = fireballDamage;
            Vector3 spellScale = spell.transform.localScale;
            spell.transform.localScale = new Vector3(spellScale.x * Mathf.Sign(gameObject.transform.localScale.x) * -1,
                spellScale.y, spellScale.z);
        }
    }
}
