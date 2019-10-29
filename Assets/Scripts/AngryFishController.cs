using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryFishController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 20, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 20, 0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3.0f * Time.deltaTime);
        }
    }
}
