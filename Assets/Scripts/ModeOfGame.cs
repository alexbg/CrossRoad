using UnityEngine;
using System.Collections;

public class ModeOfGame : MonoBehaviour {

	public FallDown fallDown;
	//public Monster

	// Mode of Game
	// 0: Quiet
	// 1: Fall down walls
	// 2: Monster


	// Use this for initialization
	void Start () {
		switch(PlayerPrefs.GetInt("mode")){
			case 0:
				// Don't do nothing
				break;
			case 1:
				this.fallDown.enabled = true;
				break;
			case 2:
				// Monster
				break;
		}
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
}
