using UnityEngine;
using System.Collections;

public class Lamps : MonoBehaviour {

	public ParticleSystem fire;
	public Light light;
	private bool lightIsAllowed;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("lamp") == 1)
			this.lightIsAllowed = true;
		else
			this.lightIsAllowed = false;

		if(Random.value > 0.9 && this.lightIsAllowed){

			this.fire.Play();
			this.light.enabled = true;
			this.audio.Play();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
