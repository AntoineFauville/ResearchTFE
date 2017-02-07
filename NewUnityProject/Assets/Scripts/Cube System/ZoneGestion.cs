using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneGestion : MonoBehaviour {

	//list searching in between actual transform of area and stuff

	GameObject Player;
	public GameObject MainCamera;

	GameObject BlocageCube;
	DeathSystem DS;	
	GameObject Artefact;

	//bool scan

	public bool didICheckNextVillage;
	public bool isThereSomthingAround;
	public bool didICheckSmallThing;

	public bool cubeIsHereInTown;
	public bool resoudreLEnigme;

	public bool cubeIsHereSmall;

	public bool didCubeGotIntoTownOnce;
	public bool didCubeGotInSmallOnce;

	GameObject ScanningFeedBack01;
	GameObject ScanningFeedBack02;
	GameObject ScanningFeedBack03;

	public GameObject[] ListDeZone;

	public GameObject[] SmallObject;

	public bool AmIInsideArea;

	public GameObject TextVertical;
	public GameObject TextDescription;
	public GameObject TextSmall;

	//animation

	public Animator animVillage;
	bool animBoolVillage;

	public Animator anim;
	bool animBool;

	GameObject ImageBookVillage;
	GameObject ImageBookSmall;

	// Use this for initialization
	void Start () {
		//TextVertical = GameObject.Find ("Text petit vertical");
		//TextDescription = GameObject.Find ("Text full text description");
		//TextSmall = GameObject.Find ("Text small text description");
		BlocageCube = GameObject.Find ("BlocageCube");
		DS = GameObject.Find ("Player").GetComponent<DeathSystem>();
		Player = GameObject.Find ("Player");
		//MainCamera = GameObject.Find ("Main Camera Main");
		ScanningFeedBack01 = GameObject.Find ("PanelNewMapInfoScan");
		ScanningFeedBack02 = GameObject.Find ("PanelNewMapInfoScanned");
		ScanningFeedBack03 = GameObject.Find ("PanelNewMapInfoScannedSmall");
		ImageBookVillage = GameObject.Find ("ButtonImageBouquinVillage01");
		ImageBookSmall = GameObject.Find ("ButtonImageBouquinPots");
		ScanningFeedBack01.SetActive (true);
		ScanningFeedBack02.SetActive (false);
		ScanningFeedBack03.SetActive (false);

		ImageBookVillage.SetActive (false);
		ImageBookSmall.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		animVillage.SetBool ("AnimVillageBool", animBoolVillage);
		anim.SetBool ("ActivateDemoSmall",animBool);
		//distance face au village

		float distance = Vector3.Distance(Player.transform.position, ListDeZone[0].transform.position);
		//inside area


		if (!cubeIsHereInTown) {

			if (distance < 80) {
				//print ("IN");
				AmIInsideArea = true;
			}
			//outside area
			if (distance >= 80) {
				//print ("OUT"); 
				AmIInsideArea = false;
			}

		} else if (cubeIsHereInTown && !didCubeGotIntoTownOnce) {
			
			//launch animation du village
			StartCoroutine("waitAnimFirstVillage");
			//bloquer le cube, destroy + faire spawn le village
			//add 1 to list des endroits à visiter.$
			//une fois résolue -> resoudreLEnigme = true;
			didCubeGotIntoTownOnce = true;
			ImageBookVillage.SetActive (true);
			StartCoroutine ("returnBoolAnim");
			cubeIsHereInTown = false;
		}

		//distance face a un petit objet
		float distanceToSmallObject = Vector3.Distance(Player.transform.position, SmallObject[0].transform.position);

		if (!cubeIsHereSmall) {
			if (distanceToSmallObject < 20) {
				isThereSomthingAround = true;
			} else {
				isThereSomthingAround = false;
			}
		} else if (cubeIsHereSmall) {
			didCubeGotInSmallOnce = true;
			animBool = true;
			ImageBookSmall.SetActive (true);
			cubeIsHereSmall = false;
		}


		//scanning
		if (!didICheckNextVillage) {
			// si j'ai rien scanner 
			ScanningFeedBack01.SetActive (true);
			ScanningFeedBack02.SetActive (false);
			ScanningFeedBack03.SetActive (false);
			TextVertical.GetComponent<Text> ().text = "0 0";
			TextSmall.GetComponent<Text> ().text = " ";
			TextDescription.GetComponent<Text> ().text = "Fermez le menu, ensuite appuyez sur E et utilisez le bouton 'scan' pour avoir un Objectif";

		} else {
			if(!didICheckSmallThing && isThereSomthingAround){
				ScanningFeedBack01.SetActive (false);
				ScanningFeedBack02.SetActive (false);
				ScanningFeedBack03.SetActive (true);
				TextSmall.GetComponent<Text> ().text = "Nouveau petit Objet découvert en : A 1";
			} else {
				TextVertical.GetComponent<Text> ().text = "C 1";
				TextDescription.GetComponent<Text> ().text = "Nouvelle entrée ! Le cube nous donne les coordonnées suivantes : C 1";
				ScanningFeedBack01.SetActive (false);
				ScanningFeedBack02.SetActive (true);
				ScanningFeedBack03.SetActive (false);
			}
		}
	}

	public void scanning(){
		//new village found
		// 3 états différents

		// 1 new village found - osef si y a des petits trucs pas loin prio village
		// si j'appuie sur scan ca lance le scan
		if (!didICheckNextVillage) {
			
			StartCoroutine ("timeOfAnimDetect");
		}

		// 2 new small thing on the map
		 
		if (isThereSomthingAround && didICheckNextVillage && !didICheckSmallThing) {
			StartCoroutine ("timeOfAnimDetect02");
		}
	}

	IEnumerator timeOfAnimDetect () {
		yield return new WaitForSeconds (1.0f);
		didICheckNextVillage = true;

		//A LANCER ICI LORSQU4ON A D2TETCER le village on relance en mettant didICheckNextVillage en, faux.
	}

	IEnumerator timeOfAnimDetect02 () {
		yield return new WaitForSeconds (1.0f);
		didICheckSmallThing = true;

		//A LANCER ICI LORSQU4ON A D2TETCER le smalltruc on relance en mettant didICheckSmallThing en, faux.
	}


	IEnumerator returnBoolAnim () {
		yield return new WaitForSeconds (0.5f);
		animBoolVillage = false;
	}

	IEnumerator waitAnimFirstVillage(){
		animBoolVillage = true;
		yield return new WaitForSeconds (0.5f);
		BlocageCube.GetComponent<VillageDecouvertBlocageCube> ().villageAnimLaunched = true;
		DS.EnigmeActiveeMortSystemOn = true;
		MainCamera.SetActive (false);
		/*Artefact = GameObject.Find ("ArtefactArcheo(Clone)");
		Artefact.SetActive (false);*/
		yield return new WaitForSeconds (8.0f);
		print ("hey");
		MainCamera.SetActive (true);
	}
}
