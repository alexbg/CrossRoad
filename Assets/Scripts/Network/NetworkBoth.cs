using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkBoth : MonoBehaviour {
	private int players;
	private int ready;
	public Text numberOfPlayers;
	public GameObject row;
	public Text textPing;
	public Text textReady;
	public Text textMode;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

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
		this.networkView.RPC ("getReady", RPCMode.Others);
	}

	public void imNotReady(){
		this.ready--;
		this.textReady.text = this.ready.ToString ();
		this.networkView.RPC ("getNotReady", RPCMode.Others);
	}

	public void sentGameMode(int mode){
		this.networkView.RPC ("getGameMode", RPCMode.Others, mode);
	}

	// Mensajes
	[RPC]
	void sentMessage(string message){
		networkView.RPC (message, RPCMode.Others);
	}

	[RPC]
	void getNumberOfPlayers(int players){
		this.players = players;
		this.numberOfPlayers.text = this.players.ToString ();
	}

	[RPC]
	void getPing(int ping){
		this.textPing.text = ping.ToString();
	}

	[RPC]
	void getReady(){
		this.ready++;
		this.textReady.text = this.ready.ToString(); 
	}

	[RPC]
	void getNotReady(){
		this.ready--;
		this.textReady.text = this.ready.ToString(); 
	}

	[RPC]
	void getReadyPlayers(int ready){
		this.ready = ready;
		this.textReady.text = ready.ToString();
	}

	[RPC]
	void getGameMode(int mode){
		PlayerPrefs.SetInt ("mode", mode);
		PlayerPrefs.Save();
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
			this.textMode.text = "Run";
	}

	// Eventos

	// Cuando inicias el servidor
	void OnServerInitialized(){
		Debug.Log ("El servidor se ha iniciado");
		this.players = 1;
		this.ready = 0;
		this.numberOfPlayers.text = this.players.ToString ();
	}

	// Cuando se conecta un usuario al servidor
	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha conectado");
		this.players++;
		this.numberOfPlayers.text = this.players.ToString ();
		// envio a todos los usuarios conectados
		this.networkView.RPC ("getNumberOfPlayers", RPCMode.Others, this.players);
		// envio al usuario conectado su ping
		this.networkView.RPC ("getPing", player,Network.GetAveragePing(player));
		// envio los usuarios que ya estan listos
		this.networkView.RPC ("getReadyPlayers", player, this.ready);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha desconectado");
		this.players--;
		this.numberOfPlayers.text = this.players.ToString ();
		this.networkView.RPC ("getNumberOfPlayers", RPCMode.Others, this.players);
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
	}
}
