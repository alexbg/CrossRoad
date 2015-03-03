using UnityEngine;
using System.Collections;

namespace CrossRoad.Audio{
	/// <summary>
	/// Emit sounds multiplayer.
	/// Controla los sonidos que emite el jugador1 en multiplayer
	/// </summary>
	public class EmitSoundsMultiplayer : MonoBehaviour {

		public AudioEmit player1;
		public AudioEmit player2;

		// Use this for initialization
		// 0: walk
		// 1: Trampoline
		// 2: tired
		// 3: Explision
		// 4: Click
		//private static AudioSource[] source;
		void Start () {
			//EmitSoundsMultiplayer.source = gameObject.GetComponents<AudioSource> ();
			//Debug.Log (EmitSoundsMultiplayer.source[0]);
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		/// <summary>
		/// Emits the sound.
		/// </summary>
		/// <param name="sound">Sound.</param>
		public void emitSound(byte sound, byte player){
			//EmitSoundsMultiplayer.source [sound].Play ();
			this.getPlayer (player).source [sound].Play ();
		}

		/// <summary>
		/// Stops the sound.
		/// </summary>
		/// <param name="sound">Sound.</param>
		public void stopSound(byte sound, byte player){
			//EmitSoundsMultiplayer.source [sound].Stop();
			this.getPlayer (player).source [sound].Stop();
		}
		/// <summary>
		/// Is the playing.
		/// </summary>
		/// <returns><c>true</c>, if playing was ised, <c>false</c> otherwise.</returns>
		/// <param name="sound">Sound.</param>
		public bool isPlaying(byte sound, byte player){
			//return EmitSoundsMultiplayer.source [sound].isPlaying;
			return this.getPlayer (player).source [sound].isPlaying;
			//return false;
		}

		/// <summary>
		/// Changes the picth.
		/// </summary>
		/// <param name="sound">Sound.</param>
		/// <param name="pitch">Pitch.</param>
		public void changePicth(byte sound, float pitch, byte player){
			//EmitSoundsMultiplayer.source [sound].pitch = pitch;
			this.getPlayer (player).source [sound].pitch = pitch;
		}


		private AudioEmit getPlayer(byte player){
			//AudioEmit selectedPlayer = new AudioEmit ();
			switch(player){
				case 1:
					return this.player1;
					break;
				case 2:
					return this.player2;
					break;
				default:
					return this.player1;
			}
			//return selectedPlayer;
		}
	}
}
