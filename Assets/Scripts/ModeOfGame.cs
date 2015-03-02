using UnityEngine;
using System.Collections;

public class ModeOfGame : MonoBehaviour {

	public FallDown fallDown;
	public GameObject monster;
	//public Monster

	// Mode of Game
	// 0: Quiet
	// 1: Fall down walls
	// 2: Monster


	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("mode", 2);
		switch(PlayerPrefs.GetInt("mode")){
			case 0:
				Debug.Log ("Ha elegido modo normal");
				break;
			case 1:
			Debug.Log ("Ha elegido modo caida");
				this.fallDown.enabled = true;
				break;
			case 2:
				Debug.Log ("Ha elegido modo monster");
				StartCoroutine(startMonster());
				break;
		}
	}


	IEnumerator startMonster(){
		Debug.Log ("corontaine start");
		yield return new WaitForSeconds (5);
		Debug.Log ("corontaine final");
		this.monster.SetActive (true);
	}
	// Update is called once per frame
	/*void Update () {
	
	}*/
}
