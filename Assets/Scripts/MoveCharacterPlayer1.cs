using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrossRoad.Audio.Player1;

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
		//private bool moving;
		// Use this for initialization
		void Start () {
			Screen.showCursor = false;
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
				EmitSoundsMultiplayer.emitSound(2);
			}
			
			// Quita el cansancion al personaje
			if(this.isTired && this.energy.value == 100){
				this.isTired = false;
				this.lightController.SetBool("isTired",false);
				//this.headSound.Stop();
				EmitSoundsMultiplayer.stopSound(2);
			}
			
			// controla la barra de energia
			if((Input.GetButton("RunPlayer1") || Input.GetAxis("RunPlayer1") > 0 )&& !this.isTired){
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
					this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x,0,this.rigidbody.velocity.z);
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
				EmitSoundsMultiplayer.changePicth(0,0.9f);
				
			}else if(Input.GetButton("RunPlayer1") || Input.GetAxis("RunPlayer1") > 0){
				this.maxVelocity = this.maxRun;
				// Controla que no se inicie la animacion cuando este saltando
				if(this.canJump)
					this.lightController.SetBool("isRunning",true);
				if(this.camera.fieldOfView > 55)
					// Reduce el field -10 cada segundo
					this.camera.fieldOfView -= 10 * Time.deltaTime;
				//this.audio.pitch = 1.3f;
				EmitSoundsMultiplayer.changePicth(0,1.3f);
			}else{
				this.maxVelocity = this.maxWalk;
				this.lightController.SetBool("isRunning",false);
				if(this.camera.fieldOfView < this.fieldOfView)
					// Aumenta el field -10 cada segundo
					this.camera.fieldOfView += 10 * Time.deltaTime;
				//this.audio.pitch = 1.0f;
				EmitSoundsMultiplayer.changePicth(0,1.0f);
			}
			
			// si se pone en FixedUpdate le da mucho mas impulso, por las repeticiones de la propia funcion
			// que no va segun los frames. De esta forma solo le impulsa una vez
			if(this.energy.value >=20){
				if((Input.GetButton ("JumpPlayer1") || Input.GetAxis("JumpPlayer1") > 0) && this.canJump && !this.isTired){
					this.rigidbody.AddForce(transform.up * this.forceJump,ForceMode.Impulse);
					this.energy.value -= 20;
					this.canJump = false;
					// Apaga la animacion de la linterna cuando esta saltando
					this.lightController.SetBool("isRunning",false);
				}
			}
			// MODIFICADO PARA MULTIPLAYER
			if((Input.GetAxis("VerticalPlayer1") != 0|| Input.GetAxis("HorizontalPlayer1") != 0) && this.canJump){
				if(!EmitSoundsMultiplayer.isPlaying(0)){
					EmitSoundsMultiplayer.emitSound(0);
				}
			}else{
				EmitSoundsMultiplayer.stopSound(0);
			}

			//Debug.Log (Input.GetAxis ("VerticalPlayer1"));

		}
		
		void FixedUpdate(){

			// Movimiento vertical la z
			if(/*Input.GetButton("Vertical") && */this.canJump){
				this.rigidbody.AddForce(transform.forward * this.force * Input.GetAxis("VerticalPlayer1"),ForceMode.Acceleration);
				this.rigidbody.AddForce(transform.right * this.force * Input.GetAxis("HorizontalPlayer1"),ForceMode.Acceleration);
			}
			
			// Movimiento horizontal la x
			if(/*Input.GetButton("Horizontal") && */this.canJump){
				//this.rigidbody.AddForce(transform.right * this.force * Input.GetAxis("HorizontalPlayer1"),ForceMode.Acceleration);
			}
			
			// Controla la velocidad
			if(this.rigidbody.velocity.magnitude > this.maxVelocity && this.canJump){
				this.rigidbody.velocity = this.rigidbody.velocity.normalized * this.maxVelocity;
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
				this.rigidbody.freezeRotation = false;
				/*this.loserText.SetActive(true);
					this.headSound.Stop();*/
				this.genericActionsCollision();
				break;
				
			case "Tree":
				this.rigidbody.freezeRotation = false;
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
			switch(info.collider.tag){
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
				this.rigidbody.freezeRotation = false;
				//this.loserText.SetActive(true);
				this.rigidbody.isKinematic = true;
				//this.headSound.Stop();
				this.genericActionsCollision();
				break;
			}
			
		}
		
		/// <summary>
		/// Acciones genericas en las colisiones con el personaje
		/// </summary>
		public void genericActionsCollision(){
			this.loserText.SetActive(true);
			EmitSoundsMultiplayer.stopSound(2);
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
			Screen.showCursor = true;
			EmitSoundsMultiplayer.stopSound(2);
		}
		
		
		// EVENTOS PREDEFINIDOS
		
		/*void OnLevelWasLoaded(int level) {
			Time.timeScale = 1.0f;
		}*/
		
		void OnDisable() {
			this.audio.Stop();
		}
	}
}