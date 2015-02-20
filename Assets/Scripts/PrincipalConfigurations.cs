using UnityEngine;
using System.Collections;

namespace CrossRoad.Configurations{

	public class PrincipalConfigurations : MonoBehaviour {

		void Start(){
			//PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetString("username","Pepe");
			//PlayerPrefs.Save ();
			//Debug.Log (Input.GetJoystickNames ()[0]);
		}

		void OnLevelWasLoaded(int level) {
			Time.timeScale = 1.0f;
		}
	}
}