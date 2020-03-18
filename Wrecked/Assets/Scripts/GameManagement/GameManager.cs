using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Game States
// for now we are only using these two
public enum GameState { PRELOAD, MAIN_MENU, L1, L2 }
public enum EnemyType { SIMPLE, CHARGER, PATROL}

// public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager() {}

    //singleton class instance
    public static GameManager Instance { get; private set; }

    //use this delegate to call from the current state to load screen and handle cleanup
    // public event OnStateChangeHandler OnStateChange;
    public  GameState gameState { get; private set; }

    //player settings
    public GameObject player { get; private set; }
    public void updatePlayer(GameObject p){
        player = p;
        playerController = player.GetComponent<PlayerController>();    
    }

    public PlayerController playerController { get; private set; }
    public void updatePlayerController(PlayerController pc){
        playerController = pc;
    }

    public GameObject hpCollectible { get; private set; }
    public void updateHPCollectible(GameObject c){
        hpCollectible = c;
    }

    public static int MAXHEALTH =100;
    public static int MAXEXP =100;
    public static int MINEXP =0;
    private float hpReduceRate = 0.1f;
    private float hpAddRate = 0.2f;
    private float xpAddRate = 0.1f;

    public int playerXP { get; private set; }
    public int playerHP { get; private set; }

    public bool DebugMode = false;
    public float DebugPlayerHitRate =1f;

    public void addPlayerXP(){        
        playerXP = playerXP + (int)(xpAddRate*MAXEXP);
        playerXP = (playerXP > MAXEXP) ? MAXEXP : playerXP;
    }

    public void addPlayerHP(){        
        playerHP=playerHP +(int)(hpAddRate*MAXHEALTH);
        playerHP = (playerHP>MAXHEALTH) ? MAXHEALTH : playerHP;
    }

    public void reducePlayerHP(float reduceRate=0){
        if (reduceRate ==0 || DebugMode){
            reduceRate = DebugPlayerHitRate;
        }
        playerHP=playerHP -(int)(reduceRate*MAXHEALTH);
        playerHP = (playerHP<0) ? 0 : playerHP;
    }

    public string playerName;

    public bool hasIDCard { get; private set; }
    public void getID(){
        hasIDCard = true;
    }


    //zombie settings
    public bool bossIsDead { get; private set; }
    public void setBossDead(){
        Debug.Log("Boss Zombie dead");
        bossIsDead = true;
    }

    public float DebugEnemyHitRate = 0.5f;
    public float getEnemyHitRate(float rate){
        if (DebugMode){
            return DebugPlayerHitRate;
        }
        else return rate;
    }

    //game setting
    public int MAXTRIALS = 3;
    int trialCount = 1;
    bool playerDead = false;

    // public float sceneDelay = 440;
    public string userMessage { get; private set; }
    private string keyInstruction;

    // private string reloadMsg ="Test Run ";
    public string reloadStateMsg {
        get{
            return "Starting run "+ (trialCount )+"." + " Press Enter to continue...";
        }
        set{}
    }

    // private string getReloadMsg(){
    //     return reloadStateMsg+trialCount;s
    // }

    // private string restartMsg = "Reboot!";
    private string nextLevelMsg = "Next gear!" + " Press Enter to continue...";
    // private string WinMsg;
    public string WinMsg {
        get{
            // return "Congratulations "+ playerName+" you won against the zombies! Lets put our graduate cap on!" + keyInstruction;
            return "Congratulations "+ playerName+" you won against the zombies!" + keyInstruction;
        }
        set{}
    }

    public string LoseMsg {
        get{
            return "The zombies got the better or you "+ playerName+"! Better luck next time." + keyInstruction;
        }
        set{}
    }

    public bool displayMessage {get; private set;}
    public void closeDisplay(){
        displayMessage = false;
        Debug.Log("displayMessage : "+displayMessage);
        invokeStateChange();
    }
    
    public SceneMsgDisplay msgDisplay { get; private set; }
    public void updateMsgDisplay(SceneMsgDisplay dc){
        msgDisplay =dc;
    }


    //player settings
    void Awake()
    {
        // Debug.Log("GM awake");
        // Debug.Log("GM Instance"+Instance);
        if (Instance == null){
            Instance = this;
            Debug.Log("GM Instance"+Instance);
        } else{
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
        //don't destroy object instance between scenes
        DontDestroyOnLoad(Instance);
    }

    //  IEnumerator Start()
    // {
    //     Debug.Log("Start1");
    //     yield return new WaitForSeconds(2.5f);
    //     Debug.Log("Start2");
    // }


    void Start () {
        //set up static references and game data
        playerHP = MAXHEALTH;
        playerXP = MINEXP;
        displayMessage = false;
        userMessage ="";
        playerName = "Burdell";
        keyInstruction = " Press Enter to continue...";
        bossIsDead = false;

        //load first scene
        // SceneManager.LoadScene((int)GameState.MAIN_MENU);
        this.gameState = GameState.MAIN_MENU;
        invokeStateChange();
    }

    public void update(){
        // Debug.Log("In GM update");

        //player dies restart level/game
        if (playerHP==0 && trialCount == MAXTRIALS){
            Debug.Log("trialCount "+trialCount);
            Debug.Log("Player Dead!!! Restart Game");

            playerXP = 0;       
            playerHP = MAXHEALTH;
            trialCount =1;

            if (this.gameState==GameState.L1){
                hasIDCard = false;
            }

            //display message
            userMessage = LoseMsg;
            displayMessage = true;

            this.gameState = GameState.MAIN_MENU;

            gameState = GameState.L1;
            Debug.Log("userMessage "+userMessage);
            Debug.Log("gameState "+gameState);

            //reload state will be menu
            gameState = GameState.MAIN_MENU;
        }
        else if (playerHP==0 && trialCount < MAXTRIALS){

            if (!playerDead){
                playerDead = true;
                //should not reduce trialCount too many times for single death
                trialCount++;
            }
            
            Debug.Log("trialCount "+trialCount);     
            Debug.Log("Player Dead!!! Restart Level");
            Debug.Log("playerDead "+playerDead);   
            //reload current state
            //drop the XP to half
            playerXP = (int)(playerXP / 2);
            playerHP=MAXHEALTH;

            if (this.gameState==GameState.L1){
                hasIDCard = false;
            }

            //display message
            userMessage = reloadStateMsg;            
            displayMessage = true;

            //reload level will be same
            
        }
        
        //not dead

        //met condition for L1 to L2
        else if (this.gameState==GameState.L1 && hasIDCard){

            //display message
            Debug.Log("next stage");
            userMessage = nextLevelMsg;
            displayMessage = true;

            hasIDCard = false;

            this.gameState = GameState.L2;
        }
        // //met condition for L2 match boss zombie
        // else if (this.gameState==GameState.L2 && playerXP == MAXEXP){
        //     //enable boss zombie

        // }
        else if (this.gameState==GameState.L2 && bossIsDead){
            Debug.Log("boss died");
            //won game put in start state
            playerXP = 0;
            playerHP = MAXHEALTH;
            bossIsDead = false;

            //display message
            userMessage = WinMsg;
            displayMessage = true;

            //restart game
            this.gameState = GameState.MAIN_MENU;
        }
    }

    public void testInvoke(){
        Debug.Log("Test invoke");
    }

    //callback to tell the GM there will be scene change and do necessary object management
    private void invokeStateChange(){
        // Debug.Log("Changing state from "+ gameState + " to " + state);

        //change game state
        // if (state != null){
        //     Debug.Log("no state change expected");
        //     this.gameState = state;
        // }

        //do necessary object management here based on new state


        // //call the callback to load the new scene
        // OnStateChange();

        //load new scene
        playerDead = false;
        Debug.Log("playerDead "+playerDead);
        SceneManager.LoadScene ((int)this.gameState);
    }

    public void ChangeGameState(GameState state){
        Debug.Log("Changing state from "+ gameState + " to " + state);
        this.gameState = state;
        invokeStateChange();
    }

    //remove GM instance on quit
    public void OnApplicationQuit(){
        GameManager.Instance = null;
    }

}