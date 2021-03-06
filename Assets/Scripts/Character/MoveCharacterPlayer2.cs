﻿using UnityEngine;
using UnityEngine.UI;
using CrossRoad.Audio;

namespace CrossRoad.Character{
	/// <summary>
	/// Move character player2.
	/// Tanto esta clase como la otra del movimiento apra jugador 1, deberan de estar unificadas
	/// </summary>
	public class MoveCharacterPlayer2 : MonoBehaviour {

		public float acceleration;//
		public float force;//
		private float maxWalk;//
		private float maxRun;//
		private float maxWalkTired;//
		private float forceJump;//
		//public int maxHeight;
		// Canvas del menu principal
		public Canvas menu;
		// Texto que muestra que has ganado
		public Text winnerText;//
		public GameObject loserText;//
		public MouseLookYoystick mouseCharacter;//
		public MouseLookYoystick mouseHead;//
		public Animator lightController;//
		public Slider energy;//
		//public Transform ignoreCollision;
		private int fieldOfView;
		//private float y;
		//private float x;
		private bool canJump;//
		private float time;//
		private bool isTired;//
		private float maxVelocity;//
		private RaycastHit info;
		public AudioSource headSound;
		public Camera camera;
		public EmitSoundsMultiplayer soundMultiplayer;
		//private bool moving;
		// Use this for initialization
		void Start () {
			Cursor.visible = false;
			this.fieldOfView = 60;
			this.maxWalk = 3;
			this.maxRun = 5;
			this.maxWalkTired = 2;
			this.force = 15f;
			this.forceJump = 6f;
			
			
			//this.animation.Stop ();
			//Physics.IgnoreCollision (this.ignoreCollision.collider, this.collider);
		}
		
		// Update is called once per frame
		void Update () {
			this.time += Time.deltaTime;
			// Pone al personaje cansado
			if(this.energy.value == 0){
				this.isTired = true;
				this.lightController.SetBool("isRunning",false);
				//if(!this.headSound.isPlaying)

				soundMultiplayer.emitSound(2,2);
			}
			
			// Quita el cansancion al personaje
			if(this.isTired && this.energy.value == 100){
				this.isTired = false;
				this.lightController.SetBool("isTired",false);
				soundMultiplayer.stopSound(2,2);

			}
			
			// controla la barra de energia
			if(Input.GetAxis("RunPlayer2") > 0 && !this.isTired){
				this.energy.value -= 20 * Time.deltaTime;
			}else{
				this.energy.value += 10 * Time.deltaTime;
			}
			
			// Controla que el personaje pueda saltar si esta tocando el suelo
			// Lanza un raycast en direccion negativa de la y. Si el raycast toca algo, es que el personaje esta en el suelo
			if(Physics.Raycast(this.transform.position,-transform.up,1.0f) && !this.canJump){
				if(!this.canJump){
					this.canJump = true;
					// Para que no haya problemas con la velocidad, se pone a 0 la velocidad de caida cuando
					// el raycast toca el objeto con el que choca
					this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x,0,this.GetComponent<Rigidbody>().velocity.z);
				}
			}
			
			// Control de velocidad y de la fieldOfView de la camara
			// El maximo fieldOfView es 60, se cambia en la variable fieldOfView
			if(this.isTired){
				this.maxVelocity = this.maxWalkTired;
				this.lightController.SetBool("isTired",true);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				//this.audio.pitch = 0.9f;
				soundMultiplayer.changePicth(0,0.9f,2);
				
			}else if(Input.GetAxis("RunPlayer2") > 0){
				this.maxVelocity = this.maxRun;
				// Controla que no se inicie la animacion cuando este saltando
				if(this.canJump)
					this.lightController.SetBool("isRunning",true);
				if(this.camera.fieldOfView > 55)
					// Reduce el field -10 cada segundo
					this.camera.fieldOfView -= 10 * Time.deltaTime;
				//this.audio.pitch = 1.3f;
				soundMultiplayer.changePicth(0,1.3f,2);
			}else{
				this.maxVelocity = this.maxWalk;
				this.lightController.SetBool("isRunning",false);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				//this.audio.pitch = 1.0f;
				soundMultiplayer.changePicth(0,1f,2);
			}
			
			// si se pone en FixedUpdate le da mucho mas impulso, por las repeticiones de la propia funcion
			// que no va segun los frames. De esta forma solo le impulsa una vez
			if(this.energy.value >=20){
				if(Input.GetAxis("JumpPlayer2") > 0 && this.canJump && !this.isTired){
					this.GetComponent<Rigidbody>().AddForce(transform.up * this.forceJump,ForceMode.Impulse);
					this.energy.value -= 20;
					this.canJump = false;
					// Apaga la animacion de la linterna cuando esta saltando
					this.lightController.SetBool("isRunning",false);
				}
			}
			
			// MODIFICADO PARA MULTIPLAYER
			if((Input.GetAxis("VerticalPlayer2") != 0 || Input.GetAxis("HorizontalPlayer2") != 0) && this.canJump){
				if(!soundMultiplayer.isPlaying(0,2)){
					soundMultiplayer.emitSound(0,2);
				}
			}else{
				soundMultiplayer.stopSound(0,2);
			}
			
		}
		
		void FixedUpdate(){
			
			// Movimiento vertical la z
			if(/*Input.GetButton("Vertical") && */this.canJump){
				this.GetComponent<Rigidbody>().AddForce(transform.forward * this.force * Input.GetAxis("VerticalPlayer2"),ForceMode.Acceleration);
				this.GetComponent<Rigidbody>().AddForce(transform.right * this.force * Input.GetAxis("HorizontalPlayer2"),ForceMode.Acceleration);
			}
			
			// Movimiento horizontal la x
			if(/*Input.GetButton("Horizontal") && */this.canJump){
				//this.rigidbody.AddForce(transform.right * this.force * Input.GetAxis("HorizontalPlayer2"),ForceMode.Acceleration);
			}
			
			// Controla la velocidad
			if(this.GetComponent<Rigidbody>().velocity.magnitude > this.maxVelocity && this.canJump){
				this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * this.maxVelocity;
			}
			
			
			//Debug.Log (this.rigidbody.velocity.magnitude);
			//this.rigidbody.ve
		}
		// COLISIONES
		
		// Comprobar si es mejor que el personaje active las trampas y las fisicas
		// Es collisionEnter y no trigger porque la bola tiene un rigidbody, asiq niega el rigidbody del padre
		// y utiliza el suyo propio
		void OnCollisionEnter(Collision info) {
			Debug.Log (info.collider.name);
			Debug.Log ("Ha colisionado con el personaje No trigger:" + info.collider.tag);
			switch(info.collider.tag){
			case "Ball":
				this.GetComponent<Rigidbody>().freezeRotation = false;
				/*this.loserText.SetActive(true);
					this.headSound.Stop();*/
				this.genericActionsCollision();
				break;
				
			case "Tree":
				this.GetComponent<Rigidbody>().freezeRotation = false;
				/*this.loserText.SetActive(true);
					this.headSound.Stop();*/
				this.genericActionsCollision();
				break;
			}
		}
		
		void OnCollisionExit(Collision info){
			this.canJump = false;
		}
		
		void OnTriggerEnter(Collider info){
			Debug.Log ("Ha colisionado con el personaje Trigger:" + info.transform.tag);
			switch(info.GetComponent<Collider>().tag){
				// Trampoline y explosion no modifican la freezeRotation, porque ya lo hace la trampa
				// Ya que son "explosiones" y lo modifican antes de afectarle
			case "Trampoline":
				/*this.loserText.SetActive(true);
				this.headSound.Stop();*/
				this.genericActionsCollision();
				break;
			case "Explosion":
				/*this.loserText.SetActive(true);
				this.headSound.Stop();*/
				this.genericActionsCollision();
				break;
			case "Spike":
				this.GetComponent<Rigidbody>().freezeRotation = false;
				//this.loserText.SetActive(true);
				this.GetComponent<Rigidbody>().isKinematic = true;
				//this.headSound.Stop();
				this.genericActionsCollision();
				break;
			case "Monster":
				this.GetComponent<Rigidbody>().freezeRotation = false;
				// Lo impulsa para que se caiga cuando le golpea el monstruo
				this.GetComponent<Rigidbody>().AddForce(transform.forward,ForceMode.Impulse);
				this.genericActionsCollision();
				break;
			}
			
		}
		
		/// <summary>
		/// Acciones genericas en las colisiones con el personaje
		/// </summary>
		public void genericActionsCollision(){
			this.loserText.SetActive(true);
			soundMultiplayer.stopSound(2,2);
		}
		
		
		// Termina la partida
		/// <summary>
		/// Ends the game.
		/// </summary>
		public void endGame(){
			this.winnerText.enabled = true;
			this.menu.enabled = true;
			Time.timeScale = 0;
			this.mouseHead.enabled = false;
			this.mouseCharacter.enabled = false;
			Cursor.visible = true;
			soundMultiplayer.stopSound(2,2);
		}
		
		
		// EVENTOS PREDEFINIDOS
		
		/*void OnLevelWasLoaded(int level) {
			Time.timeScale = 1.0f;
		}*/
		
		void OnDisable() {
			this.GetComponent<AudioSource>().Stop();
		}
	}
}