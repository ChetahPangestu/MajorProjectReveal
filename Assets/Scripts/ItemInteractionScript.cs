using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemInteractionScript : MonoBehaviour {

	public GameObject[] selectableItem;
	public GameObject[] interactiveItem;
	private GameObject item;
	private GameObject selectedItem;
	private string selecting;
	public GameObject ui;
	public GameObject itemHolder;
	public GameObject lights;
//	private int selected;
	public bool extraLightsOn;
	private bool interacting;
	private bool movingItem;
	private bool inTrigger;
	private float horizontalSpeed;
	private float verticalSpeed;
	private bool test;

	// Use this for initialization
	void Start () {
		
		interacting = false;
		inTrigger = false;
		ui.SetActive (false);
		horizontalSpeed = 2.0f;
		verticalSpeed = 2.0f;
//		selectableItem [0] = GameObject.FindGameObjectWithTag ("Jar");
//		selectableItem [1] = GameObject.FindGameObjectWithTag ("Coin");
//		selectableItem [2] = GameObject.FindGameObjectWithTag ("Knife");
//		selectableItem [3] = GameObject.FindGameObjectWithTag ("");
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
				if (selecting == "Jar") {
					selectedItem.SetActive (false);
					item.SetActive (true);
				}
				if (selecting == "Coin") {
					selectedItem.SetActive (false);
					item.SetActive (true);
				}
				if (selecting == "Knife") {
					selectedItem.SetActive (false);
					item.SetActive (true);
				}
			}

			//Stop interacting
			else if (Input.GetKeyDown (KeyCode.E) && interacting == true && movingItem == false) {
				Debug.LogError ("Does this");
				lights.SetActive (false);
				ui.SetActive (true);
				interacting = false;
				gameObject.GetComponent<FirstPersonController> ().enabled	= true;
				if (selecting == "Jar") {
					selectedItem.SetActive (true);
					item.SetActive (false);
				}
				if (selecting == "Coin") {
					selectedItem.SetActive (true);
					item.SetActive (false);
				}
				if (selecting == "Knife") {
					selectedItem.SetActive (true);
					item.SetActive (false);
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
				Debug.Log ("working");
				float a = horizontalSpeed * -1 * Input.GetAxis ("Mouse X");
				float s = verticalSpeed * -1 * Input.GetAxis ("Mouse Y");
				Debug.Log ("X " + a + ", "+ "Y "+ s);
				itemHolder.transform.Rotate (0, a, 0);
				item.transform.Rotate (s, 0, 0);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		inTrigger = true;
		selecting = other.tag;
		ui.SetActive (true);
		if (other.tag == "Jar") {
			item = interactiveItem [0];
			selectedItem = selectableItem [0];
		}
		else if (other.tag == "Coin") {
			item = interactiveItem [1];
			selectedItem = selectableItem [1];
		}
		else if (other.tag == "Knife") {
			item = interactiveItem [2];
			selectedItem = selectableItem [2];
		}
	}

	void OnTriggerExit(){
		inTrigger = false;
		ui.SetActive (false);
	}
}