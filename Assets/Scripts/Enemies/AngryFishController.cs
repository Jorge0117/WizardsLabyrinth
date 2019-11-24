using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryFishController : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 7f * Time.deltaTime);
        }
    }
}
