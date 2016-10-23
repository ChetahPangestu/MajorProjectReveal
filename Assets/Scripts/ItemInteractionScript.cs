using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemInteractionScript : MonoBehaviour {

	public GameObject[] interactiveItem;
	private GameObject item;
	private GameObject selectedItem;
	private string selecting;
	public GameObject ui;
	public GameObject itemHolder;
	public GameObject lights;
	private int selected;
	public bool extraLightsOn;
	private bool interacting;
	private bool movingItem;
	private bool inTrigger;
	private float horizontalSpeed;
	private float verticalSpeed;
	private bool test;
	private GameObject testObject;

	// Use this for initialization
	void Start () {
		
		interacting = false;
		inTrigger = false;
		ui.SetActive (false);
		horizontalSpeed = 2.0f;
		verticalSpeed = 2.0f;
	}

	// Update is called once per frame
	void Update () {
		
		if (inTrigger == true) {
			//Start interacting
			if (Input.GetKeyDown (KeyCode.E) && interacting == false) {
				if (extraLightsOn == true) {
					lights.SetActive (true);
				}
				ui.SetActive (false);
				interacting = true;
				gameObject.GetComponent<FirstPersonController> ().enabled	= false;

				Camera.main.transform.LookAt (itemHolder.transform.position);

				if (selecting == "Jar") {
					testObject.SetActive (false);
					interactiveItem [selected].SetActive (true);
				}
				if (selecting == "Coin") {
					testObject.SetActive (false);
					interactiveItem [selected].SetActive (true);
				}
				if (selecting == "Knife") {
					testObject.SetActive (false);
					interactiveItem [selected].SetActive (true);
				}
			}

			//Stop interacting
			else if (Input.GetKeyDown (KeyCode.E) && interacting == true) {
				movingItem = false;
				lights.SetActive (false);
				ui.SetActive (true);
				interacting = false;
				gameObject.GetComponent<FirstPersonController> ().enabled	= true;

				if (selecting == "Jar") {
					testObject.SetActive (true);
					interactiveItem [selected].SetActive (false);
				}
				if (selecting == "Coin") {
					testObject.SetActive (true);
					interactiveItem [selected].SetActive (false);
				}
				if (selecting == "Knife") {
					testObject.SetActive (true);
					interactiveItem [selected].SetActive (false);
				}
			}

			if (interacting == true) {
				if (Input.GetKeyDown (KeyCode.Q) && movingItem == false) {
					movingItem = true;
				}
				else if (Input.GetKeyDown (KeyCode.Q) && movingItem == true) {
					movingItem = false;
				}
			}

			if (movingItem == true) {
				float a = horizontalSpeed * -1 * Input.GetAxis ("Mouse X");
				float s = verticalSpeed * Input.GetAxis ("Mouse Y");
				interactiveItem [selected].transform.Rotate (0, a, 0);
				itemHolder.transform.Rotate (s, 0, 0);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		inTrigger = true;
		selecting = other.tag;
		ui.SetActive (true);
		if (other.tag == "Jar") {
			testObject = GameObject.FindGameObjectWithTag (other.tag);
			selected = 0;
		}
		else if (other.tag == "Coin") {
			testObject = GameObject.FindGameObjectWithTag (other.tag);
			selected = 1;
		}
		else if (other.tag == "Knife") {
			testObject = GameObject.FindGameObjectWithTag (other.tag);
			selected = 2;
		}
	}

	void OnTriggerExit(){
		inTrigger = false;
		ui.SetActive (false);
	}
}