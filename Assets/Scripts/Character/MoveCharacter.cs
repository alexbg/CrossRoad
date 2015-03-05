using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using CrossRoad.Principal;

namespace CrossRoad.Character{

	public class MoveCharacter : MonoBehaviour {

		//public float acceleration;
		public float force;//
		private float maxWalk;//
		private float maxRun;//
		private float maxWalkTired;//
		private float forceJump;//
		private bool isDead; //
		private bool isRunning; //
		private bool isWalking; //

		// Canvas del menu principal
		public Canvas menu;
		// Texto que muestra que has ganado
		public Text winnerText;//
		public GameObject loserText;//
		public MouseLook mouseCharacter;//
		public MouseLook mouseHead;//
		public Animator lightController;//
		public Slider energy;//

		private int fieldOfView;

		private bool canJump;//
		private float time;//
		private bool isTired;//
		private float maxVelocity;//
		private RaycastHit info; //
		public AudioSource headSound;//

		// Controllers
		// Guarda el nombre de los controllers
		private string[] controllers;
		// El nombre del jugador para los controllers
		// Esta: principal, player1, player2
		public string player;

		public ConfigControllers configControllers;

		// Use this for initialization
		void Start () {
			Cursor.visible = false;
			this.fieldOfView = 60;
			this.maxWalk = 3;
			this.maxRun = 5;
			this.maxWalkTired = 2;
			this.force = 15f;
			this.forceJump = 6f;
			this.isDead = false;
			this.isRunning = false;
			this.isWalking = false;
			// Obtengo los controles de este jugador
			this.controllers = configControllers.getControllers(this.player);
			Debug.Log (this.controllers);
		}
		
		// Update is called once per frame
		void Update () {
			Debug.DrawRay(this.transform.position, -this.transform.up * 1.1f, Color.red);
			this.time += Time.deltaTime;

			// Pone al personaje cansado
			if(this.energy.value == 0){
				this.isTired = true;
				this.lightController.SetBool("isRunning",false);
				//if(!this.headSound.isPlaying)
				this.headSound.Play();
			}

			// Quita el cansancion al personaje
			if(this.isTired && this.energy.value == 100){
				this.isTired = false;
				this.lightController.SetBool("isTired",false);
				this.headSound.Stop();
				this.isWalking = true;
			}

			// controla la barra de energia
			// Si se gasta, esta corriendo
			// En caso contrario esta caminando
			if((Input.GetButton(this.controllers[3]) || Input.GetAxis(this.controllers[3]) > 0 ) && !this.isTired && !this.isDead){
				this.energy.value -= 20 * Time.deltaTime;
				this.isRunning = true;
			}else{
				this.energy.value += 10 * Time.deltaTime;
				this.isWalking = true;
			}

			// Controla que el personaje pueda saltar si esta tocando el suelo
			// Lanza un raycast en direccion negativa de la y. Si el raycast toca algo, es que el personaje esta en el suelo
			// Decide si puede saltar o no
			if(Physics.Raycast(this.transform.position,-transform.up,1.1f)){
				//Debug.Log("Puedes saltar");
				//if(!this.canJump){
					this.canJump = true;
					// Para que no haya problemas con la velocidad, se pone a 0 la velocidad de caida cuando
					// el raycast toca el objeto con el que choca
					this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x,0,this.GetComponent<Rigidbody>().velocity.z);
				//}
			}else{
				this.canJump = false;
			}

			// Control de velocidad y de la fieldOfView de la camara
			// El maximo fieldOfView es 60, se cambia en la variable fieldOfView
			// CANSADO
			if(this.isTired){
				this.maxVelocity = this.maxWalkTired;
				this.lightController.SetBool("isTired",true);
				if(Camera.main.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					Camera.main.fieldOfView += 10 * Time.deltaTime;
				this.GetComponent<AudioSource>().pitch = 0.9f;
				this.changeStateOfMove(2);
			// CORRIENDO
			}else if(this.isRunning){
				//this.isRunning = true;
				if(this.isRunning){
					this.maxVelocity = this.maxRun;
					// Controla que no se inicie la animacion cuando este saltando
					if(this.canJump)
						this.lightController.SetBool("isRunning",true);
					if(Camera.main.fieldOfView > 55)
						// Reduce el field -10 cada segundo
						Camera.main.fieldOfView -= 10 * Time.deltaTime;

					this.GetComponent<AudioSource>().pitch = 1.3f;
					this.changeStateOfMove(1);
				}
			// CAMINANDO
			}else if(this.isWalking){

				this.maxVelocity = this.maxWalk;
				this.lightController.SetBool("isRunning",false);
				if(Camera.main.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					Camera.main.fieldOfView += 10 * Time.deltaTime;
				this.GetComponent<AudioSource>().pitch = 1.0f;
				this.changeStateOfMove(0);

			}

			// si se pone en FixedUpdate le da mucho mas impulso, por las repeticiones de la propia funcion
			// que no va segun los frames. De esta forma solo le impulsa una vez
			if(this.energy.value >=20){
				if((Input.GetButton (this.controllers[2]) || Input.GetAxis(this.controllers[2]) > 0) && this.canJump && !this.isTired){
					this.GetComponent<Rigidbody>().AddForce(transform.up * this.forceJump,ForceMode.Impulse);
					this.energy.value -= 20;
					this.canJump = false;
					// Apaga la animacion de la linterna cuando esta saltando
					this.lightController.SetBool("isRunning",false);
				}
			}

			// Input.GetAxis("Vertical") y Input.GetAxis("Horizontal") se utilizan tanto con el joystick como con el teclado
			if((Input.GetAxis(this.controllers[1]) != 0 || Input.GetAxis(this.controllers[0]) != 0) && this.canJump){
				if(!this.GetComponent<AudioSource>().isPlaying)
					this.GetComponent<AudioSource>().Play();
			}else{
				this.GetComponent<AudioSource>().Stop();
			}
			//Debug.Log (this.GetComponent<Rigidbody>().velocity.magnitude);
		}

		void FixedUpdate(){

			// Movimiento vertical la z y horizontal la x
			if(this.canJump){
				this.GetComponent<Rigidbody>().AddForce(transform.forward * this.force * Input.GetAxis(this.controllers[1]),ForceMode.Acceleration);
				this.GetComponent<Rigidbody>().AddForce(transform.right * this.force * Input.GetAxis(this.controllers[0]),ForceMode.Acceleration);
			}

			// Controla la velocidad
			if(this.GetComponent<Rigidbody>().velocity.magnitude > this.maxVelocity && this.canJump){
				this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * this.maxVelocity;
			}

		}
		// COLISIONES

		// Comprobar si es mejor que el personaje active las trampas y las fisicas
		// Es collisionEnter y no trigger porque la bola tiene un rigidbody, asiq niega el rigidbody del padre
		// y utiliza el suyo propio
		void OnCollisionEnter(Collision info) {
			//Debug.Log (info.collider.name);
			//Debug.Log ("Ha colisionado con el personaje No trigger:" + info.collider.tag);
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

		/*void OnCollisionExit(Collision info){
			Debug.Log ("Ha salido de la colision");
			this.canJump = false;
		}*/

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
			this.headSound.Stop();
			this.isDead = true;
		}


		// Termina la partida
		/// <summary>
		/// Ends the game.
		/// </summary>
		public void endGame(){
			Debug.Log ("Ha entrado");
			this.winnerText.enabled = true;
			this.menu.enabled = true;
			Time.timeScale = 0;
			this.mouseHead.enabled = false;
			this.mouseCharacter.enabled = false;
			Cursor.visible = true;
			this.headSound.Stop();
		}


		/// <summary>
		/// Changes the state of move.
		/// Evito que este poniendo los valores de velocidad, sonido, vista de la camara, etc en cada frame
		/// 0: walking
		/// 1: running
		/// 2: tired;
		/// </summary>
		/// <param name="state">State.</param>
		private void changeStateOfMove(byte state){
			// this.isTired no se pone a false porque debe de  ser el estado principal cuando esta cansado
			// Por lo que no cambia segun se pulse un boton o otro, por lo que no se puede cambiar como el 
			// this.isRunning o this.isWalking que dependen de pulsar uno o varios botones para cambiar
			this.isRunning = false;
			this.isWalking = false;
			switch(state){
				case 0:
					//this.isRunning = false;
					//this.isTired = false;
					Camera.main.GetComponent<CameraMotionBlur>().enabled = false;
					Camera.main.GetComponent<DepthOfField>().enabled = false;
					break;
				case 1:
					//this.isRunning = true;
					//this.isTired = false;
					Camera.main.GetComponent<CameraMotionBlur>().enabled = true;
					Camera.main.GetComponent<DepthOfField>().enabled = false;
					break;
				case 2:
					//this.isRunning = false;
					//this.isTired = true;
					Camera.main.GetComponent<CameraMotionBlur>().enabled = false;
					Camera.main.GetComponent<DepthOfField>().enabled = true;
					break;
			}
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
