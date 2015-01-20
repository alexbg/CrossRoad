using UnityEngine;
using System.Collections;

public class testPhycisc : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		this.rigidbody.AddForce (Vector3.right * 1 , ForceMode.Force);
		Debug.Log (this.rigidbody.velocity.magnitude);
	}
}
