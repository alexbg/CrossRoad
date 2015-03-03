using UnityEngine;
using System.Collections;

namespace CrossRoad.Principal{

	public class PincipalFunctions : MonoBehaviour {

		// Use this for initialization
		/*void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}*/

		public void back(){
			GameObject.Find("Character").SendMessage("pause",false);
		}

		public void loadScene(int scene){
			Time.timeScale = 1.0f;
			Application.LoadLevel (scene);
		}

		public void closeGame(){
			Application.Quit ();
		}
	}
}
