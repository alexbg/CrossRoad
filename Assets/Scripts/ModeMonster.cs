using UnityEngine;
using System.Collections;
using CrossRoad.Audio.Player1;
using CrossRoad.Audio.Player2;

public class ModeMonster : MonoBehaviour {

	public byte velocity;
	/*private Quaternion left;
	private Quaternion right;
	private bool isTurned;*/
	public Animator animationMonster;


	private Quaternion left;
	private Quaternion right;
	private bool isTurned;
	//public Animator animationMonster;
	public Transform monster;
	private Vector3 position;

	// 0: dead
	// 1: start
	private AudioSource[] audios;
	// boolean
	private bool isMove;
	private float time;
	private bool start;

	// Use this for initialization
	void Start () {
		//Debug.Log ("Monster");
		this.velocity = 4;
		this.left = Quaternion.Euler (0, 270, 0);
		this.right = Quaternion.Euler (0, 90, 0);
		this.isTurned = false;
		this.isMove = false;
		this.audios = this.gameObject.GetComponents<AudioSource> ();
		//this.audios [1].Play ();

	}
	
	// Update is called once per frame
	void Update () {


		Debug.DrawRay(this.transform.position, -this.transform.right * 6f, Color.red);
		Debug.DrawRay(this.transform.position, this.transform.right * 6f, Color.green);

		this.transform.position += this.transform.forward * this.velocity * Time.deltaTime;

		if(this.isMove){
			this.moveToPosition();
			if(this.time >= 2){
				this.isMove = false;
				this.velocity = 4;
				//this.animationMonster.SetBool ("collision", false);
			}
			this.time += Time.deltaTime;
		}


	}

	void OnTriggerEnter(Collider info){
		Debug.Log ("Monster: Ha colisionado con" + info.transform.tag + " Name:" + info.transform.name);

		if(info.transform.tag == "Wall"){
			//Debug.Log ("Monster: Ha colisionado con" + info.transform.tag);
			RaycastHit hit;
			this.velocity = 0;
			//this.animationMonster.SetBool("collision",true);
			collisionMonster();
		}

		if(info.transform.tag == "Player" || info.transform.tag == "Player2"){
			if(!PositionCharacter.multiplayer){
				this.audios[0].Play();
			}else{
				if(info.transform.tag == "Player"){
					EmitSoundsMultiplayer.emitSound(5);
				}
				
				if(info.transform.tag == "Player2"){
					EmitSoundMultiplayer2.emitSound(5);
				}
			}
		}
	}


	public void collisionMonster(){
		//this.animationMonster.SetBool ("collision", false);
		//this.animationMonster.SetBool ("collision", true);
		RaycastHit hit;

		if(Physics.Raycast(this.transform.position,-this.transform.up,out hit, 10f)){
			Debug.Log ("Ha colisionado el rayo abajo");
			if(Physics.Raycast(this.transform.position,-this.transform.right, 6f)){
				Debug.Log ("Se gira a la derecha");
				if(!this.isTurned){
					this.transform.rotation = this.right;
					this.isTurned = true;
				}else{
					this.transform.rotation = Quaternion.Euler(0,0,0);
					this.isTurned = false;
				}
			}else{
				Debug.Log ("Se gira a la izquierda");
				if(!this.isTurned){
					this.transform.rotation = this.left;
					this.isTurned = true;
				}else{
					this.transform.rotation = Quaternion.Euler(0,0,0);
					this.isTurned = false;
				}

			}
		}
		this.position = new Vector3 (hit.transform.root.transform.position.x, 3, hit.transform.root.transform.position.z);

		this.isMove = true;
		this.time = 0;
		//transform.Translate (hit.transform.root.transform.position * Time.deltaTime);
		
		// Right(90)
	}
	
	void moveToPosition(){
		//Debug.Log ("Se esta moviendo Rotation:" + this.transform.rotation);

		transform.position = Vector3.Lerp (this.transform.position, this.position, 1f * Time.deltaTime);

		//this.velocity = 8;
		//Debug.Log ("Se esta moviendo Rotation:" + this.transform.rotation);
	}
}
