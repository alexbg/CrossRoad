using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCastDetectWalls : MonoBehaviour {
	/****
	 * 
	 * Esta preparado para que siempre vaya de frente y no hacia abajo.
	 * Puede girar a la izquierda o derecha si va vertical.
	 * Si va horizontal, tiene que girar siempre en la direccion que lo coloque de forma vertical
	 * y hacia adelante no hacia abajo
	 * 
	 * */
	// Todos los muros disponibles
	public Transform[] walls;
	// El muro para girar
	public Transform wallToTurn;
	// Numero de muros que se quiere poner
	public int numberOfWalls;
	// El muro final
	public Transform finalWall;
	// Determina si estamos en test o no
	public bool isTest;


	// Rotacion de los muros que tienen trampas
	private Quaternion rotationWalls;
	// Rortacion de los muros que permite girar
	private Quaternion rotationTurn;
	// La probabilidad de que haya un giro
	private int probabilidad;
	// Lo que se le suma a todas las posiciones
	private int sum;


	// Indica si esta vertical
	private bool horizontal;

	// Posicion de los muros
	private Vector3 position;
	// Indica si ya ha empezado la creacion de muros
	private bool start;
	// true va a la derecha, false va a la izquierda
	private bool positionHorizontal;
	// Use this for initialization

	void Start () {

		if (!this.isTest)
			this.numberOfWalls = PlayerPrefs.GetInt ("walls");


		this.sum = 10;
		// Es false porque en un principio el inicio es vertical
		this.horizontal = false;
		// Posicion inicial del siguiente bloque
		this.position = new Vector3 (5, 5, 15);
		this.probabilidad = 20;
		this.start = true;

		for(int i = 1; i<= this.numberOfWalls; i++){


			//Comprobar si el siguiente bloque es para girar o no.
			int random = Random.Range(0,100);

			if(random <= this.probabilidad && !this.start){

				// Se comprueba si esta horizontal o vertical
				if(this.horizontal){
					// Se comprueba si esta yendo a la derecha o a la izquierda
					if(this.positionHorizontal){
						// significa que esta horizontal hacia la izquierda
						// Se rotan tanto el muro que gira como los muros normales
						this.rotationTurn = Quaternion.Euler(0,90,0);
						this.rotationWalls = Quaternion.Euler(0,0,0);
						Debug.Log ("Es true, va -horizontal y gira a la izquierda");
					}else{
						// significa que esta horizontal hacia la derecha
						this.rotationTurn = Quaternion.Euler(0,180,0);
						this.rotationWalls = Quaternion.Euler(0,0,0);
						Debug.Log ("Es true, va +horizontal y gira a la derecha");
					}

					this.horizontal = false;

				}else{
					int turnRandom = Random.Range(0,2);
					
					if(turnRandom == 1){
						// Indica que quiere girar a la derecha
						this.rotationTurn = Quaternion.Euler(0,-90,0);
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.positionHorizontal = true;
						Debug.Log ("Es true, va vertical y gira a la derecha");
					}
					else{
						// Indica que quiere girar a la izquierda
						this.rotationTurn = Quaternion.Euler(0,0,0);
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.positionHorizontal = false;
						Debug.Log ("Es false, va vertical y gira a la izquierda");
					}
					this.horizontal = true;
					this.start = true;
				}

				// Se clona el muro
				Instantiate (this.wallToTurn, this.position, this.rotationTurn);


			}else{
				// Elegir entre los muros
				int wall = Random.Range(0,walls.Length);

				// Se clona el muro
				Instantiate (this.walls[wall], this.position, this.rotationWalls);
				// Guardo la ultima posicion
				if(this.start)
					this.start = false;
			}


			// Aumentar la posicion para el siguiente muro
			if(this.horizontal){
				if(this.positionHorizontal)
					// significa que esta horizontal hacia la derecha
					this.position.x += this.sum;
				else
					// significa que esta horizontal hacia la izquierda
					this.position.x -= this.sum;
			}else{
				this.position.z += this.sum;
			}

			// guardo la posicion actual


		}
		Instantiate (this.finalWall, this.position, this.rotationWalls);

		// Pongo el canvas y el texto en el componente del finalWall
		//this.finalWallInstantiate.GetComponent<StartRayCast> ().textWinner = this.textWinner;
		//this.finalWallInstantiate.GetComponent<StartRayCast> ().canvasWinner = this.canvasWinner;


		// Destruye este objeto cuando haya terminado
		Destroy(this.gameObject);

		/*RaycastHit info;
		if(Physics.Raycast(transform.position,transform.forward,out info,Mathf.Infinity))
			Debug.Log("Ha tocado en frente:"+info.distance);

		if(Physics.Raycast(transform.position,-transform.right,out info,Mathf.Infinity))
			Debug.Log("Ha tocado a la izquierda:"+info.distance);

		if(Physics.Raycast(transform.position,transform.right,out info,Mathf.Infinity))
			Debug.Log("Ha tocado a la derecha:"+info.distance);

		if(Physics.Raycast(transform.position,-transform.forward,out info,Mathf.Infinity))
			Debug.Log("Ha tocado atras:"+info.distance);

		Debug.Log("ha terminado");*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
