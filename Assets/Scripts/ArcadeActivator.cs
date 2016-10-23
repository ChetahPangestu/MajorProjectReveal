using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters;

public class ArcadeActivator : MonoBehaviour {

	public GameObject arcadeGame;
	public GameObject UI;
	public GameObject arcadeCamera;
	public GameObject player;
	private bool inTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (inTrigger == true) {
			if (Input.GetKeyDown (KeyCode.E)) {
//				Debug.LogError ("Works");
				UI.SetActive (false);
				player.SetActive (false);
				arcadeCamera.SetActive (true);
//				arcadeGame.SetActive (true);
				gameObject.SetActive (false);
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Arcade") {
			UI.SetActive (true);
			inTrigger = true;
		}
	}
	void OnTriggerExit(){
		UI.SetActive (false);
		inTrigger = false;
	}
}
