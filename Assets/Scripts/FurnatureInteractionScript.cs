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

	public AudioClip objectActivationSound;
	public AudioClip objectDeactivationSound;

	public GameObject UIHolder;
	public GameObject activateUI;
	public GameObject deactivateUI;

	private bool opening;
	private bool closing;

	public Transform startMarker;
	public Transform endMarker;
	private float speed = 1.0f;
	private float startTime;
	private float journeyLength;

	public float smooth = 1f;
	private Quaternion openRotation;
	private Quaternion closeRotation;

	// Use this for initialization
	void Start () {
		if (interactionType == "slide") {
			journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
		}

		if (interactionType == "turn left") {
			openAngle = 180.0f;
			openRotation *=  Quaternion.AngleAxis(60, Vector3.up);
		} else if (interactionType == "turn right") {
			openAngle = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (colliding == true) {
			UIHolder.SetActive (true);
		} else if (colliding == false) {
			UIHolder.SetActive (false);
		}


		//Drawers
		if (interactionType == "slide") {
			if (colliding == true) {
				if (Input.GetKeyDown (KeyCode.E) && open == false) {
					if (opening == false) {
						startTime = Time.time;
						opening = true;
						AudioSource audio = GetComponent<AudioSource> ();
						audio.clip = objectActivationSound;
						audio.Play ();
					}
				} else if (Input.GetKeyDown (KeyCode.E) && open == true) {
					if (closing == false) {
						startTime = Time.time;
						closing = true;
						AudioSource audio = GetComponent<AudioSource> ();
						audio.clip = objectDeactivationSound;
						audio.Play ();
					}
				}

				if (open == false) {
					activateUI.SetActive (true);
					deactivateUI.SetActive (false);
				} else if (open == true) {
					activateUI.SetActive (false);
					deactivateUI.SetActive (true);
				}
			}

			//Open Drawers
			if (opening == true) {
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLength;
				interactingObject.transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);
			}

			if (interactingObject.transform.position == endMarker.position) {
				opening = false;
				open = true;
			}

			//Close Drawers
			if (closing == true) {
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLength;
				interactingObject.transform.position = Vector3.Lerp (endMarker.position, startMarker.position, fracJourney);
			}

			if (interactingObject.transform.position == startMarker.position) {
				closing = false;
				open = false;
			}
		}


		//Cubbards
		else if ((interactionType == "turn left" || interactionType == "turn right")) {
			if (colliding == true) {
				if (Input.GetKeyDown (KeyCode.E) && open == false) {
					if (opening == false) {
//						startTime = Time.time;
						opening = true;
//						AudioSource audio = GetComponent<AudioSource> ();
//						audio.clip = objectActivationSound;
//						audio.Play ();
					}
				} else if (Input.GetKeyDown (KeyCode.E) && open == true) {
					if (closing == false) {
						startTime = Time.time;
						closing = true;
//						AudioSource audio = GetComponent<AudioSource> ();
//						audio.clip = objectDeactivationSound;
//						audio.Play ();
					}
				}

				if (open == false) {
					activateUI.SetActive (true);
					deactivateUI.SetActive (false);
				} else if (open == true) {
					activateUI.SetActive (false);
					deactivateUI.SetActive (true);
				}
			}


			//Open Drawers
			if (opening == true) {
				transform.rotation= Quaternion.Lerp (transform.rotation, openRotation , 10 * smooth * Time.deltaTime);
			}

			if (interactingObject.transform.rotation == openRotation) {
				opening = false;
				open = true;
			}

			//Close Drawers
			if (closing == true) {
				
			}

			if (interactingObject.transform.position == startMarker.position) {
				closing = false;
				open = false;
			}
		}

		//Lights
		else if (interactionType == "switch" && colliding == true) {
			//on
			if (Input.GetKeyDown (KeyCode.E) && open == false) {
				interactingObject.SetActive (true);
				open = true;
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = objectActivationSound;
				audio.Play ();
			}
			//off
			else if (Input.GetKeyDown (KeyCode.E) && open == true) {
				interactingObject.SetActive (false);
				open = false;
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = objectDeactivationSound;
				audio.Play ();
			}
			if (open == false) {
				activateUI.SetActive (true);
				deactivateUI.SetActive (false);
			} else if (open == true) {
				activateUI.SetActive (false);
				deactivateUI.SetActive (true);
			}
		}
		colliding = false;
	}

	void CodeHolder(){
		//open
		if (Input.GetKeyDown (KeyCode.E) && open == false) {
			
			open = true;
		}
		//close
		else if (Input.GetKeyDown (KeyCode.E) && open == true) {
			interactingObject.transform.eulerAngles = new Vector3 (0, 90.0f, 0);
			open = false;
		}

		if (open == false) {
			activateUI.SetActive (true);
			deactivateUI.SetActive (false);
		} else if (open == true) {
			activateUI.SetActive (false);
			deactivateUI.SetActive (true);
		}
	}
}