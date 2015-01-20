using UnityEngine;
using System.Collections;

public class BackToGame : MonoBehaviour {

	public void back(){
		GameObject.Find("Character").SendMessage("pause",false);
	}
}
