using UnityEngine;
using System.Collections;

namespace CrossRoad.NetworkCrossRoad.Server{

	public class NetworkServer : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void startServer(){


			Network.incomingPassword = PlayerPrefs.GetString("password");

			Network.InitializeServer (4, PlayerPrefs.GetInt("port"), Network.HavePublicAddress ());

		}

		void OnServerInitialized(){
			Debug.Log ("El servidor se ha iniciado");
		}

		void OnPlayerConnected(NetworkPlayer player){
			Debug.Log ("Un jugador se ha conectado");
		}

		void OnPlayerDisconnected(NetworkPlayer player){
			Debug.Log ("Un jugador se ha desconectado");
		}
	}
}