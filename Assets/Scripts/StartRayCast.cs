using UnityEngine;
using System.Collections;


public class StartRayCast : MonoBehaviour {
	private RaycastHit info;
	private GameObject menu;
	private bool send;
	// Use this for initialization
	void Start () {
		this.send = true;
		this.menu = GameObject.Find ("MenuInGame");
	}
	
	// Update is called once per frame
	void Update () {;
		if(Physics.Raycast(this.transform.position, -transform.right,out info, 9.0f) && this.send){
			this.menu.SendMessage("winner");
			Screen.showCursor = true;
			this.send = false;
		}
	}
}
