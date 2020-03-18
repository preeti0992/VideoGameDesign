using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitGun : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody == null)
        {

        }
        else
        {
            PlayerController bc = c.attachedRigidbody.gameObject.GetComponent<PlayerController>();
            if (bc == null)
            {
            }
            else
            {

                bc.ReceiveGun();
                FindObjectOfType<PlayerAudio>().Play("GunFound");
                Destroy(this.gameObject);
            }
        }

    }
}