using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoveCharacter : MonoBehaviour {

	public float acceleration;
	public float maxWalk;
	public float maxRun;
	public float maxWalkTired;
	public int maxHeight;
	// Canvas del menu principal
	public Canvas menu;
	// Texto que muestra que has ganado
	public Text winnerText;
	public GameObject loserText;
	public MouseLook mouseCharacter;
	public MouseLook mouseHead;

	public Slider energy;
	//public Transform ignoreCollision;
	private float y;
	private float x;
	private bool canJump;
	private float time;
	private bool tired;
	private float maxVelocity;
	private RaycastHit info;
	private bool moving;
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;

		if(this.acceleration == 0)
			this.acceleration = 10;

		if(this.maxWalk == 0)
			this.maxWalk = 3;

		if(this.maxRun == 0)
			this.maxRun = 5;

		if(this.maxWalkTired == 0)
			this.maxWalkTired = 2;

		this.canJump = false;
		this.tired = false;
		//Physics.IgnoreCollision (this.ignoreCollision.collider, this.collider);
	}
	
	// Update is called once per frame
	void Update () {
		this.moving = false;
		//Debug.Log (this.rigidbody.velocity.magnitude);
		if(Input.GetAxis("Vertical") > 0){
			this.moving = true;
			this.y = 1;
		}else{
			this.moving = true;
			this.y = -1;
		}

		if(Input.GetAxis("Horizontal") > 0){
			this.x = 1;
		}else{
			this.x = -1;
		}
		/*if(Input.GetButtonDown("Cancel")){
			this.pause(false);
		}*/

		this.time += Time.deltaTime;

		// controla la barra de energia
		if(Input.GetButton("Run") && !this.tired && this.moving){
			this.energy.value -= 20 * Time.deltaTime;
		}else{
			this.energy.value += 10 * Time.deltaTime;
		}

		// comprueba si se cansa el jugadore
		if(this.energy.value == 0){
			this.tired = true;
		}else if(this.energy.value == 100){
			this.tired = false;
		}

		if((Input.GetButton("Vertical") ||  Input.GetButton("Horizontal")) && this.canJump){
			if(!this.audio.isPlaying){
				this.audio.Play();
			}

		}else{
			this.audio.Stop();

		}
		// Permite saber si puedes saltar o no

		if(Physics.Raycast(this.transform.position,-transform.up,1.0f) && !this.canJump){
			if(!this.canJump){
				this.canJump = true;
				this.rigidbody.drag = 2;
			}
		}
		Debug.Log (this.rigidbody.velocity.magnitude);
	}

	void FixedUpdate(){

		// Controla por prioridad
		if(this.tired){
			this.maxVelocity = this.maxWalkTired;
			this.audio.pitch = 0.9f;
		}
		else if(Input.GetButton("Run")){
			this.maxVelocity = this.maxRun;
			this.audio.pitch = 1.5f;
		}
		else{
			this.maxVelocity = this.maxWalk;
			this.audio.pitch = 1.0f;
		}

		if(Input.GetButton("Vertical") && this.canJump){
			this.rigidbody.AddForce(transform.forward * this.acceleration * this.y,ForceMode.Force);

		}

		if(Input.GetButton("Horizontal") && this.canJump){
			this.rigidbody.AddForce(transform.right * this.acceleration * this.x,ForceMode.Force);

		}
		// Control de velocidad

		if(this.rigidbody.velocity.magnitude > this.maxVelocity && this.canJump){

			this.rigidbody.velocity = this.rigidbody.velocity.normalized * this.maxVelocity;

		}

		// controla el salto
		if(this.canJump && Input.GetButton("Jump") && !this.tired){
			this.canJump = false;
			this.rigidbody.AddForce(new Vector3(this.rigidbody.velocity.x/2,(this.maxHeight * 1),this.rigidbody.velocity.z/2),ForceMode.Impulse);
			this.rigidbody.drag = 1;
			
			//Debug.Log("salta");

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

	/*void OnCollisionStay(Collision info) {
		//this.canJump = true;
	}*/

	/*public void back(){
		this.menu.enabled = false;
		//this.toggleMouse();
	}*/

	/// <summary>
	/// Toggles the mouse.
	/// </summary>
	/*public void toggleMouse(){
		if(this.mouseHead.enabled)
			this.mouseHead.enabled = false;
		else
			this.mouseHead.enabled = true;

		if(this.mouseCharacter.enabled)
			this.mouseCharacter.enabled = false;
		else
			this.mouseCharacter.enabled = true;
	}*/
	// Pausa el juego
	/*public void pause(bool end){
		if(end)
			this.winnerText.enabled = true;

		if(this.menu.enabled)
			this.menu.enabled = false;
		else
			this.menu.enabled = true;

		this.toggleMouse();

		if(Time.timeScale == 1)
			Time.timeScale = 0;
		else
			Time.timeScale = 1.0f;

		if(Screen.showCursor)
			Screen.showCursor = false;
		else
			Screen.showCursor = true;
	}*/

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
