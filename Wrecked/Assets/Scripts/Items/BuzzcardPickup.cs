using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzcardPickup: MonoBehaviour
{
    GameManager GM;
    public void Start()
    {
        GM = GameManager.Instance;
    }
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
                //bc.ReceiveBall();
                GM.getID();
            }
        }

    }
}