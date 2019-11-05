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
    public int cantidadSegundos;
    bool isWater;

    bool isMelting = false;
    bool isFreezing = false;

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


    public void change()
    {
        if (isWater)
        {
            gameObject.tag = "Ice";
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

        StartCoroutine(wait(1));
    }

    private void OnMouseDown()
    {
        change();
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
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Ice");
            gameObject.tag = "Ice";
            Collider2D[] waterToFreeze = Physics2D.OverlapCircleAll(pos.position, range, whatIsWater);
            for (int i = 0; i < waterToFreeze.Length; i++)
            {
                if (!waterToFreeze[i].GetComponent<WaterController>().isFreezing && waterToFreeze[i].transform.position.y == gameObject.transform.position.y)
                {
                    waterToFreeze[i].GetComponent<WaterController>().change();
                }
            }
            anim.SetBool("isChanging", false);
            isMelting = false;

            yield return new WaitForSeconds(cantidadSegundos);

            anim.SetBool("isChanging", true);

            yield return new WaitForSeconds(1);

            gameObject.tag = "Water";
            anim.SetBool("isChanging", false);
            isFreezing = false;
            gameObject.layer = LayerMask.NameToLayer("Water");
            isWater = true;
        }
    }
}
