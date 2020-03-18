using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMSetter : MonoBehaviour
{
	GameManager GM;

	public GameObject player;
	public GameObject hpCollectible;
    public GameObject SceneMsgDisplay;

	void Awake(){
		if (GameManager.Instance==null){
            // no GM available call the preload screen and get GM instance
            // this is useful for development and single screen testing
            // Debug.Log("GM"+GM);
            SceneManager.LoadScene((int)GameState.PRELOAD);
        }
        else{
            GM = GameManager.Instance;
            // Debug.Log("GM "+GM);            
	     	// GM.updatePlayerController(FindObjectOfType<PlayerController>());
	     	// Debug.Log("GM playerController : "+ GM.playerController);
            GM.updatePlayer(player);
            // Debug.Log("GM player : "+ GM.player);
            GM.updateHPCollectible(hpCollectible);
            GM.updateMsgDisplay(SceneMsgDisplay.GetComponent<SceneMsgDisplay>());
            // Debug.Log("GM updateMsgDisplay : "+ GM.msgDisplay);
            // Debug.Log("GM hpCollectible : "+ GM.hpCollectible);

        }
	}
    // Start is called before the first frame update
    void Start()
    {

     	// GM = GameManager.Instance;
     	// GM.updatePlayerController(FindObjectOfType<PlayerController>());
     	// Debug.Log("GM playerController : "+ GM.playerController);
    }

    //call and check GM update
    void Update(){
        // Debug.Log("In setter update");
        GM.update();
    }
}
