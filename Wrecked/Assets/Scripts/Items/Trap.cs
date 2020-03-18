using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(Animator))]
public class Trap : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {

        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider c)
    {
      
                anim.SetBool("TrapA", true);
       

    }
    
}
