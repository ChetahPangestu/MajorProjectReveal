using UnityEngine;
using System.Collections;

public class FurnatureInteractionScript : MonoBehaviour {

	//set whether to swing left, right or slide foward
	public string interactionType;
	public bool colliding;
	private bool open;

	private float openAngle;

	public GameObject interactingObject;

	private float xPosition;
	private float xMoveToPosition;
	private float yPosition;
	private float zPosition;

	public AudioClip[] objectActivationSound;
	public AudioClip[] objectDeactivationSound;

	public bool opening;
	public bool closing;

	public Transform startMarker;
	public Transform endMarker;
	private float speed = 1.0f;
	public float startTime;
	public float journeyLength;

	// Use this for initialization
	void Start () {
		if (interactionType == "slide") {
			journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
		}

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
			if (Input.GetKeyDown (KeyCode.E) && open == false) {
				if (opening == false) {
					startTime = Time.time;
					opening = true;
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = objectActivationSound [0];
					audio.Play ();
				}
			}
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {
				if (closing == false) {
					startTime = Time.time;
					closing = true;
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = objectDeactivationSound [0];
					audio.Play ();
				}
			}
		}

		//Open Draws
		if (interactionType == "slide" && opening == true) {
			float distCovered = (Time.time - startTime) * (speed -0.5f);
			float fracJourney = distCovered / journeyLength;
			interactingObject.transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);
		}

		if (interactionType == "slide" && interactingObject.transform.position == endMarker.position) {
			opening = false;
			open = true;
		}

		//Close Draws
		if (interactionType == "slide" && closing == true) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			interactingObject.transform.position = Vector3.Lerp (endMarker.position, startMarker.position, fracJourney);
		}

		if (interactionType == "slide" && interactingObject.transform.position == startMarker.position) {
			closing = false;
			open = false;
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
				Debug.Log ("turns on");
				open = true;
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = objectActivationSound [1];
				audio.Play ();
			}
			//off
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {
				interactingObject.SetActive (false);
				Debug.Log ("turns off");
				open = false;
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = objectDeactivationSound [1];
				audio.Play ();
			}
		}
		colliding = false;
	}


	void CodeHolder(){
	}
}