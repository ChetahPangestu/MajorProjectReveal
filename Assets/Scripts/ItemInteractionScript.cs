using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemInteractionScript : MonoBehaviour {

	public GameObject player;
	public bool inTrigger;
	public bool interacting;
	public GameObject UIExamine;
	public GameObject InteractingCam;
	public int selected;
	public GameObject objectHolder;
	public GameObject[] interactingObjects;

	public AudioClip[] voiceLine;

	public bool movingObject;
	private float horizontalSpeed = 2.0f;
	private float verticalSpeed = 2.0f;

	public AudioClip[] itemPickUpSound;
	public AudioClip[] itemPutDownSound;

	void Update () {
		
		if (inTrigger == true) {
			
			//Start interacting
			if (Input.GetKeyDown (KeyCode.E) && interacting == false) {
				interacting = true;
				gameObject.GetComponent<Camera> ().enabled = false;
				gameObject.GetComponent<AudioListener> ().enabled = false;
				player.GetComponent<FirstPersonController> ().cameraLocked = true;
				player.GetComponent<CharacterController> ().enabled = false;
				InteractingCam.SetActive (true);
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = voiceLine [selected];
				audio.Play ();
				interactingObjects [selected].SetActive (true);

				if(selected == 5 || selected == 6 || selected == 7){
					AudioSource audioInteract = GetComponent<AudioSource>();
					audioInteract.clip = itemPickUpSound [1];
					audioInteract.Play ();
				}
			}

			//Stop interacting
			else if (Input.GetKeyDown (KeyCode.E) && interacting == true) {
				movingObject = false;
				interacting = false;
				gameObject.GetComponent<Camera> ().enabled = true;
				gameObject.GetComponent<AudioListener> ().enabled = true;
				player.GetComponent<FirstPersonController> ().cameraLocked = false;
				player.GetComponent<CharacterController> ().enabled = true;
				InteractingCam.SetActive (false);
				interactingObjects [selected].SetActive (false);


				if(selected == 5 || selected == 6 || selected == 7){
					AudioSource audioInteract = GetComponent<AudioSource>();
					audioInteract.clip = itemPutDownSound [1];
					audioInteract.Play ();
				}
			}

			if (interacting == true) {
				//enable movement
				UIExamine.SetActive(false);
				if (Input.GetButtonDown ("Fire1") && movingObject == false) {
					movingObject = true;
				}
				//disable movement
				else if (Input.GetButtonUp ("Fire1") && movingObject == true) {
					movingObject = false;
				}

				if (Input.GetKeyDown (KeyCode.Q)) {
					movingObject = false;
				}
			}

			//move object
			if (movingObject == true) {
				float a = horizontalSpeed * -1 * Input.GetAxis ("Mouse X");
				float s = verticalSpeed * Input.GetAxis ("Mouse Y");
				interactingObjects [selected].transform.Rotate (s, 0, 0);
				objectHolder.transform.Rotate (0, a, 0);
			}
		}
	}
}