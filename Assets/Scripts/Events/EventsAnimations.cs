using UnityEngine;
using System.Collections;

namespace CrossRoad.Animations.EventsAnimations{

	public class EventsAnimations : MonoBehaviour {

		// Eventos al final o al principio de cada animacion
		public void enableCanvas(string name){
			GameObject.Find (name).GetComponent<Canvas>().enabled = true;
		}
		
		public void disableCanvas(string name){
			GameObject.Find (name).GetComponent<Canvas>().enabled = false;
		}
	}
}
