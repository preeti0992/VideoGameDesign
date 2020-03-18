using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    GameManager GM;

    //state variables
    private Vector3 prevPos;
    private Vector3 slideDir;
    public float speed = 1;
    public float rspeed = 0.5f;
    public float SNK = 10;
    public float fwdspeed;
    public float Fight = -10;
    public bool DeadPlayer;
    public bool Interacted = false;
    public bool Interact = false;
    private float rdhitsound;

    private Animator anim;
    private int jumpHeight = 5;
    private Rigidbody rb;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool isBacking = false;
    public bool AimMode = false;
    public bool TakingDmg = false;

    public float Pfacing = 0;
    public GameObject HoldSpot;
    public GameObject GunPrefab;
    private float turn;
    public bool turning;
    public GameObject bullet;
    public float moveHorizontal;
    public float moveVertical;
    public float bullet_force;
    public bool grounded;
    public bool Gun=false;
    
    public bool RJump=false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        Vector3 slideDir = new Vector3(0.2f, 0.0f, 0.2f);
        // DeathChecker();
    }
    void Start()
    {
        GM = GameManager.Instance;
        // if (GameManager.Instance==null){
        //     SceneManager.LoadScene((int)GameState.PRELOAD);
        // }
        // else{
        //     GM = GameManager.Instance;
        // }
        prevPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        GroundCheck();
        DeathChecker();
    }

    //before performing physics calculations
    void Update()
    {
        if (slideDir.x!=0||slideDir.z!=0){
            slideDir = this.transform.position - prevPos;
        }
        
        //LandTimer();
        GroundCheck();
        Jumpchecker();
        Fallchecker();
        DeathChecker();

        if (!DeadPlayer)
        {
            moveHorizontal = Input.GetAxis("Vertical") * speed;
            moveVertical = Input.GetAxis("Horizontal") * rspeed;

            if (Input.GetKeyDown(KeyCode.C))
            {
                SNK *= -1;
                if (SNK > 0)
                {
                    FindObjectOfType<PlayerAudio>().Play("Girl");
                 

                }
            }
            if (Input.GetKeyDown(KeyCode.X)) { if (Gun) { Fight *= -1; CombatSwitch(); } else { Fight = -1; } }


            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact = true;

            }

            anim.SetBool("Interact", Interact);

            prevPos = this.transform.position;
            PlayerMov();

            anim.SetFloat("Snking", SNK);
            anim.SetFloat("FightStyle", Fight);
            anim.SetBool("IsRunning", isRunning);

            anim.SetBool("Backing", isBacking);
            anim.SetFloat("MSpeed", speed);
            anim.SetBool("TakingDmg", TakingDmg);


            Vector3 Shootpos = transform.forward;
            //Vector3 Shootpos = new Vector3(100, 0, 100);
            if (Fight > 0)
            {
                //bool assd = true;
                if (Input.GetKeyDown(KeyCode.F))
                {

                    // Debug.Log("shoot");

                    GameObject new_bullet;
                    GameObject ShootSpot = GameObject.Find("GunInHand");
                    Vector3 pos = ShootSpot.transform.position;// transform.position + new Vector3(-0.2f, 1.5f, 1f);
                    new_bullet = Instantiate(bullet, pos, transform.rotation) as GameObject;

                    Rigidbody bullet_rb = new_bullet.GetComponent<Rigidbody>();
                    anim.SetBool("IsShooting", true);
                    FindObjectOfType<PlayerAudio>().Play("Shoot");
                    bullet_rb.AddForce(Shootpos * bullet_force * 1000.0f);
                    //bullet_rb.AddForce(pos * bullet_force);

                    Destroy(new_bullet, 2.0f);
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.F)) { anim.SetBool("Punch", true); }
                else { anim.SetBool("Punch", false); }
            }
        }
    }
    public void DoneInteract() 
    {
       Interacted = true;
        Interact = false;
     }
   
    public void DeathChecker()
    {
        if (GM.playerHP <= 0)
        {
            // hp = 0;

            DeadPlayer = true;
            anim.SetBool("Die", true);
        }
        else { DeadPlayer = false; }
        if (Input.GetKeyDown(KeyCode.M)) { anim.SetBool("Die", true); }
    }
    public void Jumpchecker()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded&&isRunning)
        {
            anim.SetBool("QuitJump", false);
            anim.SetBool("Rjump", true);
            anim.SetBool("Fallin", false);

            RJump = true;

        }
       // anim.SetBool("RJump", RJump);
    }
    public void FootStep() {
        if (grounded&&!DeadPlayer) {
            FindObjectOfType<PlayerAudio>().Play("Foot");
        }
    }
    public void Fallchecker()
    {
        if (!grounded && !RJump)
        {
            anim.SetBool("Fallin", true);
            transform.Translate(slideDir);
            if (slideDir.y <0.2f)
            {
                rb.AddForce(slideDir*100000000);
            }
        }
        else
        {
            anim.SetBool("Fallin", false);
        }
    }
    public void RunStart()
    {  //allowtojump = true;

    }
    public void DeathWord() { FindObjectOfType<PlayerAudio>().Play("WhyMe"); }

    public void PlayerMov() {
        transform.Rotate(0, moveVertical, 0);
        Pfacing += moveVertical;
        fwdspeed = Input.GetAxis("Vertical");
        turn= Input.GetAxis("Horizontal")*5;
        if (moveHorizontal > 0) { isRunning = true; isBacking = false; }
        if (moveHorizontal == 0) { isRunning = false; isBacking = false; }
        if (moveHorizontal < 0) { isRunning = false; isBacking = true; }
        anim.SetFloat("FwdSpeed", fwdspeed);
        anim.SetFloat("Turn", turn);
        if (turn!=0) { turning = true; }else{ turning = false; }
        anim.SetBool("Turning", turning);
    }
    public void LandTimer() {

    }
    public void GroundCheck()
    {

        if (Physics.Raycast(transform.position, Vector3.down, 0.3f))
        {
            grounded = true;

        }
        else {
            grounded = false;

        }
    }
    public void DmgEnd()
    {
        TakingDmg = false;
        rdhitsound = Random.Range(0.0f, 5.0f);
        if (!DeadPlayer) {
            if (rdhitsound < 1)
            {
                FindObjectOfType<PlayerAudio>().Play("Hit1");
            }
            else if (rdhitsound < 2)
            {
                FindObjectOfType<PlayerAudio>().Play("Hit2");
            }
            else if (rdhitsound < 3)
            {
                FindObjectOfType<PlayerAudio>().Play("Hit3");
            }
            else if (rdhitsound < 4)
            {
                // FindObjectOfType<PlayerAudio>().Play("Hit4");//not found
                FindObjectOfType<PlayerAudio>().Play("Hit5");
            }
            else
            { FindObjectOfType<PlayerAudio>().Play("Hit5"); }
        }
        anim.SetBool("TakingDmg", TakingDmg);
    }
    public void ShootEnd()
    {
        anim.SetBool("IsShooting", false);
    }
    public void Land()
    {

        //landtimer = true;
        anim.SetBool("Rjump", false);
        anim.SetBool("QuitJump", true);
        RJump = false;
    }
    public void ReceiveGun() { Gun = true; }
    public void CombatSwitch()
    {
        //if(hasgun)
        if (Fight > 0) {
        GameObject Gun;
        Gun = Instantiate(GunPrefab, HoldSpot.transform) as GameObject;
        Gun.name = "GunInHand";
            FindObjectOfType<PlayerAudio>().Play("GunReady");
        }
        else
        {
            GameObject Gun = GameObject.Find("GunInHand");
            Destroy(Gun);

        }
    }

    public void hitTarget(){
        Debug.Log("Hit target");
        GM.addPlayerXP();
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("collision.gameObject.tag "+collision.gameObject.tag );
    //     if (collision.gameObject.CompareTag ("zombie"))
    //     {
    //         Debug.Log("Player collision");            
    //         GM.reducePlayerHP();
    //     }
    // }

    public void collectHP(){
        GM.addPlayerHP();
    }

    public GameObject getCameraBox(){
        return this.transform.GetChild(8).gameObject;
    }
    public GameObject getShootCamera(){
        return this.transform.GetChild(9).gameObject;
    }
}
