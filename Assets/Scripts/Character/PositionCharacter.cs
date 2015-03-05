using UnityEngine;
using System.Collections;
/// <summary>
/// Position character.
/// Se encarga de posicionar y de activar o desactivar los gameobject 
/// necesarios para jugar, tanto para multijugador offline como un jugador
/// </summary>
public class PositionCharacter : MonoBehaviour {

	// Use this for initialization
	// Tiene los 4 gameobject que son los personajes para multijugador
	public GameObject[] characteres;
	// Tiene el gameobject del personaje principal
	public GameObject principal;
	// El audio que se usa para el multijugador
	public GameObject audioListener;
	// Al ser static, se puede acceder desde cualquier sitio, y no esta en ningun
	// namespace asique con poner el nombre de la variable clase.nombreVariable funciona
	// Indica si esta jugando con mas de un jugador en modo offline
	public static bool multiplayer;

	void Start () {

		//PlayerPrefs.SetInt ("players", 2);
		Debug.Log (PlayerPrefs.GetInt ("players"));
		// configuracion multijugador offline
		if(PlayerPrefs.GetInt("players") != 1){
			PositionCharacter.multiplayer = true;
			this.principal.SetActive (false);
			this.audioListener.SetActive(true);
			// El 0 es el character 1
			for(int i = 0;i<=PlayerPrefs.GetInt("players")-1;i++){
				// Se activa el gameobject
				this.characteres[i].SetActive(true);
				//Configura la camara segun el numero de jugadores
				this.characteres[i].GetComponentInChildren<Camera>().rect = this.changeCamera(i,PlayerPrefs.GetInt("players"));
				Debug.Log ("Jugador"+i+" esta en posicion");
			}	
		}else{
			PositionCharacter.multiplayer = false;
		}
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
	// Configura la camara para 2,3 o 4 jugadores
	private Rect changeCamera(int character,int players){
		Rect rect = new Rect(0,0,0,0);
		switch(players){
			case 2:
				if(character == 0)
					rect = new Rect(0,0,0.5f,1);
				if(character == 1)
					rect = new Rect(0.5f,0,0.5f,1);
				break;
			case 3:
				break;
			case 4:
				break;
		}

		return rect;
	}
}
