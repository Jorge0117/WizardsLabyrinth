using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Aabbeell : MonoBehaviour
{
    public Transform attackPos;
    
    public Transform fireAttackPos;
    
    public float iceballCoolDown = 2f;
    private float nextIceballTime = 0f;
    public GameObject iceball;
    public float iceAngle = 5;
    public float fireballCoolDown = 2f;
    private float nextFireballTime = 0f;
    public GameObject fireball;
    public int prueba = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prueba++;
        if (prueba >= 200)
        {
            prueba = 0;
            fireAttack();
        }
    }

    void iceAttackHorizaontal()
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

	void iceAttackVertical(){
        nextIceballTime = Time.time + iceballCoolDown;
        var position = attackPos.position;
        //position.x = position.x * -1;
            
        GameObject spellA1 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA1Controller = spellA1.GetComponent<PlayerIce>();
        spellA1Controller.enemy = "Player";
        GameObject spellA2 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA2Controller = spellA2.GetComponent<PlayerIce>();
        spellA2Controller.enemy = "Player";
        GameObject spellA3 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA3Controller = spellA3.GetComponent<PlayerIce>();
        spellA3Controller.enemy = "Player";
            
        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            spellA1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
            spellA2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
            spellA3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
        else
        {
            spellA2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90 + iceAngle);
            spellA3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
            
        Vector3 spellAScale = spellA1.transform.localScale;
        spellA1.transform.localScale = new Vector3(spellAScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellAScale.y, spellAScale.z);



        GameObject spellB1 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB1Controller = spellB1.GetComponent<PlayerIce>();
        spellB1Controller.enemy = "Player";
        GameObject spellB2 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB2Controller = spellB2.GetComponent<PlayerIce>();
        spellB2Controller.enemy = "Player";
        GameObject spellB3 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB3Controller = spellB3.GetComponent<PlayerIce>();
        spellB3Controller.enemy = "Player";
            
        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            spellB1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
            spellB2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
            spellB3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
        else
        {
            spellB2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90 + iceAngle);
            spellB3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
            
        Vector3 spellBScale = spellB1.transform.localScale;
        spellB1.transform.localScale = new Vector3(spellBScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellBScale.y, spellBScale.z);
    }

    void fireAttack()
    {
        GameObject spell1 = Instantiate(fireball, new Vector2(74.74f, 3.55f), Quaternion.identity);
    }
}
