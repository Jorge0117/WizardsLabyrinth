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
    private int unlockedSpellCount = 0;
    
    // Current spell equiped of Chawa.
    private spells equipedSpell;
    
    public GameObject fireball;
    public GameObject ice;
    public float iceAngle = 30;
    
    private static readonly int Attack = Animator.StringToHash("attack");
    
    private SpellCooldown _spellCooldownController;
    public GameObject spellCooldown;

    //private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Sets all for Chawa's UI 
        _spellCooldownController = spellCooldown.GetComponent<SpellCooldown>();
            
        animator = GetComponent<Animator>();
        /*
        if (unlockedSpells.Contains(spells.Fire))
        {
            equipedSpell = spells.Fire;
        }
        */
    }

    private void Awake()
    {
        unlockedSpells = new spells[3];
        if (PlayerPrefs.HasKey("unlockedSpells"))
        {
            string spellArray = PlayerPrefs.GetString("unlockedSpells");
            if (spellArray[0] == '1')
            {
                unlockedSpells[0] = spells.Fire;
                unlockedSpellCount += 1;
                equipedSpell = unlockedSpells[0];
            }
            if (spellArray[1] == '1')
            {
                unlockedSpells[1] = spells.Ice;
                unlockedSpellCount += 1;
                equipedSpell = unlockedSpells[1];
            }
            if (spellArray[2] == '1')
            {
                unlockedSpells[2] = spells.Air;
                unlockedSpellCount += 1;
                equipedSpell = unlockedSpells[2];
            }
        }
        else
        {
            equipedSpell = spells.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
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
                //Starts cooldown animation in HUD
                _spellCooldownController.triggerSpell("fire");
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
                //Starts cooldown animation in HUD
                _spellCooldownController.triggerSpell("ice");
                nextIceTime = Time.time + iceCoolDown;
                var position = attackPos.position;
                GameObject spell = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                PlayerIce spell1Controller = spell.GetComponent<PlayerIce>();
                spell1Controller.enemy = "Enemy";
                GameObject spell2 = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                PlayerIce spell2Controller = spell2.GetComponent<PlayerIce>();
                spell2Controller.enemy = "Enemy";
                GameObject spell3 = Instantiate(ice, new Vector2(position.x + 2*Mathf.Sign(gameObject.transform.localScale.x), position.y ), Quaternion.identity);
                PlayerIce spell3Controller = spell3.GetComponent<PlayerIce>();
                spell3Controller.enemy = "Enemy";
                
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
                //Starts cooldown animation in HUD
                _spellCooldownController.triggerSpell("air");
                nextDashTime = Time.time + dashCoolDown;
                animator.SetBool("isDashing", true);
            }
        }

        if (Input.GetKey("e") && Time.time > nextSpellChange)
        {
            if (unlockedSpellCount > 1)
            {
                nextSpellChange = Time.time + spellChangeCoolDown;
                int index = Array.IndexOf(unlockedSpells, equipedSpell);
                index = (index + 1) % unlockedSpellCount;

                equipedSpell = unlockedSpells[index];
            }
        }
        
        if (Input.GetKey("q") && Time.time > nextSpellChange)
        {
            if (unlockedSpellCount > 1)
            {
                nextSpellChange = Time.time + spellChangeCoolDown;
                int index = Array.IndexOf(unlockedSpells, equipedSpell);
                index = (index - 1) % unlockedSpellCount;
                if (index == -1)
                    index = unlockedSpellCount - 1;
                equipedSpell = unlockedSpells[index];
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                //enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damage);
                if (enemiesToDamage[i].GetComponent<EnemyController>().enemyName.CompareTo("Aabbeell") == 0)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damage, damage/*Basic*/);
                }
                else
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().takeDamage(damage);
                }
            }
        }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public string getCurrentSpell()
    {
        string currentSpell;
        if (this.equipedSpell == spells.Fire)
        {
            currentSpell = "fire";
        } else if (this.equipedSpell == spells.Ice)
        {
            currentSpell = "ice";
        } else if (this.equipedSpell == spells.Air)
        {
            currentSpell = "air";
        }
        else
        {
            currentSpell = "";
        }

        return currentSpell;
    }

    public float getSpellCooldown()
    {
        return this.spellChangeCoolDown;
    }
    
    enum spells
    {
        None, Fire, Ice, Air
    };

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Book")) // Si coge libro, pocion o corazon, desaparece
        {
            int spellId = other.gameObject.GetComponent<BookController>().id;
            if (PlayerPrefs.HasKey("unlockedSpells"))
            {
                string unlockedSpells = PlayerPrefs.GetString("unlockedSpells");
                char[] spellArray = unlockedSpells.ToCharArray();
                spellArray[spellId] = '1';
                unlockedSpells = new string(spellArray);
                PlayerPrefs.SetString("unlockedSpells", unlockedSpells);
            }
            else
            {
                char[] spells = {'0', '0', '0'};
                spells[spellId] = '1';
                string unlockedSpells = new string(spells);
                PlayerPrefs.SetString("unlockedSpells", unlockedSpells);
            }
            Destroy(other.gameObject);

            if (spellId == 0)
            {
                unlockedSpells[0] = spells.Fire;
            }
            else if (spellId == 1)
            {
                unlockedSpells[1] = spells.Ice;
            }
            else if (spellId == 2)
            {
                unlockedSpells[2] = spells.Air;
            }

            equipedSpell = unlockedSpells[spellId];
            unlockedSpellCount += 1;
        }
    }
}

