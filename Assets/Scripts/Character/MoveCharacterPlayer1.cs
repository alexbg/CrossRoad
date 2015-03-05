using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrossRoad.Audio;
using UnityStandardAssets.ImageEffects;
using CrossRoad.Principal;

namespace CrossRoad.Character{

	/// <summary>
	/// Move character player1.
	/// Tanto esta clase como la otra del movimiento apra jugador 1, deberan de estar unificadas
	/// </summary>
	public class MoveCharacterPlayer1 : MonoBehaviour {

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
		private bool isRunning;
		private bool isWalking;
		//private bool moving;

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
			this.isWalking = false;
			this.isRunning = false;
			// Obtengo los controles de este jugador
			this.controllers = configControllers.getControllers(this.player);
			
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
				//EmitSoundsMultiplayer.emitSound(2,1);
				soundMultiplayer.emitSound(2,this.getPlayer(this.player));

			}
			
			// Quita el cansancion al personaje
			if(this.isTired && this.energy.value == 100){
				this.isTired = false;
				this.lightController.SetBool("isTired",false);
				//this.headSound.Stop();
				soundMultiplayer.stopSound(2,this.getPlayer(this.player));
				this.isWalking = true;
			}
			
			// controla la barra de energia
			if((Input.GetButton(this.controllers[3]) || Input.GetAxis(this.controllers[3]) > 0 )&& !this.isTired ){
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
				//this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x,0,this.GetComponent<Rigidbody>().velocity.z);
				//}
			}else{
				this.canJump = false;
			}
			
			// Control de velocidad y de la fieldOfView de la camara
			// El maximo fieldOfView es 60, se cambia en la variable fieldOfView
			/*if(this.isTired){
				this.maxVelocity = this.maxWalkTired;
				this.lightController.SetBool("isTired",true);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				//this.audio.pitch = 0.9f;
				soundMultiplayer.changePicth(0,0.9f,1);
				
			}else if(Input.GetButton("RunPlayer1") || Input.GetAxis("RunPlayer1") > 0){
				this.maxVelocity = this.maxRun;
				// Controla que no se inicie la animacion cuando este saltando
				if(this.canJump)
					this.lightController.SetBool("isRunning",true);
				if(this.camera.fieldOfView > 55)
					// Reduce el field -10 cada segundo
					this.camera.fieldOfView -= 10 * Time.deltaTime;
				//this.audio.pitch = 1.3f;
				soundMultiplayer.changePicth(0,1.3f,1);
			}else{
				this.maxVelocity = this.maxWalk;
				this.lightController.SetBool("isRunning",false);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				//this.audio.pitch = 1.0f;
				soundMultiplayer.changePicth(0,1.0f,1);
			}*/



			// Control de velocidad y de la fieldOfView de la camara
			// El maximo fieldOfView es 60, se cambia en la variable fieldOfView
			// CANSADO
			if(this.isTired){
				this.maxVelocity = this.maxWalkTired;
				this.lightController.SetBool("isTired",true);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				soundMultiplayer.changePicth(0,0.9f,this.getPlayer(this.player));
				this.changeStateOfMove(2);
				// CORRIENDO
			}else if(this.isRunning){
				//this.isRunning = true;
				if(this.isRunning){
					this.maxVelocity = this.maxRun;
					// Controla que no se inicie la animacion cuando este saltando
					if(this.canJump)
						this.lightController.SetBool("isRunning",true);
					if(this.camera.fieldOfView > 55)
						// Reduce el field -10 cada segundo
						this.camera.fieldOfView -= 10 * Time.deltaTime;
					
					soundMultiplayer.changePicth(0,1.3f,this.getPlayer(this.player));
					this.changeStateOfMove(1);
				}
				// CAMINANDO
			}else if(this.isWalking){
				
				this.maxVelocity = this.maxWalk;
				this.lightController.SetBool("isRunning",false);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				soundMultiplayer.changePicth(0,1.0f,this.getPlayer(this.player));
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
			// MODIFICADO PARA MULTIPLAYER
			if((Input.GetAxis(this.controllers[1]) != 0 || Input.GetAxis(this.controllers[0]) != 0) && this.canJump){
				if(!soundMultiplayer.isPlaying(0,this.getPlayer(this.player))){
					soundMultiplayer.emitSound(0,this.getPlayer(this.player));
				}
			}else{
				soundMultiplayer.stopSound(0,this.getPlayer(this.player));
			}

			//Debug.Log (Input.GetAxis ("VerticalPlayer1"));

		}
		
		void FixedUpdate(){

			// Movimiento vertical la z
			if(/*Input.GetButton("Vertical") && */this.canJump){
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
		
		/*void OnCollisionExit(Collision info){
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
			soundMultiplayer.stopSound(2,this.getPlayer(this.player));
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
				this.camera.GetComponent<CameraMotionBlur>().enabled = false;
				this.camera.GetComponent<DepthOfField>().enabled = false;
				break;
			case 1:
				//this.isRunning = true;
				//this.isTired = false;
				this.camera.GetComponent<CameraMotionBlur>().enabled = true;
				this.camera.GetComponent<DepthOfField>().enabled = false;
				break;
			case 2:
				//this.isRunning = false;
				//this.isTired = true;
				this.camera.GetComponent<CameraMotionBlur>().enabled = false;
				this.camera.GetComponent<DepthOfField>().enabled = true;
				break;
			}
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
			soundMultiplayer.stopSound(2,this.getPlayer(this.player));
		}

		/// <summary>
		/// indica que jugador esta jugando. Se utiliza para enviarle al audio el jugador
		/// </summary>
		/// <returns>The player.</returns>
		/// <param name="text">Text.</param>
		private byte getPlayer(string text){
			byte player = 1;
			switch(text){
			case "player1":
				player = 1;
				break;
			case "player2":
				player = 2;
				break;
			}

			return player;
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