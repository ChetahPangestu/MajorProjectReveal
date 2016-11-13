using UnityEngine;
using System.Collections;

public class ArcadeMachineActivator : MonoBehaviour {

	public GameObject[] arcadeMachinesActive;
	public GameObject[] arcadeMachinesDeactive;
	private bool knifeActivated;
	private bool tokenActivated;
	private bool jarActivated;
	private bool umbrellaActivated;
	private bool colliding;

	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			Debug.LogError ("");
		}

		if (Input.GetKeyDown (KeyCode.E) && colliding == true && jarActivated == true) {
			arcadeMachinesActive [0].SetActive (true);
			arcadeMachinesDeactive [0].SetActive (false);
		}
		else {
			arcadeMachinesActive [0].SetActive (false);
			arcadeMachinesDeactive [0].SetActive (true);
		}
	}
		
	void OnTriggerEnter(Collider other){
//		Debug.Log ("colliding with " + other.tag.ToString());
		if (jarActivated == false && other.name == "ExaminableObject - Jar") {
			jarActivated = true;
			colliding = true;
		} else {
			colliding = false;
		}
	}

	void OnTriggerExit(){
		colliding = false;
	}
}