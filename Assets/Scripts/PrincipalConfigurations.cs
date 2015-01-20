using UnityEngine;
using System.Collections;

namespace CrossRoad.Configurations{

	public class PrincipalConfigurations : MonoBehaviour {

		void OnLevelWasLoaded(int level) {
			Time.timeScale = 1.0f;
		}
	}
}