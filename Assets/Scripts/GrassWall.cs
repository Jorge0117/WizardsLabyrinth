using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassWall : MonoBehaviour
{
    Animator anim;
    public Transform pos;
    public float burnRange;
    public LayerMask whatIsGrassWall;

    public bool isBurning = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            anim.SetBool("Hited", false);
        }
    }

    void OnMouseDown()
    {
        burn();
    }

    public void burn()
    {
        anim.SetBool("Hited", true);
        isBurning = true;

        StartCoroutine(wait(2));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, burnRange);
    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        Collider2D[] grassWallToBurn = Physics2D.OverlapCircleAll(pos.position, burnRange, whatIsGrassWall);
        for (int i = 0; i < grassWallToBurn.Length; i++)
        {
            if (!grassWallToBurn[i].GetComponent<GrassWall>().isBurning)
            {
                grassWallToBurn[i].GetComponent<GrassWall>().burn();
            }
        }

        yield return new WaitForSeconds(seconds / 2);
        Destroy(gameObject);
    }
}
