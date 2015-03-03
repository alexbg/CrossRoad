using UnityEngine;
using System.Collections;

namespace CrossRoad.Audio{

	public class AudioListenerSlitCreen : MonoBehaviour {

		public GameObject[] characters;
		private int players;
		// Use this for initialization
		void Start () {
			this.players = PlayerPrefs.GetInt("players");

		}
		
		// Update is called once per frame
		void Update () {
			/*switch(this.players){
				case 2:
					this.transform.position = (this.characters[0].transform.position+this.characters[1].transform.position)/2;
				break;
			}*/
		}
	}
}
