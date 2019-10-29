using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage;

    Animator animator;

    public float basicAttackCoolDown = 0.5f;
    private float nextBasicAttackTime;
    public float fireballCoolDown = 2f;
    private float nextFireballTime;
    
    public float iceCoolDown = 2f;
    private float nextIceTime;

    public float dashCoolDown = 3f;
    private float nextDashTime;

    private float spellChangeCoolDown = 0.5f;
    private float nextSpellChange = 0.5f;
    
    private spells[] unlockedSpells;
    private spells equipedSpell;
    public GameObject fireball;
    public GameObject ice;
    public float iceAngle = 30;
    
    private static readonly int Attack = Animator.StringToHash("attack");

    //private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        unlockedSpells = new spells[3];
        unlockedSpells[0] = spells.Fire;
        unlockedSpells[1] = spells.Ice;
        unlockedSpells[2] = spells.Air;
        
        animator = GetComponent<Animator>();
        if (unlockedSpells.Contains(spells.Fire))
        {
            equipedSpell = spells.Fire;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextBasicAttackTime)
        {
            nextBasicAttackTime = Time.time + basicAttackCoolDown;
            
            animator.SetTrigger(Attack);
        }

        if (Input.GetButton("Fire2"))
        {
            if (equipedSpell == spells.Fire && Time.time >= nextFireballTime)
            {
                nextFireballTime = Time.time + fireballCoolDown;
                var position = attackPos.position;
                GameObject spell = Instantiate(fireball, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                PlayerFireBall spellController = spell.GetComponent<PlayerFireBall>();
                spellController.enemy = "Enemy";
                Vector3 spellScale = spell.transform.localScale;
                spell.transform.localScale = new Vector3(spellScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellScale.y, spellScale.z);
                //Instantiate(fireball, attackPos.transform);
            }
            
            if (equipedSpell == spells.Ice && Time.time >= nextIceTime)
            {
                nextIceTime = Time.time + iceCoolDown;
                var position = attackPos.position;
                GameObject spell = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                GameObject spell2 = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                GameObject spell3 = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);

                if (Mathf.Sign(transform.localScale.x) > 0)
                {
                    spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, iceAngle);
                    spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 360 - iceAngle);
                }
                else
                {
                    spell.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,180);
                    spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 + iceAngle);
                    spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 - iceAngle);
                }
                
                Vector3 spellScale = spell.transform.localScale;
                spell.transform.localScale = new Vector3(spellScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellScale.y, spellScale.z);
            }

            if (equipedSpell == spells.Air && Time.time >= nextDashTime)
            {
                nextDashTime = Time.time + dashCoolDown;
                animator.SetBool("isDashing", true);
            }
        }

        if (Input.GetKey("e") && Time.time > nextSpellChange)
        {
            if (unlockedSpells.Length > 0)
            {
                nextSpellChange = Time.time + spellChangeCoolDown;
                int index = Array.IndexOf(unlockedSpells, equipedSpell);
                index = (index + 1) % unlockedSpells.Length;

                equipedSpell = unlockedSpells[index];
                Debug.Log(equipedSpell);
            }
        }
        
        if (Input.GetKey("q") && Time.time > nextSpellChange)
        {
            if (unlockedSpells.Length > 0)
            {
                nextSpellChange = Time.time + spellChangeCoolDown;
                int index = Array.IndexOf(unlockedSpells, equipedSpell);
                index = (index - 1) % unlockedSpells.Length;
                if (index == -1)
                    index = 2;
                equipedSpell = unlockedSpells[index];
                Debug.Log(equipedSpell);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damage);
            }
        }

        if (Input.GetKey("c"))
        {
            gameObject.GetComponent<PlayerController>().returnToGround();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    enum spells
    {
        Fire, Ice, Air
    };

}

