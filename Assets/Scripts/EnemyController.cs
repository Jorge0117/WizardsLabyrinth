using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class EnemyController : MonoBehaviour
{
    //Vida inicial de enemigo
    public int maxHealth = 6;
    
    //Vida del enemigo
    private int currentHealth;

    //Se esta recibiendo daño?
    private bool isTakingDamage = false;
    
    //Tiempo en que no se recibe mas daño
    public int invencibilitySeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        if(!isTakingDamage)
        {
            isTakingDamage = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(wait(invencibilitySeconds));
        }
    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTakingDamage = false;
    }
}
