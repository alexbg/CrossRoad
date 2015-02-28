using UnityEngine;
using System.Collections;

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
	
	// boolean
	private bool isMove;
	private float time;

	// Use this for initialization
	void Start () {
		//Debug.Log ("Monster");
		this.velocity = 8;
		this.left = Quaternion.Euler (0, 270, 0);
		this.right = Quaternion.Euler (0, 90, 0);
		this.isTurned = false;
		this.isMove = false;


		//this.left = Quaternion.Euler (0, -90, 0);
		//this.right = Quaternion.Euler (0, 90, 0);
		//this.isTurned = false;
		//this.transform.rotation = this.left;
		//this.transform.rotation = this.right;
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 forward = this.transform.TransformDirection (this.transform.forward);
		Debug.DrawRay(this.transform.position, this.transform.forward * 10f, Color.green);
		if(Physics.Raycast(this.transform.position,this.transform.forward,10f)){
			Debug.Log ("Mosntruo golpeado pared");
		}*/

		Debug.DrawRay(this.transform.position, -this.transform.right * 6f, Color.red);
		Debug.DrawRay(this.transform.position, this.transform.right * 6f, Color.green);

		this.transform.position += this.transform.forward * this.velocity * Time.deltaTime;

		if(this.isMove){
			this.moveToPosition();
			if(this.time >= 2){
				this.isMove = false;
				this.velocity = 8;
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
	}


	public void collisionMonster(){
		//this.animationMonster.SetBool ("collision", false);
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
