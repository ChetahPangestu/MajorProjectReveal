using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class SelectingItemScript : MonoBehaviour {

	//Furnature Interaction
	public bool interacting;
	private string selectedObject;
	private GameObject interactingObject;
	private float maxDetectionDistance = 5.0f;
	public GameObject defaultObject;


	private GameObject lockedDoor;
	private bool setLockedDoor;

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
			}
		}
	}

	void ObjectInteraction(){
		//Furnature
		if (selectedObject == "Lamp" || selectedObject == "Draw" || selectedObject == "Door") {
			interactingObject.transform.GetComponent<FurnatureInteractionScript> ().colliding = true;
			interacting = true;
		}
		//Locked door
		else if (selectedObject == "Locked Door") {
			lockedDoor = interactingObject;
			lockedDoor.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			setLockedDoor = true;
		} else if (selectedObject != "Locked Door" && setLockedDoor == true) {
			lockedDoor.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}

		//extra interactve items
		else if (selectedObject == "Report" || selectedObject == "Comic" || selectedObject == "Late slip" && gameObject.GetComponent<ItemInteractionScript> ().inTrigger != true)
		{
			//parent teacher
			if (selectedObject == "Report") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 0;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			//comic
			else if (selectedObject == "Comic") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 1;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
			//late slip
			else if (selectedObject == "Late slip") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 2;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
		}

		else {
			interacting = false;
			gameObject.GetComponent<ItemInteractionScript> ().inTrigger = false;
			interactingObject = defaultObject;
		}
	}
}
