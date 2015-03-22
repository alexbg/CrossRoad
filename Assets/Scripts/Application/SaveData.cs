using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Save basic information what is need in game by PlayerPrefs.
/// </summary>
namespace CrossRoad.SaveData{

	public class SaveData : MonoBehaviour {

		// Text of walls
		public Text walls;
		// Toggle of lamps
		public Toggle lamp;
		// 
		public Text text;

		/*void awake(){

		}

		void Start(){

		}*/

		/// <summary>
		/// Saves the walls.
		/// </summary>
		public void saveWalls(){
			if(this.walls.text != null)
				PlayerPrefs.SetInt ("walls", int.Parse(walls.text));

		}

		/// <summary>
		/// Saves the lamp.
		/// </summary>
		public void saveLamp(){
			if(this.lamp.isOn){
				PlayerPrefs.SetInt ("lamp", 1);
				//Debug.Log (PlayerPrefs.GetInt("lamp"));
			}else{
				PlayerPrefs.SetInt ("lamp", 0);
				//Debug.Log (PlayerPrefs.GetInt("lamp"));
			}
			//this.saveData ();
			//Debug.Log (PlayerPrefs.GetInt("lamp"));
		}

		/// <summary>
		/// Saves the game mode.
		/// </summary>
		/// <param name="gameMode">Game mode.</param>
		public void saveGameMode(int gameMode){
			Debug.Log ("Jugadores: "+PlayerPrefs.GetInt("players"));
			PlayerPrefs.SetInt ("mode", gameMode);

			/*if(this.lamp.isOn)
				PlayerPrefs.SetInt ("lamp", 1);*/
			Debug.Log ("Lamparas: "+PlayerPrefs.GetInt("lamp"));
			//this.saveData ();

		}

		/*public void savePassword(){
			PlayerPrefs.SetString ("password", this.text.text);
		}

		public void savePort(){
			PlayerPrefs.SetInt ("port", int.Parse(this.text.text));
		}

		public void saveConnections(){
			PlayerPrefs.SetInt ("connections", int.Parse(this.text.text));
		}*/

		/// <summary>
		/// Saves the a string given as parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		public void saveString (string name){
			PlayerPrefs.SetString (name, text.text);
		}

		/// <summary>
		/// Saves the int given as parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		public void saveInt(string name){
			PlayerPrefs.SetInt (name, int.Parse(text.text));
			Debug.Log (name + " " + text.text);
		}

		/// <summary>
		/// Saves the float given as parameter.
		/// </summary>
		/// <param name="name">Name.</param>
		public void saveFloat(string name){
			PlayerPrefs.SetFloat (name, float.Parse(text.text));
		}


		// Guarda los PlayerPrefs
		private void saveData(){
			PlayerPrefs.Save ();
		}
	}
}
