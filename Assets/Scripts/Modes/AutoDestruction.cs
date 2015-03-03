using UnityEngine;
using System.Collections;

public class AutoDestruction : MonoBehaviour {
	public GameObject walls;
	public GameObject lamps;
	public GameObject traps;
	public GameObject toggle;
	public GameObject rayCasts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < -20)
			Destroy(this.gameObject);
	}

	/*void OnBecameVisible(){

		//this.walls.SetActive (true);
		if(this.rayCasts != null)
			this.rayCasts.SetActive(true);
		if(this.traps != null)
			this.traps.SetActive (true);
		if(this.toggle != null)
			this.toggle.SetActive(true);
		if(this.lamps != null)
			this.lamps.SetActive (true);
	}

	void OnBecameInvisible(){

		//this.walls.SetActive (false);
		if(this.rayCasts != null)
			this.rayCasts.SetActive(false);
		if(this.traps != null)
			this.traps.SetActive (false);
		if(this.toggle != null)
			this.toggle.SetActive(false);
		if(this.lamps != null)
			this.lamps.SetActive (false);
	}*/
}
