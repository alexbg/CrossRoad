using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoveCharacter : MonoBehaviour {

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
	public MouseLook mouseCharacter;//
	public MouseLook mouseHead;//
	public Animator lightController;
	public Slider energy;
	//public Transform ignoreCollision;
	private int fieldOfView;
	//private float y;
	//private float x;
	private bool canJump;//
	private float time;//
	private bool isTired;//
	private float maxVelocity;//
	private RaycastHit info;
	//private bool moving;
	// Use this for initialization
	void Start () {
		//Screen.showCursor = false;
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
		}

		// Quita el cansancion al personaje
		if(this.isTired && this.energy.value == 100){
			this.isTired = false;
			this.lightController.SetBool("isTired",false);
		}

		// controla la barra de energia
		if(Input.GetButton("Run") && !this.isTired){
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
			if(Camera.main.fieldOfView < this.fieldOfView)
				// Aumenta el field -10 cada segundo
				Camera.main.fieldOfView += 10 * Time.deltaTime;
			this.audio.pitch = 0.9f;

		}else if(Input.GetButton("Run")){
			this.maxVelocity = this.maxRun;
			// Controla que no se inicie la animacion cuando este saltando
			if(this.canJump)
				this.lightController.SetBool("isRunning",true);
			if(Camera.main.fieldOfView > 55)
				// Reduce el field -10 cada segundo
				Camera.main.fieldOfView -= 10 * Time.deltaTime;
			this.audio.pitch = 1.3f;

		}else{
			this.maxVelocity = this.maxWalk;
			this.lightController.SetBool("isRunning",false);
			if(Camera.main.fieldOfView < this.fieldOfView)
				// Aumenta el field -10 cada segundo
				Camera.main.fieldOfView += 10 * Time.deltaTime;
			this.audio.pitch = 1.0f;

		}

		// si se pone en FixedUpdate le da mucho mas impulso, por las repeticiones de la propia funcion
		// que no va segun los frames. De esta forma solo le impulsa una vez
		if(this.energy.value >=20){
			if(Input.GetButton ("Jump") && this.canJump && !this.isTired){
				this.rigidbody.AddForce(transform.up * this.forceJump,ForceMode.Impulse);
				this.energy.value -= 20;
				this.canJump = false;
				// Apaga la animacion de la linterna cuando esta saltando
				this.lightController.SetBool("isRunning",false);
			}
		}

		if((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && this.canJump){
			if(!this.audio.isPlaying)
				this.audio.Play();
		}else{
			this.audio.Stop();
		}

		//Debug.Log (Input.GetButton ("Jump"));
		//Debug.Log (this.rigidbody.velocity.magnitude);
		//Debug.Log (Camera.main.fieldOfView);
		//this.canJump = false;
	}

	void FixedUpdate(){

		// Movimiento vertical la z
		if(Input.GetButton("Vertical") && this.canJump){
			this.rigidbody.AddForce(transform.forward * this.force * Input.GetAxis("Vertical"),ForceMode.Acceleration);
		}

		// Movimiento horizontal la x
		if(Input.GetButton("Horizontal") && this.canJump){
			this.rigidbody.AddForce(transform.right * this.force * Input.GetAxis("Horizontal"),ForceMode.Acceleration);
		}

		// Controla la velocidad
		if(this.rigidbody.velocity.magnitude > this.maxVelocity && this.canJump){
			this.rigidbody.velocity = this.rigidbody.velocity.normalized * this.maxVelocity;
		}


		Debug.Log (this.rigidbody.velocity.magnitude);
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
				this.loserText.SetActive(true);
				//this.endGame();
				break;

			case "Tree":
				this.rigidbody.freezeRotation = false;
				this.loserText.SetActive(true);
				//this.endGame();
				break;
			/*case "Spike":
				this.rigidbody.freezeRotation = false;
				this.loserText.SetActive(true);
				break;*/
		}
	}

	void OnCollisionExit(Collision info){
		this.canJump = false;
	}

	void OnTriggerEnter(Collider info){
		Debug.Log ("Ha colisionado con el personaje Trigger:" + info.transform.tag);
		switch(info.collider.tag){
		case "Trampoline":
			this.loserText.SetActive(true);
			break;
		case "Explosion":
			this.loserText.SetActive(true);
			break;
		case "Spike":
			this.rigidbody.freezeRotation = false;
			this.loserText.SetActive(true);
			this.rigidbody.isKinematic = true;
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
		Screen.showCursor = true;
	}


	// EVENTOS PREDEFINIDOS

	/*void OnLevelWasLoaded(int level) {
		Time.timeScale = 1.0f;
	}*/

	void OnDisable() {
		this.audio.Stop();
	}
}
