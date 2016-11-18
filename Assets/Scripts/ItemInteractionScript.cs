using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemInteractionScript : MonoBehaviour {

	public GameObject player;
	public bool inTrigger;
	public bool interacting;
	public GameObject UIExamine;
	public int selected;
	public GameObject objectHolder;
	public GameObject[] interactingObjects;


	private bool movingObject;
	private float horizontalSpeed = 2.0f;
	private float verticalSpeed = 2.0f;


	void Update () {
		
		if (inTrigger == true) {
			UIExamine.SetActive (true);
			//Start interacting
			if (Input.GetKeyDown (KeyCode.E) && interacting == false) {
				UIExamine.SetActive (false);
//				inTrigger = true;
				interacting = true;
				player.GetComponent<FirstPersonController> ().enabled = false;

				Camera.main.transform.LookAt (objectHolder.transform.position);
				interactingObjects [selected].SetActive (true);
			}

			//Stop interacting
			else if (Input.GetKeyDown (KeyCode.E) && interacting == true) {
				movingObject = false;
				UIExamine.SetActive (true);
				interacting = false;
				player.GetComponent<FirstPersonController> ().enabled = true;
				interactingObjects [selected].SetActive (false);
			}

			if (interacting == true) {
				//enable movement
				if (Input.GetButtonDown ("Fire1") && movingObject == false) {
					movingObject = true;
				}
				//disable movement
				else if (Input.GetButtonUp("Fire1") && movingObject == true) {
					movingObject = false;
				}
			}

			//move object
			if (movingObject == true) {
				float a = horizontalSpeed * -1 * Input.GetAxis ("Mouse X");
				float s = verticalSpeed * Input.GetAxis ("Mouse Y");
				interactingObjects [selected].transform.Rotate (0, a, 0);
				objectHolder.transform.Rotate (s, 0, 0);
			}
		}
	}
}