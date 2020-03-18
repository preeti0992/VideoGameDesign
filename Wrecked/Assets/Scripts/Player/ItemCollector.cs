using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class ItemCollector : MonoBehaviour


{

    public float ColaSpeedBonus = 1.0f;
    public float SpeedBase = 1.0f;
    public float SpeedBonus = 1.0f;
    public float ColaTime = 1.0f;
    public bool ColaBuff = false;
    //private Rigidbody currBall;
    private Animator anim;
    public GameObject ballPrefab;

    public GameObject BallHoldSpot;
    public float Thwforce = 30.00f;
    public int nBall = 0;
    public Boolean hasBall = false;
    public GameObject currBall = null;


    public int nK = 0;
    public Boolean hasK = false;
    // Start is called before the first frame update
    private void Awake()
    {

        //ballPrefab = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.SetFloat("SpeedBonus", SpeedBonus);
    }

    void Update()
    {
        if (ColaBuff)
        {
            ColaTimer();
        }

    }

    public void SpeedUpdate()
    {
        SpeedBonus = SpeedBase * ColaSpeedBonus;
        anim.SetFloat("SpeedBonus", SpeedBonus);

    }


    public void ReceiveCola()
    {
        ColaTime += 10.0f;
        ColaSpeedBonus = 1.50f;
        ColaBuff = true;
        SpeedUpdate();
        //  ColaSpeedBonus = 1.00f;

    }
    public void ColaTimer()
    {
        ColaTime -= Time.deltaTime;
        if (ColaTime < 0)
        {
            ColaBuff = false;
            ColaSpeedBonus = 1.00f;
            SpeedUpdate();
            ColaTime = 10.0f;
        }
    }
    public void ReceiveBall()
    {
        nBall += 1;

        if (nBall > 0)
        {

            hasBall = true;
            anim.SetBool("hasball", true);

            GameObject RcurrBall;
            RcurrBall = Instantiate(ballPrefab, BallHoldSpot.transform) as GameObject;
            // if (nBall < 2)
            //  {
            RcurrBall.name = "Ballinhand";
            //}

        }
        // Rigidbody currBall_rb = currBall.GetComponent<Rigidbody>();

    }

    public void ThrowBall()
    {

        currBall = GameObject.Find("Ballinhand");

        currBall.transform.parent = null;
        Rigidbody currBall_rb = currBall.GetComponent<Rigidbody>();
        currBall_rb.isKinematic = false;
        currBall_rb.velocity = new Vector3(0, 0, 0);
        currBall_rb.angularVelocity = new Vector3(0, 0, 0);
        currBall_rb.AddForce(this.transform.forward * Thwforce, ForceMode.VelocityChange);
        nBall -= 1;
        currBall.name = "BallThrown";



        if (nBall == 0)
        {
            hasBall = false;
            anim.SetBool("hasball", false);
            //  GameObject.Destroy(.gameObject);
        }
    }
   

    public void ReceiveKtoDoor()
    {
        nK += 1;

        if (nK > 0)
        {

            hasK = true;
        
       

        }
        // Rigidbody currBall_rb = currBall.GetComponent<Rigidbody>();

    }

}
