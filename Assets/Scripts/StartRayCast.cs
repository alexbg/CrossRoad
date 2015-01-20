using UnityEngine;
using System.Collections;


public class StartRayCast : MonoBehaviour {
	private RaycastHit info;
	private GameObject character;
	// Use this for initialization
	void Start () {
		this.character = GameObject.Find ("Character");
	}
	
	// Update is called once per frame
	void Update () {;
		if(Physics.Raycast(this.transform.position, -transform.right,out info, 9.0f)){
			this.character.SendMessage("endGame");
		}
	}
}
