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

		this.canJump = true;
		this.tired = false;
		//Physics.IgnoreCollision (this.ignoreCollision.collider, this.collider);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (this.rigidbody.velocity.magnitude);
		if(Input.GetAxis("Vertical") > 0)
			this.y = 1;
		else
			this.y = -1;

		if(Input.GetAxis("Horizontal") > 0)
			this.x = 1;
		else
			this.x = -1;

		if(Input.GetButtonDown("Cancel")){
			this.pause(false);
		}

		this.time += Time.deltaTime;

		// controla la barra de energia
		if(Input.GetButton("Run") && !this.tired){
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
		// Test rayCast

		if(Physics.Raycast(this.transform.position,-transform.up,1.0f) && !this.canJump){
			if(!this.canJump){
				this.canJump = true;
				this.rigidbody.drag = 2;
			}
		}
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

	// Se usan ambos porque los muros tienen el rigidbody en kinematic
	// los kinematic detectan colisiones con los objetos que son trigger, ya que no les
	// deberian de afectar las fisicas. Los botones de las trampas son triggers
	// Esto se hace para que el conjunto(los muros las trampas) sean kinematic y poder desactivarlo
	// para que se caiga por la gravedad
	void OnCollisionEnter(Collision info) {
		//this.canJump = true;
		/*if(info.gameObject.tag == "Ground" || info.gameObject.tag == "Trap"){
			this.canJump = true;
			this.rigidbody.drag = 2;
		}*/

		switch(info.gameObject.tag){
			case "Ball":
				this.rigidbody.freezeRotation = false;
				break;
			case "Explosion":
				this.rigidbody.freezeRotation = false;
				break;
			case "Trampoline":
				this.rigidbody.freezeRotation = false;
				break;
			case "Tree":
				this.rigidbody.freezeRotation = false;
				break;
		}

	}

	void OnTriggerEnter(Collider info){
		
		//this.canJump = true;
		/*if(info.gameObject.tag == "Ground" || info.gameObject.tag == "Trap"){
			this.canJump = true;
			this.rigidbody.drag = 2;
		}*/

		switch(info.gameObject.tag){
		case "Ball":
			this.rigidbody.freezeRotation = false;
			break;
		case "Explosion":
			this.rigidbody.freezeRotation = false;
			break;
		case "Trampoline":
			this.rigidbody.freezeRotation = false;
			break;
		case "Tree":
			this.rigidbody.freezeRotation = false;
			break;
		}
	}

	void OnCollisionStay(Collision info) {
		//this.canJump = true;
	}

	public void back(){
		this.menu.enabled = false;
		this.toggleMouse();
	}

	public void toggleMouse(){
		if(this.mouseHead.enabled)
			this.mouseHead.enabled = false;
		else
			this.mouseHead.enabled = true;

		if(this.mouseCharacter.enabled)
			this.mouseCharacter.enabled = false;
		else
			this.mouseCharacter.enabled = true;
	}
	// Pausa el juego
	public void pause(bool end){
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
	}

	// Termina la partida
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
}
