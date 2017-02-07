﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathSystem : MonoBehaviour {

	GameObject Artefact;
	GameObject RespawnEnigme;

	public CanvasGroup ImageWhite;
	public float smoothWhiteBeginning = 0.05f;
	public bool amIAtZero = false;
	public bool amIAtMax = false;
	public float secondsBeforeDecreasing = 2.0f;
	private bool goingUp = false;
	private bool goingDown = false;

	DropCube DC;
	SanityGestion SG;

	public bool EnigmeActiveeMortSystemOn;


	// Use this for initialization
	void Start () {
		DC = GameObject.Find ("Player").GetComponent<DropCube> ();
		SG = GameObject.Find ("Player").GetComponent<SanityGestion> ();
		RespawnEnigme = GameObject.Find ("RespawnEnigmeTotem");
	}
	
	// Update is called once per frame
	void Update () {

		//dans le cas ou je lache par moi meme le cube

		if (DC.isCubeOnGround) {
			Artefact = GameObject.Find ("ArtefactRecherche(Clone)");

			if (SG.sanity <= 0) {
				StartCoroutine ("Dying");
			}
		}

		//dans le cas ou je suis dans une énigme

		if (EnigmeActiveeMortSystemOn) {
			if (SG.sanity <= 0) {
				StartCoroutine ("DyingEnigma");

			}
		}

		if (amIAtZero && goingUp) {
			ImageWhite.alpha += smoothWhiteBeginning;
		}

		if (amIAtMax && goingDown) {
			ImageWhite.alpha -= smoothWhiteBeginning;
		}

		if (ImageWhite.alpha <= 0) {
			amIAtZero = true;
		}

		if (ImageWhite.alpha >= 0) {
			amIAtMax = true;
		}
	}

	IEnumerator Dying () {
		goingUp = true;
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingUp = false;
		this.transform.position =  Artefact.transform.position + new Vector3 (1,0,1);
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingDown = true;
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingDown = false;
	}

	IEnumerator DyingEnigma () {
		goingUp = true;
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingUp = false;
		this.transform.position =  RespawnEnigme.transform.position;
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingDown = true;
		yield return new WaitForSeconds (secondsBeforeDecreasing);
		goingDown = false;
	}
}
