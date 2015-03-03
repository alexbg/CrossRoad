using UnityEngine;
using System.Collections;

public class GetObjects : MonoBehaviour {

	RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Physics.Raycast(this.transform.position,this.transform.forward,out hit,6f)){
			Debug.Log (hit.transform.name);
		}

	}
}
