using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void loadScene(int scene){
		Time.timeScale = 1.0f;
		Application.LoadLevel (scene);
	}
}
