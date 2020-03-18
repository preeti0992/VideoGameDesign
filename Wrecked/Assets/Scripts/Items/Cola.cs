using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody == null)
        {

        }
        else
        {
            ItemCollector bc = c.attachedRigidbody.gameObject.GetComponent<ItemCollector>();
            if (bc == null)
            {
            }
            else
            {
                // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
                Destroy(this.gameObject);
                FindObjectOfType<PlayerAudio>().Play("Burp");
                bc.ReceiveCola();
            }
        }

    }
}
