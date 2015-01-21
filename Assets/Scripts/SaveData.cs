using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CrossRoad.SaveData{

	public class SaveData : MonoBehaviour {

		public Text walls;
		public Toggle lamp;
		public Text text;

		void Start(){
			// Pongo el valor inicial de las lamparas
			PlayerPrefs.SetInt ("lamp", 0);
			// Pongo un modo de juego por defecto
			PlayerPrefs.SetInt ("mode", 0);
			this.saveData ();
		}

		public void saveWalls(){
			if(this.walls.text != null)
				PlayerPrefs.SetInt ("walls", int.Parse(walls.text));

		}

		public void saveLamp(){
			if(this.lamp.isOn)
				PlayerPrefs.SetInt ("lamp", 1);
			else
				PlayerPrefs.SetInt ("lamp", 0);
			this.saveData ();

		}

		public void saveGameMode(int gameMode){

			PlayerPrefs.SetInt ("mode", gameMode);
			this.saveData ();

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
