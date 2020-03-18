using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuitter : MonoBehaviour
{
    public void QuitGame(){
    	// Application.Quit();
    	SceneManager.LoadScene ((int)GameState.MAIN_MENU);
    }
}
