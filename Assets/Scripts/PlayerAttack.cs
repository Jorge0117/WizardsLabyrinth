﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float framesBtwAttack;
    private float currentFramesBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentFramesBtwAttack = framesBtwAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFramesBtwAttack >= framesBtwAttack)
        {
            if (Input.GetButton("Fire1"))
            {
                animator.SetTrigger("attack");
                currentFramesBtwAttack = 0;
            }
        }
        else
        {
            currentFramesBtwAttack += 1;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(2);
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
