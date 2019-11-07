using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var localScale = gameObject.transform.localScale;
        var position = transform.position;
        Gizmos.DrawWireCube(position, new Vector3(localScale.x, localScale.y, localScale.z));
        Gizmos.DrawWireSphere(new Vector3(position.x, position.y - localScale.y / 2.3f, position.z), 0.5f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var position = transform.position;
            other.gameObject.GetComponent<PlayerController>().setCheckpoint(new Vector2(position.x, position.y - gameObject.transform.localScale.y / 2.3f));
        }
    }
}
