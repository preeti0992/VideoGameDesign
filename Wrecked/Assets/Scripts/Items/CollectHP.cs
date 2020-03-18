using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHP : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        // Debug.Log("collision.gameObject.tag "+c.gameObject.tag );
        if (c.gameObject.CompareTag ("Player"))
        {            
            Debug.Log("collecting hp");
            PlayerController player = c.attachedRigidbody.gameObject.GetComponent<PlayerController>();
            player.collectHP();
            Destroy(this.gameObject);
        }
    }
}
