using UnityEngine;
using System.Collections;

public class EnableTraps : MonoBehaviour {

	// This is the gameobject of trap;
	public GameObject trap;
	// If this is true then the trap will be enabled when the character enter in collision
	private bool isEnable;
	public AudioSource globalSound;

	// Use this for initialization
	void Start () {
		//this.isEnable = true;
		if(Random.value > 0.4){
			this.isEnable = true;
			this.collider.enabled = true;
			this.renderer.enabled = true;
		}

	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

	/*void OnCollisionEnter(Collision info){
		
		this.fireTrap (info);
			
	}*/

	/*void OnControllerColliderHit(ControllerColliderHit info){
		Debug.Log ("Colision");
	}*/

	void OnTriggerEnter(Collider info){
	
		this.fireTrap(info);
	}


	public void fireTrap(Collider info){
		Debug.Log ("Colision con:" + this.trap.tag);
		if (this.isEnable && info.tag == "Player") {
			switch(this.trap.tag){
				case "Ball":
					trap.rigidbody.isKinematic = false;
					this.genericTrapsActions();
					break;

				case "Trampoline":
					info.rigidbody.freezeRotation = false;
					info.rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
					
					this.genericTrapsActions();
					break;

				case "Tree":
					Debug.Log ("Ha entrado en el Tree");
					//trap.rigidbody.isKinematic = false; // No funciona bien 
					trap.rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
					this.genericTrapsActions();
					break;

				case "Explosion":
					Debug.Log("Ha explotado");
					Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5f);
					int i = 0;
					while (i < hitColliders.Length) {
						if(hitColliders[i].rigidbody){
							hitColliders[i].rigidbody.freezeRotation = false;
							hitColliders[i].rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);	
						}
						i++;
					}
					this.genericTrapsActions();
					break;

				case "SpikeGround":
					this.genericTrapsActions(true);
					this.trap.SetActive(false);
					break;
			}
		}
	}

	/// <summary>
	/// Acciones genericas de las trampas
	/// </summary>
	/// <param name="global">If set to <c>true</c> global.</param>
	public void genericTrapsActions(bool global = false){
		if(global)
			this.globalSound.Play();
		else
			this.audio.Play();

		this.isEnable = false;
	}
}
