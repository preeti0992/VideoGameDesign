using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class SceneMsgDisplay : MonoBehaviour
{
    // Start is called before the first frame update

	GameManager GM;
	private CanvasGroup canvasGroup;
	Text messageText;
	
	void Awake()
	{
		try{
			canvasGroup = GetComponent<CanvasGroup>();
		}
		catch(MissingComponentException){
			Debug.LogError("No CanvasGroup attached");
		}
	}

    void Start()
    {
        // pc= player.GetComponent<PlayerController>();
        GM = GameManager.Instance;
        // Debug.Log("In Scene Display GM "+GM );
        messageText = (Text)this.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
        // Debug.Log("In Scene Display messageText "+messageText );
    }

    void Update(){
    	if(GM.displayMessage){
    		showDisplay();
    	}
    	if (GM.displayMessage && Input.GetKeyUp (KeyCode.Return)){
    		hideDisplay();
    	}
    }
 
    public void showDisplay()
    {
    	// if(GM.displayMessage){
    		// Debug.Log("In update of SceneMsgDisplay");
    		if (!canvasGroup.interactable) {
				//pause game by setting timeScale =0f
				Time.timeScale =0f;
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
				canvasGroup.alpha = 1f;
				messageText.text = GM.userMessage;
	    	}
    }

    public void hideDisplay(){
    	// Debug.Log("Cleaning up the display");
    	if (canvasGroup.interactable) {
			//start game by setting timeScale =0f
			Time.timeScale =1f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0f;
			// messageText.text = GM.userMessage;
		}
		GM.closeDisplay();
    }

    // public IEnumerator pause(float time){
    // 	yield return new WaitForSeconds(time);
    // }
}