using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CrossRoad.SaveData{

	public class SaveData : MonoBehaviour {

		public Text walls;
		public Toggle lamp;
		public Text text;

		void awake(){
			/*PlayerPrefs.SetInt ("players", 1);
			PlayerPrefs.SetInt ("mode", 0);
			PlayerPrefs.SetInt ("lamp", 0);
			Debug.Log("awake");*/
		}

		void Start(){

		}

		public void saveWalls(){
			if(this.walls.text != null)
				PlayerPrefs.SetInt ("walls", int.Parse(walls.text));

		}

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


		public void saveString (string name){
			PlayerPrefs.SetString (name, text.text);
		}

		public void saveInt(string name){
			PlayerPrefs.SetInt (name, int.Parse(text.text));
			Debug.Log (name + " " + text.text);
		}

		public void saveFloat(string name){
			PlayerPrefs.SetFloat (name, float.Parse(text.text));
		}


		// Guarda los PlayerPrefs
		private void saveData(){
			PlayerPrefs.Save ();
		}
	}
}
