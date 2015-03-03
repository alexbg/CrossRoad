using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CreateCrossRoad : MonoBehaviour {
	// Array de los muros
	public GameObject[] walls;
	public GameObject left;
	public GameObject right;
	// Cuantos muros quiere el usuario tener
	public int numberOfWalls;
	// Posicion de los muros, tanto de los que permiten girar como los que no
	private Vector3 position;
	// Rotacion de los muros que tienen trampas
	private Quaternion rotationWalls;
	// Rortacion de los muros que permite girar
	private Quaternion rotationTurn;
	// Longitud del array walls
	private int length;
	// Lo que debe de sumar a la posicion en cada momento
	private int sum;
	// Probabilidad de que haya un giro
	private int probabilidad;
	// Indica si esta subiendo[1] o bajando[-1]
	private int vertical;
	// indica si va a la izqueirda[-1] o a la derecha[1]
	private int horizontal;
	// Indica si va a girar a la derecha o a la izquierda
	private bool turn;
	// BORRAR
	private bool mustTurn;
	// Guarda las posiciones de los muros
	private Dictionary <string, int> dictionary;

	private List<float> listPositionX;
	private List<float> listPositionZ;

	private float maxZ;
	private float maxX;

	int total;
	// Use this for initialization
	void Start () {
		this.listPositionX = new List<float> ();
		this.listPositionZ = new List<float> ();
		// Lo que se le suma cada vez que cambia de posicion, ya sea la x o la z
		this.sum = 10;
		// Cuantos muros hay para elegir
		this.length = this.walls.Length;
		// Posicion de los muros
		this.position = new Vector3 (5, 5, 15);
		// La rotacion de los muros
		this.rotationWalls = new Quaternion (0, 0, 0, 0);
		// La rotacion de los giros
		this.rotationTurn = new Quaternion (0, 0, 0, 0);
		// Muro de inicio, siempre esta en el 5,5,5
		Instantiate (this.walls [0], new Vector3 (5, 5, 5), new Quaternion (0, 0, 0, 0));

		this.probabilidad = 50;

		this.vertical = 1;

		this.horizontal = 0;

		this.mustTurn = false;


		// PARA EVITAR LOS CHOCHES < SE USARAN LOS RAYCAST
		// Colocacion de los muros
		for(int i = 1;i <= this.numberOfWalls; i++){

			//Comprobar si el siguiente bloque es para girar o no.
			int random = Random.Range(0,100);

			// Comprobamos si debe de girar
			total = this.sum+20;

			Debug.DrawRay(this.position,transform.right,Color.green,30);
			// Determina si tiene que girar obligatoriamente
			if(this.vertical == 1){
				if(this.listPositionZ.Contains(this.position.z + 10))
					this.mustTurn = true;
			}else if(this.vertical == -1){
				if(this.listPositionZ.Contains(this.position.z - 10))
					this.mustTurn = true;
			}else{
				// Si va horizontal
				if(this.horizontal == 1){
					if(this.listPositionX.Contains(this.position.x + 10))
						this.mustTurn = true;
				}else if(this.horizontal == -1){
					if(this.listPositionX.Contains(this.position.x - 10))
						this.mustTurn = true;
				}
			}




			// Si la cantidad es menor a la probabilidad de que los giros salgan, entonces se creara un giro
			if(random <= this.probabilidad || this.mustTurn){


				// Determinar hacia que lado debe de girar

				if(this.mustTurn){

					Debug.Log ("Debe girar");

					if(this.vertical != 0 )
						if(this.position.x > this.maxX)
							this.turn = false;
						else if(this.position.x == this.maxX)
							this.turn = true;
						else
							this.turn = true;

					if(this.horizontal != 0)
						if(this.position.z > this.maxZ)
							this.turn = false;
						else if(this.position.z == this.maxZ)
							this.turn = true;
						else
							this.turn = true;


				}
					

				if(!this.mustTurn){
					if(Random.Range (0,1) == 1)
						this.turn = true;
					else
						this.turn = false;

				}

				// Giros true izquierda, false derecha
				if(this.turn){
					// en caso de 1 sera left
					//Instantiate (this.left, this.position, this.rotationTurn);
					Debug.Log ("Gira izquierda" + this.position);
					// Decidir que posicion debe de tener el siguiente giro

					// Si va vertical

					if(this.vertical == 1){
						this.horizontal = -1;
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.rotationTurn = Quaternion.Euler(0,0,0);
						this.vertical = 0;
					}else if(this.vertical == -1){
						this.horizontal = 1;
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.rotationTurn = Quaternion.Euler(0,180,0);
						this.vertical = 0;
					}else{
						// Si va horizontal
						if(this.horizontal == 1){
							this.vertical = 1;
							this.rotationWalls = Quaternion.Euler(0,0,0);
							this.rotationTurn = Quaternion.Euler(0,90,0);
							this.horizontal = 0;
						}else if(this.horizontal == -1){
							this.vertical = -1;
							this.rotationWalls = Quaternion.Euler(0,0,0);
							this.rotationTurn = Quaternion.Euler(0,-90,0);
							this.horizontal = 0;
						}
					}

					Instantiate (this.left, this.position, this.rotationTurn);

					this.mustTurn = false;
				}else{
					// en caso de 0 sera right
					Debug.Log ("Gira derecha" + this.position);
					// Si va vertical
					
					if(this.vertical == 1){
						this.horizontal = 1;
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.rotationTurn = Quaternion.Euler(0,-90,0);
						this.vertical = 0;
					}else if(this.vertical == -1){
						this.horizontal = -1;
						this.rotationWalls = Quaternion.Euler(0,90,0);
						this.rotationTurn = Quaternion.Euler(0,90,0);
						this.vertical = 0;
					}else{
						// Si va horizontal
						if(this.horizontal == 1){
							this.vertical = -1;
							this.rotationWalls = Quaternion.Euler(0,0,0);
							this.rotationTurn = Quaternion.Euler(0,0,0);
							this.horizontal = 0;
						}else if(this.horizontal == -1){
							this.vertical = 1;
							this.rotationWalls = Quaternion.Euler(0,0,0);
							this.rotationTurn = Quaternion.Euler(0,180,0);
							this.horizontal = 0;
						}
					}
					Instantiate (this.left, this.position, this.rotationTurn);

				}


			}else{

				random = Random.Range(0,this.length);

				Instantiate (this.walls [random], this.position, this.rotationWalls);

			}

			this.listPositionX.Add(this.position.x);
			this.listPositionZ.Add(this.position.z);

			if(this.maxX < this.position.x)
				this.maxX = this.position.x;
			if(this.maxZ < this.position.z)
				this.maxZ = this.position.z;

			this.position.x += this.sum * this.horizontal;
			
			this.position.z += this.sum * this.vertical;

		}
		Destroy (this.gameObject);
		//Instantiate (this.walls [0], new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
