using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
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

    private float spellChangeCoolDown = 0.5f;
    private float nextSpellChange = 0.5f;
    
    private spells[] unlockedSpells;
    private spells equipedSpell;
    public GameObject fireball;
    private static readonly int Attack = Animator.StringToHash("Attack");

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
        //Ataque
        if (Input.GetButton("Fire1") && Time.time >= nextBasicAttackTime)
        {
            nextBasicAttackTime = Time.time + basicAttackCoolDown;
            Debug.Log("Attack");
            animator.SetTrigger(Attack);
        }
        //Fuego
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
        
        //Mandar a hacer daño
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            
            /*Controller2D controller = GetComponent<Controller2D> ();
            
            //Jugador
            GameObject jugador;
            jugador = GameObject.Find("Chawa");
            
            bool visibleChawa = false;
            float distanciachawaenemigo = jugador.transform.position.x - enemyTransform.position.x;

            //Vemos si chawa esta visible
            if (left_visible <= distanciachawaenemigo && right_visible <= distanciachawaenemigo)
            {
                visibleChawa = true;
            }
            
            */
            GameObject chawa = GameObject.Find("Chawa");
            chawa.GetComponent<PlayerController>().takeDamage(damage);
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
