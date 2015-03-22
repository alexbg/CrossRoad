using UnityEngine;
using System.Collections;
using CrossRoad.Character;

public class IsMyInstantiate : MonoBehaviour {
	public GameObject mainCamera;
	// Use this for initialization
	void Start () {
		/*if(!this.GetComponent<NetworkView>().isMine){
			// Elimino los componentes que no necesito
			//Destroy (this.GetComponent<MouseLook>());
			Destroy (this.GetComponent<MoveCharacter>());
			Destroy (this.GetComponent<Rigidbody>());
			Destroy (this.GetComponent<AudioSource>());
			MouseLook[] look = this.GetComponentsInChildren<MouseLook>();

			foreach(MouseLook i in look){
				Destroy(i);
			}
			//this.mainCamera.SetActive(true);
		}else{
			this.mainCamera.SetActive(true);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
