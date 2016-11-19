using UnityEngine;
using System.Collections;

public class SelectingItemScript : MonoBehaviour {

	//Furnature Interaction
	public bool interacting;
	public GameObject UIActivate;
	private string selectedObject;
	private GameObject interactingObject;
	private float maxDetectionDistance = 5.0f;
	public GameObject defaultObject;

	void Update () {
		
		if (gameObject.GetComponent<Camera>().isActiveAndEnabled == true) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rch;
		
			if (Physics.Raycast (ray, out rch)) {
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
				if (interacting == true && gameObject.GetComponent<ItemInteractionScript> ().UIExamine.activeInHierarchy == false) {
					UIActivate.SetActive (true);
				} else {
					UIActivate.SetActive (false);
				}

				//UI Examine control
				if (gameObject.GetComponent<ItemInteractionScript> ().inTrigger == true && UIActivate.activeInHierarchy == false) {
					gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (true);
				} else {
					gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (false);
				}
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
		else if (selectedObject == "Jar" || selectedObject == "Book" || selectedObject == "Token" || selectedObject == "Knife" || selectedObject == "Umbrella" && gameObject.GetComponent<ItemInteractionScript> ().inTrigger != true)
		{
			gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (true);
			if (selectedObject == "Book") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 0;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			else if (selectedObject == "Token") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 1;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			else if (selectedObject == "Jar") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 2;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			else if (selectedObject == "Knife") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 3;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			else if (selectedObject == "Umbrella") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 4;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
		}

		else {
			interacting = false;
			gameObject.GetComponent<ItemInteractionScript> ().inTrigger = false;
			gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (false);
			UIActivate.SetActive (false);
			interactingObject = defaultObject;
		}
	}
}
