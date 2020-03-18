using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeytoDoor: MonoBehaviour
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
               
                bc.ReceiveKtoDoor();
                FindObjectOfType<PlayerAudio>().Play("TakingItem");
                Destroy(this.gameObject);
            }
        }

    }
}