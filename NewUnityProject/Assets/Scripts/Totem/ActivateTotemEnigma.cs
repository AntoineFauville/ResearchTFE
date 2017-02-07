using UnityEngine;
using System.Collections;
using ThirdPersonCamera;

public class ActivateTotemEnigma : MonoBehaviour {

	public bool playerIsHere = false;
	public bool amIOn = false;

	GameObject player;
	GameObject playerWrong;
	GameObject cam1;
	GameObject cam2;

	GameObject MapAndInventory;

	public GameObject cube;

	EndTotemEnigma EndingTotemScript;
	GestionSelectionParts GSP;

	void Start () {
		player = GameObject.Find ("Player");
		playerWrong = GameObject.Find ("CrounchingPlayerPuzzleTotem");
		cam1 = GameObject.Find ("Main Camera Main");
		cam2 = GameObject.Find ("CameraEnigmeTotem");
		GSP = GameObject.Find ("GestionPartSelectionTotem").GetComponent<GestionSelectionParts>();

		MapAndInventory = GameObject.Find ("ScriptManager");

		EndingTotemScript = GameObject.Find ("EndGestionTotem").GetComponent<EndTotemEnigma>();

		playerWrong.SetActive (false);
		cam2.SetActive (false);
	}

	void Update () {

		if (!amIOn && playerIsHere && Input.GetButtonDown ("Submit") && EndingTotemScript.EnigmaIsDone == false) {
			amIOn = true;

			player.SetActive (false);
			playerWrong.SetActive (true);

			MapAndInventory.GetComponent<Map> ().enabled = false;
			MapAndInventory.GetComponent<OpenCloseBook> ().enabled = false;

			cam1.SetActive (false);
			cam2.SetActive (true);


			//GestionSelectionParts

			GSP.state1 = true;
			GSP.state2 = false;
			GSP.state3 = false;

			GSP.OutlineDown.SetActive (true);
			GSP.OutlineMid.SetActive (false);
			GSP.OutlineTop.SetActive (false);

			//est ce que j'ai résolu l'énigme ou pas
			//cube.SetActive (true);

		} else if (amIOn && playerIsHere && Input.GetButtonDown ("Submit") && EndingTotemScript.EnigmaIsDone == false) {
			amIOn = false;

			player.SetActive (true);
			playerWrong.SetActive (false);

			MapAndInventory.GetComponent<Map> ().enabled = true;
			MapAndInventory.GetComponent<OpenCloseBook> ().enabled = true;

			cam1.SetActive (true);
			cam2.SetActive (false);

			//GestionSelectionParts

			GSP.canIUseItDown = false;
			GSP.canIUseItMid = false;
			GSP.canIUseItUp = false;

			GSP.state1 = false;
			GSP.state2 = false;
			GSP.state3 = false;

			GSP.OutlineDown.SetActive (false);
			GSP.OutlineMid.SetActive (false);
			GSP.OutlineTop.SetActive (false);

			//est ce que j'ai résolu l'énigme ou pas
			//cube.SetActive (true);
		}
	}

	void OnTriggerEnter (Collider coll){
		if (coll.tag == "Player") {
			playerIsHere = true;
		}
	}
	void OnTriggerExit (Collider coll){
		if (coll.tag == "Player") {
			playerIsHere = false;
		}
	}
}
