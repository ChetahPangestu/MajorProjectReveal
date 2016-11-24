using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class SelectingItemScript : MonoBehaviour {

	//Furnature Interaction
	public bool interacting;
	public GameObject UIActivate;
	private string selectedObject;
	private GameObject interactingObject;
	private float maxDetectionDistance = 5.0f;
	public GameObject defaultObject;

	//FSM bool

//	public FsmBool machine1 = FsmVariables.GlobalVariables.FindFsmBool ("'3DSpace - Arcade 1 Activated");
//	public FsmBool machine2 = FsmVariables.GlobalVariables.FindFsmBool ("'3DSpace - Arcade 2 Activated");
//	public FsmBool machine3 = FsmVariables.GlobalVariables.FindFsmBool ("'3DSpace - Arcade 3 Activated");
//	public FsmBool machine4 = FsmVariables.GlobalVariables.FindFsmBool ("'3DSpace - Arcade 4 Activated");

	void Start(){
//		machine1.Value = true;
//		machine2.Value = true;
//		machine3.Value = true;
//		machine4.Value = true;
	}

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
//				if (interacting == true && gameObject.GetComponent<ItemInteractionScript> ().UIExamine.activeInHierarchy == false) {
//					UIActivate.SetActive (true);
//				} else {
//					UIActivate.SetActive (false);
//				}

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

			//machine 1
			if (selectedObject == "Book") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 0;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
//				machine1.Value = true;
			}

			//machine 2
			else if (selectedObject == "Token") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 1;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
//				machine2.Value = true;
			}

			//machine 3
			else if (selectedObject == "Jar") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 2;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
//				machine3.Value = true;
			}

			//machine 4
			else if (selectedObject == "Knife") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 3;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
//				machine4.Value = true;
			}

			//machine 5
			else if (selectedObject == "Umbrella") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 4;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}

			//extra: parent teacher
			else if (selectedObject == "Report") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 5;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}

			//extra: comic
			else if (selectedObject == "Comic") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 6;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}

			//extra: late slip
			else if (selectedObject == "Late slip") {
				gameObject.GetComponent<ItemInteractionScript> ().selected = 7;
				gameObject.GetComponent<ItemInteractionScript> ().inTrigger = true;
			}
		}

		else {
			interacting = false;
			gameObject.GetComponent<ItemInteractionScript> ().inTrigger = false;
			gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (false);
//			UIActivate.SetActive (false);
			interactingObject = defaultObject;
		}
	}
}
