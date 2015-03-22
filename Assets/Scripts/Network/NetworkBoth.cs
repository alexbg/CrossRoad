using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkBoth : MonoBehaviour {
	private int players;
	private int ready;
	private int positions;
	public Text numberOfPlayers;
	public GameObject row;
	public Text textPing;
	public Text textReady;
	public Text textMode;
	public InputField textChat;
	public Text textInputChat;
	// Use this for initialization
	/*void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}*/

	// Conexion y creacion de servidor

	public void startServer(){
		
		
		Network.incomingPassword = PlayerPrefs.GetString("password");
		
		Network.InitializeServer (4, PlayerPrefs.GetInt("port"), Network.HavePublicAddress ());
		
	}

	public void connect(){
		
		Network.Connect (
			PlayerPrefs.GetString ("ip"), 
			PlayerPrefs.GetInt ("port"), 
			PlayerPrefs.GetString ("password")
			);
		
	}

	// Funciones no RPC

	public void imReady(){
		Debug.Log ("I am ready");
		this.ready++;
		this.textReady.text = this.ready.ToString ();
		this.GetComponent<NetworkView>().RPC ("getReady", RPCMode.Others);
	}

	public void imNotReady(){
		this.ready--;
		this.textReady.text = this.ready.ToString ();
		this.GetComponent<NetworkView>().RPC ("getNotReady", RPCMode.Others);
	}

	public void sentGameMode(int mode){
		this.GetComponent<NetworkView>().RPC ("getGameMode", RPCMode.Others, mode);
	}

	public void sendMessageToChat(){
		Debug.Log ("Enviado al chat");
		this.GetComponent<NetworkView>().RPC("sendMessage",RPCMode.All,this.textInputChat.text,PlayerPrefs.GetString("username"));
	}

	// Mensajes
	[RPC]
	void sendMessage(string message,string username){
		Debug.Log ("Recibido el caht"+username+" "+message);
		if(this.textChat.text == "")
			this.textChat.text = username+": "+message+"\n";
		else
			this.textChat.text += username+": "+message+"\n";
	}

	// Obtiene el numero de jugadores
	[RPC]
	void getNumberOfPlayers(int players){
		this.players = players;
		this.numberOfPlayers.text = this.players.ToString ();
	}

	// Obtiene el ping
	[RPC]
	void getPing(int ping){
		this.textPing.text = ping.ToString();
	}

	// indica que esta preparado
	[RPC]
	void getReady(){
		this.ready++;
		this.textReady.text = this.ready.ToString(); 
	}

	// Indica que no esta preparado
	[RPC]
	void getNotReady(){
		this.ready--;
		this.textReady.text = this.ready.ToString(); 
	}

	// Obtiene el numero de jugadores preparados
	[RPC]
	void getReadyPlayers(int ready){
		this.ready = ready;
		this.textReady.text = ready.ToString();
	}

	[RPC]
	void getGameMode(int mode){
		PlayerPrefs.SetInt ("mode", mode);
		//PlayerPrefs.Save();
		/*switch(mode){
			case 0:
				this.textMode.text = "Quite";
				break;
			case 1:
				this.textMode.text = "Run";
				break;
		}*/

		if(mode == 0)
			this.textMode.text = "Quite";
		if(mode == 1)
			this.textMode.text = "Run!!";
	}

	// Inicia el nivel
	// SI es el servidor, les dice a todos que inicien el nivel
	[RPC]
	public void loadLevelByNetwork(int level){
		Application.LoadLevel (level);
		if(Network.isServer){
			this.GetComponent<NetworkView>().RPC ("loadLevelByNetwork",RPCMode.OthersBuffered,level);
			// Paro la red y nadie puede recibir ningun RPC
			Network.isMessageQueueRunning = false;
		}

	}

	// Obtiene su posicion en el juego
	[RPC]
	public void getMyPosition(int position){
		switch(position){
			case 2:
				PlayerPrefs.SetString ("position", "Two");
				break;
			case 3:
				PlayerPrefs.SetString ("position", "Three");
				break;
			case 4:
				PlayerPrefs.SetString ("position", "Four");
				break;
		}
		Debug.Log ("Mi posicion es: " + PlayerPrefs.GetString("position"));
	}

	// Eventos

	// Cuando inicias el servidor
	void OnServerInitialized(){
		Debug.Log ("El servidor se ha iniciado");
		this.players = 1;
		this.ready = 0;
		this.numberOfPlayers.text = this.players.ToString ();
		this.positions = 2;
		PlayerPrefs.SetString ("position", "One");
		//Debug.Log ("Mi posicion es: " + PlayerPrefs.GetString("position"));
	}

	// Cuando se conecta un usuario al servidor
	/*void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha conectado");
		this.players++;
		this.numberOfPlayers.text = this.players.ToString ();
		// envio a todos los usuarios conectados
		this.GetComponent<NetworkView>().RPC ("getNumberOfPlayers", RPCMode.Others, this.players);
		// envio al usuario conectado su ping
		this.GetComponent<NetworkView>().RPC ("getPing", player,Network.GetAveragePing(player));
		// envio los usuarios que ya estan listos
		this.GetComponent<NetworkView>().RPC ("getReadyPlayers", player, this.ready);
		// Le envio su posicion en el juego
		this.GetComponent<NetworkView>().RPC ("getMyPosition", player,this.players);
		//Debug.Log ("Mi posicion es: " + PlayerPrefs.GetString("position"));
	}
	
	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha desconectado");
		this.players--;
		this.ready--;
		this.numberOfPlayers.text = this.players.ToString ();
		// Obtiene el numero de jugadores
		this.GetComponent<NetworkView>().RPC ("getNumberOfPlayers", RPCMode.Others, this.players);
		// envio los usuarios que ya estan listos
		this.GetComponent<NetworkView>().RPC ("getReadyPlayers", player, this.ready);
	}

	// Cuando falla al intentar conectarse con el servidor
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log ("ha fallado la conexion");
	}

	// Cuando te conectas al server
	void OnConnectedToServer(){
		Debug.Log("Te has conectado al server");
	}

	// Cuando te desconectas del server, tanto como servidor como cliente
	void OnDisconnectedFromServer(NetworkDisconnection info){
		Debug.Log ("Te has desconectado del servidor");
	}*/
}
