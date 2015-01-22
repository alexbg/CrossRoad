using UnityEngine;
using System.Collections;

public class IAmClient : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isClient)
			this.gameObject.SetActive(false);
	}
}
