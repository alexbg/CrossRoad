using UnityEngine;
using System.Collections;

public class AnimationMonster : MonoBehaviour {

	private Quaternion left;
	private Quaternion right;
	private bool isTurned;
	public Animator animationMonster;
	public Transform monster;
	private Vector3 position;

	// boolean
	private bool isMove;

	void Start(){
		this.left = Quaternion.Euler (0, -90, 0);
		this.right = Quaternion.Euler (0, 90, 0);
		this.isTurned = false;
		this.isMove = false;
	}

	void Update(){

		if(this.isMove){
			this.moveToPosition();
		}

	}

	public void collisionMonster(){
		this.animationMonster.SetBool ("collision", false);
		RaycastHit hit;

		if(Physics.Raycast(this.monster.position,-this.monster.up,out hit, 10f)){
			Debug.Log ("Ha colisionado el rayo abajo");
			if(Physics.Raycast(this.monster.position,-this.monster.right, 5f)){
				//this.monster.position = new Vector3(hit.transform.root.transform.position.x,3,hit.transform.root.transform.position.z);
				
				if(!this.isTurned){
					this.monster.transform.rotation = this.right;
					this.isTurned = true;
				}else{
					this.monster.rotation = Quaternion.Euler(0,0,0);
					this.isTurned = false;
				}
				
				/*this.transform.rotation = this.right;
				this.transform.rotation = Quaternion.identity;*/
				
				//ModeMonster.velocity = 3;
				//Debug.Log ("Monster: Right" + " Name: " + hit.collider.name);
			}else{
				
				//this.monster.position = new Vector3(hit.transform.root.transform.position.x,3,hit.transform.root.transform.position.z);
				
				if(!this.isTurned){
					this.monster.rotation = this.left;
					this.isTurned = true;
				}else{
					this.monster.rotation = Quaternion.Euler(0,0,0);
					this.isTurned = false;
				}
				
				/*this.transform.rotation = this.left;
				this.transform.rotation = Quaternion.identity;*/
				//ModeMonster.velocity = 3;
				//Debug.Log ("Monster: Left");
				this.transform.root.transform.position = new Vector3(this.transform.position.x,3,this.transform.position.z);
			}
		}
		//this.position = new Vector3 (hit.transform.root.transform.position.x, 3, hit.transform.root.transform.position.z);
		//this.velocity = 3;
		this.isMove = true;
		//transform.Translate (hit.transform.root.transform.position * Time.deltaTime);

		// Right(90)
	}

	void moveToPosition(){
		Debug.Log ("Se esta moviendo: " + this.position);
		transform.Translate ((this.position * 5) * Time.deltaTime ,Space.World);
		if(this.transform.position == this.position){
			this.isMove = false;
			//ModeMonster.velocity = 3;
		}
	}
}
