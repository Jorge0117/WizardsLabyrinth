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
    
    private spells[] unlockedSpells;
    private spells equipedSpell;
    public GameObject fireball;
    private static readonly int Attack = Animator.StringToHash("attack");

    // Start is called before the first frame update
    void Start()
    {
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
                Vector3 spellScale = spell.transform.localScale;
                spell.transform.localScale = new Vector3(spellScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellScale.y, spellScale.z);
                //Instantiate(fireball, attackPos.transform);
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

