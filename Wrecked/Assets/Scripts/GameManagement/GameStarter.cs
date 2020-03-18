using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
	GameManager GM;

    void Start(){
        // Debug.Log("In GameStarter");
        if (GameManager.Instance==null){
            SceneManager.LoadScene((int)GameState.PRELOAD);
        }
        else{
            GM = GameManager.Instance;
        }
        // GM = GameManager.Instance;
    }
    public void StartGame(){
    	Debug.Log("In start game");
    	// SceneManager.LoadScene ((int)GameState.L1);
    	GM.ChangeGameState(GameState.L1);
    }
}
