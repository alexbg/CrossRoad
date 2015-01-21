using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CrossRoad.SaveData{

	public class SaveData : MonoBehaviour {

		public Text walls;
		public Toggle lamp;

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

		// Guarda los PlayerPrefs
		private void saveData(){
			PlayerPrefs.Save ();
		}
	}
}
