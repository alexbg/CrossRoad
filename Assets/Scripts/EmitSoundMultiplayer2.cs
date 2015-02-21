using UnityEngine;
using System.Collections;

namespace CrossRoad.Audio.Player2{
	/// <summary>
	/// Emit sound multiplayer2.
	/// Controla los sonidos que emite el jugador2 en multiplayer
	/// </summary>
	public class EmitSoundMultiplayer2 : MonoBehaviour {

		// Use this for initialization
		// 0: walk
		// 1: Trampoline
		// 2: tired
		// 3: Explision
		// 4: Click
		private static AudioSource[] source;
		void Start () {
			EmitSoundMultiplayer2.source = gameObject.GetComponents<AudioSource> ();
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
			EmitSoundMultiplayer2.source [sound].Play ();
		}
		
		/// <summary>
		/// Stops the sound.
		/// </summary>
		/// <param name="sound">Sound.</param>
		public static void stopSound(int sound){
			EmitSoundMultiplayer2.source [sound].Stop();
		}
		/// <summary>
		/// Is the playing.
		/// </summary>
		/// <returns><c>true</c>, if playing was ised, <c>false</c> otherwise.</returns>
		/// <param name="sound">Sound.</param>
		public static bool isPlaying(int sound){
			return EmitSoundMultiplayer2.source [sound].isPlaying;
		}

		public static void changePicth(int sound, float pitch){
			EmitSoundMultiplayer2.source [sound].pitch = pitch;
		}
		
	}
}

