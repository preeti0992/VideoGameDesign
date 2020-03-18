using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittarget : MonoBehaviour
{
	public EnemyType type;
	void OnTriggerEnter(Collider other)
	 {
	 	// Debug.Log("In hittarget other.gameObject.tag "+other.gameObject.tag );
		if (other.gameObject.CompareTag ("WeaponHit"))
		{
			// Debug.Log("WeaponHit");

			switch (type)
			{
				case EnemyType.CHARGER:
					ZbChargerController zc = transform.parent.GetComponent<ZbChargerController>();
					zc.gotHit();
					break;
				case EnemyType.SIMPLE:
					EnemyController ec = transform.parent.GetComponent<EnemyController>();
					ec.gotHit();
					break;
				case EnemyType.PATROL:
					ZbPatrolController pc = transform.parent.GetComponent<ZbPatrolController>();
					pc.gotHit();
					break;
			}
			
       		
       		Destroy(other.gameObject);
		}
        
    }
}
