using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FallDown : MonoBehaviour {
	public Transform[] objects;
	private float time;
	private int objectToFall;
	void Awake(){
		//this.objects = new Dictionary<int, Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		//this.objects = new Dictionary<int, Rigidbody> ();
		this.objectToFall = 1;
	}
	
	// Update is called once per frame
	void Update () {
		this.time += Time.deltaTime;
		// Cada 5 segundos se cae uno de los muros
		if(this.time >= 5 && this.objectToFall < this.objects.Length){
			Debug.Log (this.objects[this.objectToFall]);
			this.time = 0;
			this.objects[this.objectToFall].rigidbody.isKinematic = false;
			// Hay que darle un mepujoncito para que se caiga despues de quitarle el kinematic
			this.objects[this.objectToFall].rigidbody.AddForce(-transform.up,ForceMode.Impulse);
			this.objectToFall++;
		}
	}
}
