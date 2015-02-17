using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CrossRoad.Menu{

	public class MenuInGame : MonoBehaviour {

		public GameObject character;

		public GameObject canvasWinner;

		public Text textWinner;
		//public Canvas canvasWinner;

		// Variables privadas

		private bool pause;


		// Use this for initialization
		void Start () {
			this.pause = false;
		}
		
		// Update is called once per frame
		void Update () {
		
			if(Input.GetButtonDown("Cancel")){
				Debug.Log ("Se ha pulsado el boton escape");
				if(this.pause){
					this.changeActions(true,1.0f);
					this.canvasWinner.SetActive(false);
					Screen.showCursor = false;
				}else{
					this.changeActions(false,0);
					this.canvasWinner.SetActive(true);
					Screen.showCursor = true;
				}
				togglePause();
				//Debug.Log(pause);
			}
		}

		private void togglePause(){
			if(this.pause)
				this.pause = false;
			else
				this.pause = true;
		}

		private void changeActions(bool pause,float time){

			Time.timeScale = time;
			//Debug.Log ("Se ha despausado");
			foreach(MouseLook look in this.character.gameObject.GetComponentsInChildren<MouseLook>()){
				look.enabled = pause;
			}
			this.character.gameObject.GetComponent<MoveCharacter>().enabled = pause;
			//Screen.showCursor = pause;
		}

		public void winner(){
			this.changeActions (false, 0);
			this.canvasWinner.SetActive(true);
			this.textWinner.enabled = true;
		}

	}
}
