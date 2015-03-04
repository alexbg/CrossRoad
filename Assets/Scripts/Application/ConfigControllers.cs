using UnityEngine;
using System.Collections;

public class ConfigControllers : MonoBehaviour {

	// 0: Horizontal
	// 1: Vertical
	// 2: Jump
	// 3: Run
	// 4: Cancel

	private string[] principal;
	private string[] player1;
	private string[] player2;

	void Awake(){

		// Configuracion botones principal;
		this.principal = new string[5];
		this.principal[0] = "Horizontal";
		this.principal[1] = "Vertical";
		this.principal[2] = "Jump";
		this.principal[3] = "Run";
		this.principal[4] = "Cancel";

		// Configuracion botones player1

		this.player1 = new string[5];
		this.player1[0] = "HorizontalPlayer1";
		this.player1[1] = "VerticalPlayer1";
		this.player1[2] = "JumpPlayer1";
		this.player1[3] = "RunPlayer1";
		this.player1[4] = "CancelPlayer1";

		// Configuracion botones player2

		this.player2 = new string[5];
		this.player2[0] = "HorizontalPlayer2";
		this.player2[1] = "VerticalPlayer2";
		this.player2[2] = "JumpPlayer2";
		this.player2[3] = "RunPlayer2";
		this.player2[4] = "CancelPlayer2";


		Debug.Log ("Configurando los controles " + this.principal[2]);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string[] getControllers(string player){
		string[] players = new string[1];
		switch(player){
			case "principal":
				players =  this.principal;
				break;
		}
		return players;
	}
}
