using UnityEngine;
using System.Collections;

public class AudioEmit : MonoBehaviour {
	public AudioSource[] source;
	// Use this for initialization
	void Start () {
		this.source = gameObject.GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
