﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonCamera;

public class MoveCamNoFollow : MonoBehaviour {

	Follow F;

	public float TimeBeforeBackToCam; 

	OpenCloseBook OCB;

	// Use this for initialization
	void Start () {
		OCB = GameObject.Find ("ScriptManager").GetComponent<OpenCloseBook> ();
		F = GameObject.Find ("Main Camera Main1").GetComponent<Follow> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && OCB.ImInMenuBool == false) {
			F.enabled = false;
		} else if (Input.GetButtonUp ("Fire1")) {
			StartCoroutine ("putCamBack");
		}

		if (Input.GetAxis ("Horizontal") !=0) {
			F.enabled = true;
		}
	}

	IEnumerator putCamBack (){
		yield return new WaitForSeconds (TimeBeforeBackToCam);
		F.enabled = true;
	}
}
