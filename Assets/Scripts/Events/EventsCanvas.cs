using UnityEngine;
using System.Collections;
//using CrossRoad.SaveData;

namespace CrossRoad.EventsCanvas{

	public class EventsCanvas : MonoBehaviour {
		//private SaveData save;
		public Animator animator;
		// Use this for initialization
		void Start () {
			//this.save = new SaveData ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		/// <summary>
		/// Loads the scene.
		/// </summary>
		/// <param name="scene">The number of scene</param>
		public void loadScene(int scene){
			Time.timeScale = 1.0f;
			Application.LoadLevel (scene);
		}

		/// <summary>
		/// Return to game
		/// </summary>
		public void back(){
			GameObject.Find("Character").SendMessage("pause",false);
		}

		// Activa una animacion de bool
		public void enableAnimation(string animation){
			this.animator.SetBool (animation, true);
		}

		// Desactiva una animacion de bool
		public void disableAnimation(string animation){
			this.animator.SetBool (animation, false);
		}

	}
}
