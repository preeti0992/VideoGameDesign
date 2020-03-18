using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
   // NavMeshAgent agent;
   // Transform target;
    GameManager GM;
    NavMeshAgent agent;
    public bool PlayerDead = false;
    private float dirLeft = -1;
    private float limits = 3.0f;
    private float snkin = -10.0f;
    private Vector3 offset;

    public float speed = 0.1f;
    // public GameObject player;
    private Animator anim;
    public bool walkin=false;
    public bool chase = false;
    public bool awake=false;
    public bool awakezone = false;
    public bool attk = false;
    // public GameObject gp;
    public float hitRate = 0.5f;

    public static int MAXZOMBIEHEALTH =50;

    private int hp = MAXZOMBIEHEALTH;

    private Collider hit;
    // public GameObject hp_collectable;
    PlayerController gps;

   // public GameObject gp;
    
    public void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //GameObject gp = GameObject.Find("Player");
        // PlayerController gps= player.GetComponent<PlayerController>();

        hit = this.transform.GetChild(4).gameObject.GetComponent<Collider>();
        // Debug.Log("hit : " + hit);

    }
    void Start()
    {
        GM = GameManager.Instance;
        
        hitRate = GM.getEnemyHitRate(hitRate);
        // hitRate = 0.5f;

        // Debug.Log("GM "+GM);
        // PlayerController gps= GM.playerController;
        gps = GM.playerController;
        snkin = gps.SNK;

      //  orig_x = transform.position.x;
      //  orig_y = transform.position.y;
      //  orig_z = transform.position.z;
        offset = -transform.position + GM.player.transform.position;
       
        //target = player.transform;
    }

    void OnTriggerEnter(Collider c)
    {
        // Debug.Log("In trigger event");

        if (c.attachedRigidbody == null)
        {

        }
        else
        {
            ItemCollector bc = c.attachedRigidbody.gameObject.GetComponent<ItemCollector>();
          //  PlayerController player = c.attachedRigidbody.gameObject.GetComponent<PlayerController>();
            if (bc == null)
            {
            }
            else
            {
                // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
                //Destroy(this.gameObject);
                //bc.ReceiveBall();
                // Debug.Log("In awake test");
                awakezone = true;
                // Debug.Log("snkin <0 :"+ snkin);
                if (snkin < 0)
                {
                    // Debug.Log("In snkin test");
                    awake = true;
                    FindObjectOfType<ZbsAudio>().Play("ZbScream");
                    walkin = true;
                }
                
                
            }
        }

    }
    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody == null)
        {

        }
        else
        {
            ItemCollector bc = c.attachedRigidbody.gameObject.GetComponent<ItemCollector>();
            // GM.player = c.attachedRigidbody.gameObject.GetComponent<PlayerController>();
            if (bc == null)
            {
            }
            else
            {

                awakezone = false;

            }
        }

    }
    void Update()
    {
        if (!PlayerDead)
        {
            offset = -transform.position + GM.player.transform.position;
            // PlayerController gps = player.GetComponent<PlayerController>();
            snkin = gps.SNK;
            float dist = Vector3.Distance(transform.position, GM.player.transform.position);
            // walkin = awake;
            if (dist < 2.0) { attk = true; } else { attk = false; }
            if (chase == false) { attk = false; } else { anim.SetBool("Attacking", attk); }

            anim.SetBool("AZ", awakezone);
            anim.SetBool("Awaken", awake);
            anim.SetBool("Walking", walkin);
            if (awakezone)
            {
                if (snkin < 0)
                {
                    awake = true;
                    walkin = true;
                    // chase = true;
                    anim.SetBool("Awaken", true);
                    anim.SetBool("Walking", true);
                }
            }
            else
            {

            }

            if (chase)
            {
                offset.y = 0;


                // Quaternion rotation = Quaternion.LookRotation(offset);

                // transform.rotation = rotation;//(new Vector3(15, 30, 45) * Time.deltaTime);
                
                       

                            agent.SetDestination(GM.player.transform.position);
                     


                }

        }
    }
    public void AttackPlayer() 
    {

        PlayerController gps = GM.playerController;//.GetComponent<PlayerController>();
        if (GM.playerHP != 0)
        {
            gps.TakingDmg = true;
            // gps.hp -= 30;
            GM.reducePlayerHP(0.3f);
            FindObjectOfType<ZbsAudio>().Play("ZbHit");
        }
        else
        {
            PlayerDead = true;
            awake = false;
            chase = false;
            anim.SetBool("Awaken", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("AZ", false);
            //  anim.SetBool("Walkin", false);
            anim.SetBool("PlayerDead", true);
        }

    }
    public void StartWalking() 
    {
        chase = true;
        
    }

    // void OnCollisionEnter(Collision collision)
       // {
          //  Debug.Log("collision.gameObject.tag "+collision.gameObject.tag );
          //  if (collision.gameObject.CompareTag ("Pick Up"))
          //  {
             //   Debug.Log("Enemy collision");
                //collision.gameObject.SetActive (false);
               // Destroy(collision.gameObject);
            //    PlayerController pc = (PlayerController) player.GetComponent(typeof(PlayerController));
            //    pc.hitTarget();
            //}
       // }
    //}

    public void gotHit(){
        // Debug.Log("Hit simple zombie");
        hp=hp -(int)(hitRate*MAXZOMBIEHEALTH);
        hp = (hp<0) ? 0 : hp;
        // collision.gameObject.SetActive (false);
        // PlayerController pc = GM.player.GetComponent<PlayerController>();
        PlayerController pc = GM.playerController;
        
        // PlayerController player = collision.attachedRigidbody.gameObject.GetComponent<PlayerController>();
        // pc.hitTarget();

        if (hp<=0){
            Debug.Log("Enemy dead");
            Vector3 pos = new Vector3(transform.position.x,1,transform.position.z);
            GameObject new_hp_collectable = Instantiate(GM.hpCollectible, pos, transform.rotation) as GameObject;
            Destroy(new_hp_collectable, 10.0f);
            
            this.transform.gameObject.SetActive(false);
            pc.hitTarget();
        }
    }
}
