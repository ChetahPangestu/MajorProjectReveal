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
		else if (selectedObject == "Report" || selectedObject == "Comic" || selectedObject == "Late slip" && gameObject.GetComponent<ItemInteractionScript> ().inTrigger != true)
		{
			gameObject.GetComponent<ItemInteractionScript> ().UIExamine.SetActive (true);

			//extra: parent teacher
			if (selectedObject == "Report") {
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
			interactingObject = defaultObject;
		}
	}
}
