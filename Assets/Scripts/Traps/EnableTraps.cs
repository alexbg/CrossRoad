using UnityEngine;
using System.Collections;
using CrossRoad.Audio;
//using CrossRoad.Audio.Player2;

public class EnableTraps : MonoBehaviour {

	// This is the gameobject of trap;
	public GameObject trap;
	// If this is true then the trap will be enabled when the character enter in collision
	private bool isEnable;
	public AudioSource globalSound;
	public EmitSoundsMultiplayer soundMultiplayer;

	// Use this for initialization
	void Start () {
		//this.isEnable = true;
		if(Random.value > 0.4){
			this.isEnable = true;
			this.GetComponent<Collider>().enabled = true;
			this.GetComponent<Renderer>().enabled = true;
		}
		// Obtiene el EmitSoundsMultiplayer que esta en el objeto AudioListener
		// que es el que se encarga ed los sonidos en multiplayer
		if(PositionCharacter.multiplayer)
			this.soundMultiplayer = GameObject.Find ("AudioListener").GetComponent<EmitSoundsMultiplayer> ();

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
		if (this.isEnable && (info.tag == "Player" || info.tag == "Player2")) {
			switch(this.trap.tag){
				case "Ball":
					trap.GetComponent<Rigidbody>().isKinematic = false;
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,4);

					break;

				case "Trampoline":
					info.GetComponent<Rigidbody>().freezeRotation = false;
					info.GetComponent<Rigidbody>().AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);
					
					if(!PositionCharacter.multiplayer)
						this.genericTrapsActions();
					else
						this.genericTrapsActionsMultiplayer(tag,1);
					
					break;

				case "Tree":
					Debug.Log ("Ha entrado en el Tree");
					//trap.rigidbody.isKinematic = false; // No funciona bien 
					trap.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
						if(hitColliders[i].GetComponent<Rigidbody>()){
							hitColliders[i].GetComponent<Rigidbody>().freezeRotation = false;
							hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(40.0f,transform.position,10.0f,5.0f,ForceMode.Impulse);	
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
				this.GetComponent<AudioSource>().Play();
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
	public void genericTrapsActionsMultiplayer(string tag, byte sound){


		Debug.Log("Sonido");
		if(tag == "Player"){
			soundMultiplayer.emitSound(sound,1);
		}

		if(tag == "Player2"){
			soundMultiplayer.emitSound(sound,2);
		}
		// No los destruyo porque si no, no se emite el sonido	
		this.isEnable = false;

	}
}
