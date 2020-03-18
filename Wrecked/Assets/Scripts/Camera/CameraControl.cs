using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameManager GM;

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float rspeed = 5;
    private float yaw = 0.0f;
    private float pitch = -20.0f;
    public bool Aimin=false;

    // public GameObject player;
    // public GameObject camerabox;
    // public GameObject ShootCamera;
    GameObject camerabox;
    GameObject ShootCamera;
    private float aimindex;
    private Vector3 offset;



    public bool movingCamera=true;

    private void Awake()
    {
        aimindex = 1.0f;    
        Aimin = false;
        transform.Rotate(0.0f, 0.0f, 0.0f);
    }
    void Start()
    {

        GM = GameManager.Instance;

        transform.Rotate(0.0f, 0.0f, 0.0f);
        // offset = transform.position - GM.player.transform.position;
        offset = -transform.position + GM.player.transform.position;
        // float angleX = GM.player.transform.rotation.x;
        //  float angleY = GM.player.transform.rotation.y;
        //  float angleZ = GM.player.transform.rotation.z;
        // movingCamera=true;

        ShootCamera= GM.playerController.getShootCamera();
        // Debug.Log("shootCamera : "+ShootCamera);
        camerabox= GM.playerController.getCameraBox();
        // Debug.Log("cameraBox : "+camerabox);

        transform.Rotate(0.0f, 0.0f, 0.0f);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { 
            Aimin = !Aimin;
            aimindex *= -1.0f;
        
        }

            //float angleX = GM.player.transform.rotation.x;
            //float angleY = GM.player.transform.rotation.y;
            //float angleZ = GM.player.transform.rotation.z;

        offset = -transform.position + GM.player.transform.position;

        /** commented below code to make the fixed and not motion controled
        */
        // Debug.Log("movingCamera "+movingCamera);
        if (movingCamera == true){
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            
            // Debug.Log("yaw"+yaw);
            // Debug.Log("pitch"+pitch);

            pitch=(pitch<=-20f)?-20f:pitch;
            pitch=(pitch>0f)?0f:pitch;
            yaw=(yaw<-30f)?-30f:yaw;
            yaw=(yaw>=30f)?30f:yaw;

            Debug.Log("yaw"+yaw);
            Debug.Log("pitch"+pitch);
            
           // if (Input.GetKeyDown(KeyCode.W))
          //  { transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); }
           //  float moveVertical = Input.GetAxis("Horizontal") * rspeed;
        }
        
        Quaternion rotation = Quaternion.LookRotation(offset*aimindex);
        transform.rotation = rotation;
        transform.Rotate(pitch, yaw, 0.0f);
        if (Aimin) { transform.position = ShootCamera.transform.position; }
        else
        {
            transform.position = camerabox.transform.position; //+ offset;
        }
    }
}

