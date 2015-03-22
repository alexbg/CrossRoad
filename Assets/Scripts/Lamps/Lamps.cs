using UnityEngine;
using System.Collections;

public class Lamps : MonoBehaviour {

	public ParticleSystem fire;
	public Light light;
	public GameObject torch;
	private bool lightIsAllowed;
	private bool enable;
	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt("lamp") == 1){
			this.lightIsAllowed = true;
			//Debug.Log ("Si lamparas");
		}else{
			this.lightIsAllowed = false;
			//Debug.Log ("No lamparas");
		}
		//Debug.Log (PlayerPrefs.GetInt ("lamp").ToString());
		//lightIsAllowed = true;
		//this.lightIsAllowed = true;
		if(Random.value > 0.9 && this.lightIsAllowed){
			this.enable = true;
			this.fire.Play();
			this.light.enabled = true;
			if(!PositionCharacter.multiplayer)
				this.GetComponent<AudioSource>().Play();
			this.torch.GetComponent<Renderer>().enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void OnBecameVisible(){
		Debug.Log ("Es visible");
		if(this.enable){
			this.fire.Play();
			this.light.enabled = true;
			this.audio.Play();
		}
	}

	void OnBecameInvisible(){
		Debug.Log ("No es visible");
		this.fire.Stop();
		//this.light.enabled = false;
		this.audio.Stop();
	}*/
}
