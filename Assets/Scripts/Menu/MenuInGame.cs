using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrossRoad.Character;

namespace CrossRoad.Menu{

	public class MenuInGame : MonoBehaviour {

		// El gameObject del personaje principal
		public GameObject character;

		// El gameobject donde esta el menu
		public GameObject canvasWinner;

		// Text que muestra el texto de victoria
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

		/// <summary>
		/// Toggles the variable pause.
		/// </summary>
		private void togglePause(){
			if(this.pause)
				this.pause = false;
			else
				this.pause = true;
		}

		/// <summary>
		/// Cambia el tiempo para pausar el juego y desactiva o activa el movimiento
		/// de la cabeza(MouseLook), Movimiento del personaje(MoveCharacter).
		/// Si es multijugador, solo desactiva el movimiento de la camara que se controla
		/// con el mando
		/// </summary>
		/// <param name="pause">If set to <c>true</c> pause.</param>
		/// <param name="time">Time.</param>
		private void changeActions(bool pause,float time){

			Time.timeScale = time;

			if(!PositionCharacter.multiplayer){
				foreach(MouseLook look in this.character.gameObject.GetComponentsInChildren<MouseLook>()){
					look.enabled = pause;
				}
				this.character.gameObject.GetComponent<MoveCharacter>().enabled = pause;
			}else{
				foreach(MouseLookYoystick look in this.character.gameObject.GetComponentsInChildren<MouseLookYoystick>()){
					look.enabled = pause;
				}
			}
			//Screen.showCursor = pause;
		}

		/// <summary>
		/// Winner this instance.
		/// </summary>
		public void winner(){
			this.changeActions (false, 0);
			this.canvasWinner.SetActive(true);
			this.textWinner.enabled = true;
		}

	}
}
