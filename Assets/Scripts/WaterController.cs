using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public Sprite water;
    public Sprite ice;
    public bool isFreeze = false;
    public bool isWater = true;

    public Transform pos;
    public float freezeRange;
    public LayerMask whatIsWater;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.H))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Ice"))
            {
                gameObject.layer = LayerMask.NameToLayer("Water");
                spriteR.sprite = water;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Ice");
                spriteR.sprite = ice;
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Si entra");
        change();
    }

    public void change()
    {
        anim.SetBool("isFreezing", true);
        if (isFreeze)
        {
            isWater = true;
            isFreeze = false;
        }
        else
        {
            isFreeze = true;
            isWater = false;
        }

        StartCoroutine(wait(2));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, freezeRange);
    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        Collider2D[] waterToFreeze = Physics2D.OverlapCircleAll(pos.position, freezeRange, whatIsWater);

        if (isWater)
        {
            for (int i = 0; i < waterToFreeze.Length; i++)
            {
                if (!waterToFreeze[i].GetComponent<WaterController>().isFreeze)
                {
                    waterToFreeze[i].GetComponent<WaterController>().change();
                }
            }

            yield return new WaitForSeconds(seconds / 2);

            anim.SetBool("isFreezing", false);
            gameObject.layer = LayerMask.NameToLayer("Water");
            //spriteR.sprite = water;
        }
        else
        {
            for (int i = 0; i < waterToFreeze.Length; i++)
            {
                if (!waterToFreeze[i].GetComponent<WaterController>().isFreeze)
                {
                    waterToFreeze[i].GetComponent<WaterController>().change();
                }
            }

            yield return new WaitForSeconds(seconds / 2);

            anim.SetBool("isFreezing", false);
            gameObject.layer = LayerMask.NameToLayer("Ice");
            //spriteR.sprite = ice;
        }
    }
}
