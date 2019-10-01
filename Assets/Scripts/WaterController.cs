using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public Sprite water;
    public Sprite ice;
    public bool isFreezing = false;

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
            if (gameObject.layer == LayerMask.NameToLayer("Obstacles"))
            {
                gameObject.layer = LayerMask.NameToLayer("Water");
                spriteR.sprite = water;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Obstacles");
                spriteR.sprite = ice;
            }
        }
    }

    private void OnMouseDown()
    {
        //freeze();
    }

    public void freeze()
    {
        anim.SetBool("isFreezing", true);
        isFreezing = true;

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
        for (int i = 0; i < waterToFreeze.Length; i++)
        {
            if (!waterToFreeze[i].GetComponent<WaterController>().isFreezing)
            {
                waterToFreeze[i].GetComponent<WaterController>().freeze();
            }
        }

        yield return new WaitForSeconds(seconds / 2);

        if (gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            gameObject.layer = LayerMask.NameToLayer("Water");
            spriteR.sprite = water;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Obstacles");
            spriteR.sprite = ice;
        }
    }
}
