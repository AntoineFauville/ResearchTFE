using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTotemEnigma : MonoBehaviour {

	public GameObject FEEDINFORMATIOOON;
	public GameObject Endcanvas;

	public Quaternion actual1;

	public GameObject TopPart;
	public GameObject MidPart;
	public GameObject DownPart;

	GameObject TopPartScript;
	GameObject MidPartScript;
	GameObject DownPartScript;

	public GameObject TopOutLineTotem;
	public GameObject MidOutLineTotem;
	public GameObject DownOutLineTotem;

	public bool EnigmaIsDone;

	GestionSelectionParts GSP;

	TriggerSendingInfoTotem1 Trigger01;
	TriggerSendingInfoTotem2 Trigger02;

	void Start () {
		Trigger01 = GameObject.Find ("TriggerForResolution01").GetComponent<TriggerSendingInfoTotem1>();
		Trigger02 = GameObject.Find ("TriggerForResolution02").GetComponent<TriggerSendingInfoTotem2>();

		//TopOutLineTotem = GameObject.Find ("TopPartOutlineTotem");
		//MidOutLineTotem = GameObject.Find ("MiddlePartOutlineTotem");
		//DownOutLineTotem = GameObject.Find ("DownPartOutlineTotem");

		TopPartScript = GameObject.Find ("PartTopGestionTotem");
		MidPartScript = GameObject.Find ("PartMidGestionTotem");
		DownPartScript = GameObject.Find ("PartDownGestionTotem");

		GSP = GameObject.Find ("GestionPartSelectionTotem").GetComponent<GestionSelectionParts>();
	}

	// Update is called once per frame
	void Update () {

		Quaternion currentRotationPart1 = (TopPart.transform.rotation);
		Quaternion currentRotationPart2 = (MidPart.transform.rotation);
		Quaternion currentRotationPart3 = (DownPart.transform.rotation);

		float rot1 = Mathf.Abs (currentRotationPart1.y); 
		float rot2 = Mathf.Abs (currentRotationPart2.y);
		float rot3 = Mathf.Abs (currentRotationPart3.y);

		//print (rot2);

		/*if (rot1 >= 0.95f && rot1 <= 0.96f &&
			rot2 >= 0.95f && rot2 <= 0.96f &&
			rot3 >= 0.95f && rot3 <= 0.96f) 
		{*/

		if (Trigger01.GoodToGoTotem01 && Trigger02.GoodToGoTotem02) {
			if(rot1 == 1.0f && rot2 == 1.0f && rot3 == 1.0f){
				EnigmaIsDone = true;

				TopPartScript.GetComponent<AnimTopPartNotMoving> ().enabled = false;
				MidPartScript.GetComponent<RotationEnigmeLazer> ().enabled = false;
				DownPartScript.GetComponent<RotationEnigmeLazer> ().enabled = false;

				GSP.state1 = false;
				GSP.state2 = false;
				GSP.state3 = false;

				TopOutLineTotem.SetActive (false);
				MidOutLineTotem.SetActive (false);
				DownOutLineTotem.SetActive (false);

				GSP.enabled = false;

				print ("EnigmaTotemDone");
				//	Endcanvas.SetActive (true);
				//	FEEDINFORMATIOOON.SetActive (true);
			}
		}
	}
}
