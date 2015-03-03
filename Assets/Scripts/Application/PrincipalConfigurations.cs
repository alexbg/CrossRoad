using UnityEngine;
using System.Collections;

namespace CrossRoad.Principal{

	public class PrincipalConfigurations : MonoBehaviour {

		void Start(){
			//PlayerPrefs.DeleteAll ();
			//PlayerPrefs.SetString("username","Pepe");
			//PlayerPrefs.Save ();
			//Debug.Log (Input.GetJoystickNames ()[0]);
		}

		void OnLevelWasLoaded(int level) {
			Time.timeScale = 1.0f;
			// El PlayerPrefs se guarda en cada incluso al iniciar diferentes scenarios
			// y si cierras la aplicacion se guardan en el disco duro. Por eso
			// Los elimino para tener unos valores por defecto en las lamparas, numeros de jugadores, etc
			if(level == 0){
				PlayerPrefs.DeleteAll();
			}
		}

		void OnApplicationQuit(){
			// Al cerrar la aplicacion elimino todos los PlayerPrefes guardados
			PlayerPrefs.DeleteAll ();
		}
	}
}