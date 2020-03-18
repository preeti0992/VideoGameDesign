using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class ZbPatrolController : MonoBehaviour
{
    GameManager GM;
    //NavMesh Needed
    NavMeshAgent agent;
    // Transform target;
    public GameObject[] Ptrwaypoints;
    public int curWaypoint = 0;
    private float Dis;
    private float dist;
    private bool viewblocked;

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
    public float hitRate = 0.25f;

    public static int MAXZOMBIEHEALTH =50;

    public int hp = MAXZOMBIEHEALTH;
    private float rdsound;
    // public Collider hit;
    // public GameObject hp_collectable;
    public bool PlayerDead = false;
    // public GameObject gp;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        curWaypoint = 0;
        Dis = Vector3.Distance(gameObject.transform.position, Ptrwaypoints[curWaypoint].transform.position);


    }
    void Start()
    {
      //  orig_x = transform.position.x;
      //  orig_y = transform.position.y;
      //  orig_z = transform.position.z;
        GM = GameManager.Instance;

        hitRate = GM.getEnemyHitRate(hitRate);
        offset = -transform.position + GM.player.transform.position;
        PlayerController plr= GM.playerController;
        snkin = plr.SNK;
        //target = player.transform;
    }

    void Update()
    {
        rdsound = Random.Range(-1.0f, 500.0f);
        if (rdsound < 0)
        {
            FindObjectOfType<ZbPAudio>().Play("ZbIdle");
        }
        if (!PlayerDead)
        {

            offset = -transform.position + GM.player.transform.position;
            PlayerController plr = GM.playerController;
            snkin = plr.SNK;
            dist = Vector3.Distance(transform.position, GM.player.transform.position);
            anim.SetFloat("Dist", dist);
            // walkin = awake;
            if (dist < 2.0) { attk = true; } else { attk = false; }
            if (chase == false) { attk = false; } else { anim.SetBool("Attacking", attk); }

            anim.SetBool("AZ", awakezone);


            //Check if awaken, once awaken, never sleep


            if (awakezone)
            {
                ViewBlocked();
                if (viewblocked)
                {
                    // Debug.Log("Viewblocked");

                }
                else
                {
                    awake = true;
                    walkin = true;
                    chase = true;
                    anim.SetBool("Awaken", true);
                    anim.SetBool("Walking", walkin);
                }

            }

            Ptrling();

            if (dist > 50)
            {
                anim.SetBool("Awaken", false);
                chase = false;
                walkin = false;

            }
            anim.SetBool("Walking", walkin);
        }

    }
  

    public void Ptrling()
    {
        if (chase)
        {
            if (dist < 2.0)    
            //use dummy chase if too close
            {
         
                offset.y = 0;
             

                Quaternion rotation = Quaternion.LookRotation(offset);

                transform.rotation = rotation;//(new Vector3(15, 30, 45) * Time.deltaTime);
            }
            else
            {
                
                agent.SetDestination(GM.player.transform.position);
            }
        }
        else
        {
            agent.SetDestination(Ptrwaypoints[curWaypoint].transform.position);
          
            SetNextWaypoint();
        }
        anim.SetFloat("MovinSpeed", agent.velocity.magnitude);
    }
    private void SetNextWaypoint()
    {

        bool pathpending = agent.pathPending;
        if (agent.remainingDistance < 2)
        {
            if (!pathpending) { curWaypoint += 1; }
            if (curWaypoint > 4) { curWaypoint = 0; }
        }


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
               
                awakezone = true;
                // Debug.Log("snkin <0 :"+ snkin);
                if (snkin < 0)
                {
                    // Debug.Log("In snkin test");
                    awake = true;
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
            PlayerController player = c.attachedRigidbody.gameObject.GetComponent<PlayerController>();
            if (bc == null)
            {
            }
            else
            {

                awakezone = false;

            }
        }

    }

    public void AttackPlayer()
    {
        PlayerController gps = GM.playerController;
        // PlayerController gps = player.GetComponent<PlayerController>();
        if (GM.playerHP != 0)
        {
            gps.TakingDmg = true;
            // GM.playerHP -= 30;
            GM.reducePlayerHP(0.3f);
            FindObjectOfType<ZbPAudio>().Play("ZbHit");
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
    public void ZbScream() 
    {
        FindObjectOfType<ZbPAudio>().Play("ZbScream");

        Debug.Log("Scream");
    }


    public void ViewBlocked()
    {

        viewblocked = Physics.Raycast(this.transform.position,offset,dist-5.0f);
        
    }
    public void gotHit(){

        // Debug.Log("Hit patrol zombie");
       
        hp=hp -(int)(hitRate*MAXZOMBIEHEALTH);
        hp = (hp<0) ? 0 : hp;
        // collision.gameObject.SetActive (false);
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
