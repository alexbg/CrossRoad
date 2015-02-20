using UnityEngine;
using System.Collections;
using CrossRoad.Audio.Player1;
using CrossRoad.Audio.Player2;

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
		string tag = info.tag;
		if (this.isEnable && info.tag == "Player" || info.tag == "Player2") {
			switch(this.trap.tag){
				case "Ball":
					trap.rigidbody.isKinematic = false;
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,4);

					break;

				case "Trampoline":
					info.rigidbody.freezeRotation = false;
					info.rigidbody.AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
					
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,1);
					
					break;

				case "Tree":
					Debug.Log ("Ha entrado en el Tree");
					//trap.rigidbody.isKinematic = false; // No funciona bien 
					trap.rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,4);
					
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
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,3);
					break;

				case "SpikeGround":
					
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions(true);
					else
						this.genericTrapsActionsMultiplayer(tag,4);
					// De esta forma el suelo desaparece y cae a los pinchos
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
		// No los destruyo porque si no, no se emite el sonido	
		this.isEnable = false;

	}

	/// <summary>
	/// Generics the traps actions multiplayer.
	/// Se utiliza para emitir los sonidos en multijugador
	/// Se separan por jugadores, para que puedan sonar por separado
	/// Player el sonido sonara por el altavoz izquierdo
	/// Player2 el sonido sonara por el altavoz derecho
	/// </summary>
	/// <param name="tag">Tag.</param>
	/// <param name="sound">Sound.</param>
	public void genericTrapsActionsMultiplayer(string tag, int sound){

		if(tag == "Player"){
			EmitSoundsMultiplayer.emitSound(sound);
		}

		if(tag == "Player2"){
			EmitSoundMultiplayer2.emitSound(sound);
		}
		// No los destruyo porque si no, no se emite el sonido	
		this.isEnable = false;

	}
}
