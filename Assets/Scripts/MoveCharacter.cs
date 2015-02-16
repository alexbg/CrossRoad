﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoveCharacter : MonoBehaviour {

	public float acceleration;//
	public float force;//
	public float maxWalk;//
	public float maxRun;//
	public float maxWalkTired;//
	public float forceJump;
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

		this.maxWalk = 3;
		this.maxRun = 5;
		this.maxWalkTired = 2;
		this.force = 20.8f;
		this.forceJump = 20;
		//Physics.IgnoreCollision (this.ignoreCollision.collider, this.collider);
	}
	
	// Update is called once per frame
	void Update () {

		this.time += Time.deltaTime;

		// controla la barra de energia
		if(Input.GetButton("Run") && !this.tired && this.moving){
			this.energy.value -= 20 * Time.deltaTime;
		}else{
			this.energy.value += 10 * Time.deltaTime;
		}

		if(Physics.Raycast(this.transform.position,-transform.up,1.0f) && !this.canJump){
			if(!this.canJump){
				this.canJump = true;
			}
		}

		//this.canJump = false;
	}

	void FixedUpdate(){

		if(Input.GetButton("Vertical") && this.canJump){
			this.rigidbody.AddForce(transform.forward * this.force * Input.GetAxis("Vertical"),ForceMode.Acceleration);
		}

		if(Input.GetButton("Horizontal") && this.canJump){
			this.rigidbody.AddForce(transform.right * this.force * Input.GetAxis("Horizontal"),ForceMode.Acceleration);
		}

		if(Input.GetButton ("Jump") && this.canJump){
			this.rigidbody.AddForce(transform.up * this.forceJump,ForceMode.Impulse);
			this.canJump = false;
		}

		if(this.rigidbody.velocity.magnitude > this.maxRun){
			this.rigidbody.velocity = this.rigidbody.velocity.normalized * this.maxRun;
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
