using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    // Start is called before the first frame update

	private CanvasGroup canvasGroup;
	void Awake()
	{
		// Debug.Log("in awake");
		try{
			canvasGroup = GetComponent<CanvasGroup>();
		}
		catch(MissingComponentException){
			Debug.LogError("No CanvasGroup attached");
		}
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	/**
    	* Input.GetKeyUp() should eventually be replaced with Input.GetButtonUp()
    	* with a virtual button created in the InputManager settings. This will allow multiple game controllers
		* to map to common input events (e.g. simultaneous keyboard, and handheld game controller support.
    	*/
    	// Debug.Log("in update");
        if (Input.GetKeyUp (KeyCode.Escape)) {
        	// Debug.Log("in escape");
			if (canvasGroup.interactable) {
				//start game by setting timeScale =0f
				Time.timeScale =1f;
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
				canvasGroup.alpha = 0f;
			}
			else {
				//pause game by setting timeScale =0f
				Time.timeScale =0f;
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
				canvasGroup.alpha = 1f;
			}
		}
		/**
		* Time.timeScale =0f; will pause time for alll scenes
		* call SceneManager.LoadScene(), you will find the newly loaded scene to still be paused
		* So use void Start(){Time.timeScale =1f;} in script and attach to empty object in tyhe scen to make it unpaused once it get laoded
		*/
    }
}
