using UnityEngine;
using System.Collections;

namespace CrossRoad.Audio.Player1{
	/// <summary>
	/// Emit sounds multiplayer.
	/// Controla los sonidos que emite el jugador1 en multiplayer
	/// </summary>
	public class EmitSoundsMultiplayer : MonoBehaviour {

		// Use this for initialization
		// 0: walk
		// 1: Trampoline
		// 2: tired
		// 3: Explision
		// 4: Click
		private static AudioSource[] source;
		void Start () {
			EmitSoundsMultiplayer.source = gameObject.GetComponents<AudioSource> ();
			//Debug.Log (EmitSoundsMultiplayer.source[0]);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		/// <summary>
		/// Emits the sound.
		/// </summary>
		/// <param name="sound">Sound.</param>
		public static void emitSound(int sound){
			EmitSoundsMultiplayer.source [sound].Play ();
		}

		/// <summary>
		/// Stops the sound.
		/// </summary>
		/// <param name="sound">Sound.</param>
		public static void stopSound(int sound){
			EmitSoundsMultiplayer.source [sound].Stop();
		}
		/// <summary>
		/// Is the playing.
		/// </summary>
		/// <returns><c>true</c>, if playing was ised, <c>false</c> otherwise.</returns>
		/// <param name="sound">Sound.</param>
		public static bool isPlaying(int sound){
			return EmitSoundsMultiplayer.source [sound].isPlaying;
		}

	}
}
