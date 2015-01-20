using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
	public Canvas canvasWinner;
	public Text textWinner;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0.9f){
			this.canvasWinner.enabled = true;
			this.textWinner.enabled = true;
			GameObject.Find("Character").SendMessage("toggleMouse");
			Time.timeScale = 0;
		}
	}

	void OnLevelWasLoaded(int level) {
		//Time.timeScale = 1.0f;
	}
}
