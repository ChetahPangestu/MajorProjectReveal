using UnityEngine;
using System.Collections;

public class FurnatureInteractionScript : MonoBehaviour {

	//set whether to swing left, right or slide foward
	public string interactionType;
	public bool colliding;
	private bool open;

	private float openAngle;

	public GameObject interactingObject;
	public GameObject moveFromPoint;
	public GameObject moveToPoint;

	private float xPosition;
	private float xMoveToPosition;
	private float yPosition;
	private float zPosition;

	private Vector3 startingPosition;
	private Vector3 moveToPosition;

	private float startTime;
	private float journeyLength;
	private float speed = 0.001F;
	public float speedActual;


	// Use this for initialization
	void Start () {
		speedActual = speed * Time.deltaTime;

		if (interactionType == "turn left") {
			openAngle = 180.0f;
		} else if (interactionType == "turn right") {
			openAngle = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//Draws
		if (interactionType == "slide" && colliding == true) {
			//open
			if (Input.GetKeyDown (KeyCode.E) && open == false) {

				interactingObject.transform.position = Vector3.Lerp(moveFromPoint.transform.position, moveToPoint.transform.position, speed * Time.deltaTime);

				open = true;
			}
			//close
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {

				interactingObject.transform.position = Vector3.Lerp(moveToPoint.transform.position, moveFromPoint.transform.position, speed * Time.deltaTime);

				open = false;
			}
		}

		//Cubbards
		else if ((interactionType == "turn left" || interactionType == "turn right") && colliding == true) {
			//open
			if (Input.GetKeyDown (KeyCode.E) && open == false) {
				interactingObject.transform.eulerAngles = new Vector3 (0, openAngle, 0);
				open = true;
			}
			//close
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {
				interactingObject.transform.eulerAngles = new Vector3 (0, 90.0f, 0);
				open = false;
			}
		}

		//Lights
		else if (interactionType == "switch" && colliding == true) {
			//on
			if (Input.GetKeyDown (KeyCode.E) && open == false) {
				interactingObject.SetActive (true);
				open = true;
			}
			//off
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {
				interactingObject.SetActive (false);
				open = false;
			}
		}
		colliding = false;
	}
}