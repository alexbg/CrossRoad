using UnityEngine;
using System.Collections;

public class NetworkInGame : MonoBehaviour {

	// Public
	public GameObject character;
	//Private

	//private GameObject position;
	private bool instatiate;
	private int usersConnected;
	private GameObject position;

	// Use this for initialization
	void Start () {
		if(Network.isServer){
			Network.isMessageQueueRunning = true;
			this.usersConnected++;
			Debug.Log ("Mi posicion es: " + PlayerPrefs.GetString("position"));
		}

		this.instatiate = true;
		//usersConnected++;
		// Pido permiso al servidor para poder instanciar el personaje si soy cliente
		/*if(Network.isClient)
			this.networkView.RPC ("canInstantiate",RPCMode.Server);*/
		if(Network.isClient){
			this.networkView.RPC("connected",RPCMode.Server);
		}

	}
	
	// Update is called once per frame
	void Update () {
		//Time.timeScale = 0;
		if(Network.isServer && this.usersConnected >= 2 && this.instatiate){

			this.networkView.RPC ("instantiateCharacter",RPCMode.AllBuffered);

			this.instatiate = false;
		}
	
		//Debug.Log (this.usersConnected);
	}

	// Metodos RPC

	[RPC]
	void connected(){
		Debug.Log ("Se ha conectado un jugador");
		this.usersConnected++;
	}

	// Inicia el personaje
	[RPC]
	void instantiateCharacter(){
		//Debug.Log ("position: " + PlayerPrefs.GetString ("position"));
		this.position = GameObject.Find(PlayerPrefs.GetString("position"));
		Debug.Log (this.position.name);
		Network.Instantiate(
			this.character,
			this.position.transform.position,
			this.position.transform.rotation,
			0
		);


		//Time.timeScale = 1.0;*/
	}
}
