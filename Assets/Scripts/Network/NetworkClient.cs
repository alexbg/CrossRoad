using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void connect(){

		Network.Connect (
			PlayerPrefs.GetString ("ip"), 
			PlayerPrefs.GetInt ("port"), 
			PlayerPrefs.GetString ("password")
		);

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
