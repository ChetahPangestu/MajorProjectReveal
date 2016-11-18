﻿using UnityEngine;
using System.Collections;

public class SelectingItemScript : MonoBehaviour {

	//Furnature Interaction
	public bool interacting;
	public GameObject UIActivate;
	private string selectedObject;
	public GameObject interactingObject;
	private float maxDetectionDistance = 5.0f;
	public GameObject defaultObject;

	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rch;

		if(Physics.Raycast(ray, out rch))
		{
			selectedObject = rch.transform.tag;
			interactingObject = rch.transform.gameObject;

			//Check to see if in range
			if (rch.distance < maxDetectionDistance) {
				ObjectInteraction ();
			} else {
				interacting = false;
				interactingObject = defaultObject;
			}

			//Object interaction control
			if (gameObject.GetComponent<ItemInteractionScript> ().interacting == true) {
				interactingObject.SetActive (false);
			} else {
				interactingObject.SetActive (true);
			}

			//UI Activate control
			if (interacting == true && interactingObject != defaultObject) {
				UIActivate.SetActive (true);
			} else {
				UIActivate.SetActive (false);
			}
		}
	}

	void ObjectInteraction(){
		//Furnature
		if (selectedObject == "Lamp" || selectedObject == "Draw" || selectedObject == "Door") {
			interactingObject.transform.GetComponent<FurnatureInteractionScript> ().colliding = true;
			interacting = true;
		}
		//Plot Objects
		else if (selectedObject == "Jar" || selectedObject == "Diary" || selectedObject == "Token" || selectedObject == "Knife" || selectedObject == "Umbrella") {
			gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			if (selectedObject == "Jar") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 0;
			}
			else if (selectedObject == "Diary") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 1;
			}
			else if (selectedObject == "Token") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 2;
			}
			else if (selectedObject == "Knife") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 3;
			}
			else if (selectedObject == "Umbrella") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 4;
			}
		}

		else {
			interacting = false;
			gameObject.GetComponent<ItemInteractionScript> ().inTrigger = false;
			interactingObject = defaultObject;
		}
	}
}
