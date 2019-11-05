using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomlessPit : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var localScale = gameObject.transform.localScale;
        var position = transform.position;
        Gizmos.DrawWireCube(position, new Vector3(localScale.x, localScale.y, localScale.z));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().respawnAfterFall();
        }
    }
}
