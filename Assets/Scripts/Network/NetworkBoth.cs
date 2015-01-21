using UnityEngine;
using System.Collections;

public class NetworkBoth : MonoBehaviour {

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

	// Mensajes

	void sentMessage(string message){
		networkView.RPC (message, RPCMode.Others);
	}


	// Eventos

	void OnServerInitialized(){
		Debug.Log ("El servidor se ha iniciado");
	}
	
	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha conectado");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("Un jugador se ha desconectado");
	}

	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log ("ha fallado la conexion");
	}
	
	void OnConnectedToServer(){
		Debug.Log("Te has conectado al server");
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info){
		Debug.Log ("Te has desconectado del servidor");
	}
}
