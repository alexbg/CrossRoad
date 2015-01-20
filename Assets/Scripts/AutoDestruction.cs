using UnityEngine;
using System.Collections;

public class AutoDestruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < -20)
			Destroy(this.gameObject);
	}
}
