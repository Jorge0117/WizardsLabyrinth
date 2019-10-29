using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    Animator anim;
    public Transform pos;
    public float range;
    public LayerMask whatIsIce;
    public LayerMask whatIsWater;
    private SpriteRenderer spriteR;
    bool isWater;

    bool isMelting = false;
    bool isFreezing = false;

    public GameObject smokeParticles;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isWater = true;
        anim.SetBool("isWater", true);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void melt()
    {
        if (isWater)
        {
            anim.SetBool("isChanging", true);
            isFreezing = true;
            isWater = false;
        }
        else
        {
            anim.SetBool("isChanging", true);
            isMelting = true;
            isWater = true;
        }
        //Instantiate(smokeParticles, gameObject.transform.position, Quaternion.identity);

        StartCoroutine(wait(1));
    }

    private void OnMouseDown()
    {
        melt();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, range);
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (isWater)
        {
            Collider2D[] iceToMelt = Physics2D.OverlapCircleAll(pos.position, range, whatIsIce);
            for (int i = 0; i < iceToMelt.Length; i++)
            {
                if (!iceToMelt[i].GetComponent<WaterController>().isMelting)
                {
                    iceToMelt[i].GetComponent<WaterController>().melt();
                }
            }
            anim.SetBool("isChanging", false);
            isFreezing = false;
            gameObject.layer = LayerMask.NameToLayer("Water");
            yield return new WaitForSeconds(seconds / 2);
        }
        else
        {
            Collider2D[] waterToFreeze = Physics2D.OverlapCircleAll(pos.position, range, whatIsWater);
            for (int i = 0; i < waterToFreeze.Length; i++)
            {
                if (!waterToFreeze[i].GetComponent<WaterController>().isFreezing)
                {
                    waterToFreeze[i].GetComponent<WaterController>().melt();
                }
            }
            anim.SetBool("isChanging", false);
            isMelting = false;
            gameObject.layer = LayerMask.NameToLayer("Ice");
            yield return new WaitForSeconds(seconds / 2);
        }
    }
}
