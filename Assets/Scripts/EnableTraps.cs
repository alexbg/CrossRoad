using UnityEngine;
using System.Collections;

public class EnableTraps : MonoBehaviour {

	// This is the gameobject of trap;
	public GameObject trap;
	// If this is true then the trap will be enabled when the character enter in collision
	private bool isEnable;

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
		if (this.isEnable) {
			switch(this.trap.tag){
				case "Ball":
					trap.rigidbody.isKinematic = false;
					this.isEnable = false;
					this.audio.Play();
					break;

				case "Trampoline":
					info.rigidbody.freezeRotation = false;
					info.rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
					this.isEnable = false;
					this.audio.Play();
					break;

				case "Tree":
					Debug.Log ("Ha entrado en el Tree");
					//trap.rigidbody.isKinematic = false; // No funciona bien 
					trap.rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
					this.isEnable = false;
					this.audio.Play();
					break;

				case "Explosion":
					Debug.Log("Ha explotado");
					Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5F);
					int i = 0;
					while (i < hitColliders.Length) {
						if(hitColliders[i].rigidbody){
							hitColliders[i].rigidbody.freezeRotation = false;
							hitColliders[i].rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);	
						}
						i++;
					}
					this.isEnable = false;
					this.audio.Play();
					break;
			}
		}
	}

	/*public void fireTrap(Collision info){
		Debug.Log ("Colision con:" + this.trap.tag);
		if (this.isEnable) {
			switch(this.trap.tag){
			case "Ball":
				trap.rigidbody.isKinematic = false;
				this.isEnable = false;
				this.audio.Play();
				break;
				
			case "Trampoline":
				info.rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
				this.isEnable = false;
				this.audio.Play();
				break;
				
			case "Tree":
				Debug.Log ("Ha entrado en el Tree");
				//trap.rigidbody.isKinematic = false; // No funciona bien 
				trap.rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
				this.isEnable = false;
				this.audio.Play();
				break;
				
			case "Explosion":
				Debug.Log("Ha explotado");
				Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5F);
				int i = 0;
				while (i < hitColliders.Length) {
					if(hitColliders[i].rigidbody){
						hitColliders[i].rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
						hitColliders[i].rigidbody.freezeRotation = false;	
					}
					i++;
				}
				this.isEnable = false;
				this.audio.Play();
				break;
			}
		}
	}*/
}
