using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CrossRoad.Principal{

	public class PrincipalConfigurations : MonoBehaviour {



		/*void Awake(){

		}

		void Start(){

		}*/

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