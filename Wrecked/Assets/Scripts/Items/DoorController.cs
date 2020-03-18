using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameManager GM;
    public bool closeenough=false;
    public bool hasitem=false;
    public bool Doneact=false;
    //public GameObject player;
    public float dist;

    void Awake()
    {

       
    }
    void Start()
    {
        GM = GameManager.Instance;
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, GM.player.transform.position);
        if (dist<1) { closeenough = true; } else { closeenough = false; }
        if (closeenough)
        {
            PlayerController gps = GM.playerController;
            Doneact = gps.Interacted;
            ItemCollector itm = GM.player.GetComponent<ItemCollector>();
            hasitem = itm.hasK;
            if (hasitem)
            {
                if (Doneact)
                {
                    FindObjectOfType<PlayerAudio>().Play("Cheer");
                    FindObjectOfType<PlayerAudio>().Play("OpenDoor");

                    Destroy(this.gameObject);
                    gps.Interacted = false;
                    itm.nK -= 1;
                }

            }
            else {
                if (Doneact)
                {

                  FindObjectOfType<PlayerAudio>().Play("CantOpen");
                }
                
                Debug.Log("Need a K"); gps.Interacted = false; }
        }
        else
        {
            PlayerController gps = GM.playerController;
            Doneact = gps.Interacted;
            ItemCollector itm = GM.player.GetComponent<ItemCollector>();
            hasitem = itm.hasK; gps.Interacted = false; }


    }


    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.attachedRigidbody == null)
    //    {

    //    }
    //    else
    //    {
    //        ItemCollector Ic = c.attachedRigidbody.gameObject.GetComponent<ItemCollector>();
    //        if (Ic == null)
    //        {
    //        }
    //        else
    //        {

    //            closeenough = true;

    //        }
    //    }
    //}
    //void OnTriggerExit(Collider c) 
    //{
    //    if (c.attachedRigidbody == null)
    //    {

    //    }
    //    else
    //    {
    //        ItemCollector Ic = c.attachedRigidbody.gameObject.GetComponent<ItemCollector>();
    //        if (Ic == null)
    //        {
    //        }
    //        else
    //        {

    //            closeenough = false;

    //        }
    //    }
  //  }

    
}
