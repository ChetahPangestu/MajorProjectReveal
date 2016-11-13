using UnityEngine;
using System.Collections;

public class FurnatureInteractionScript : MonoBehaviour {

	//set whether to swing left, right or slide foward
	public string moveType;
	private bool colliding;
	private bool open;

	private float openAngle;

	public GameObject hinge;
//	public GameObject UI;


	// Use this for initialization
	void Start () {
		if (moveType == "left") {
			openAngle = 80.0f;
		} else if (moveType == "right") {
			openAngle = -80.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (moveType != "slide") {
			//open
			if (Input.GetKeyDown (KeyCode.E) && colliding == true && open == false) {
				hinge.transform.eulerAngles = new Vector3 (0, openAngle, 0);
				open = true;
			}
			//close
			else if (Input.GetKeyDown (KeyCode.E) && colliding == true && open == true) {
				hinge.transform.eulerAngles = new Vector3 (0, 0, 0);
				open = false;
			}
		}
	}
	void OnTriggerEnter(){
		colliding = true;
	}

	void OnTriggerExit(){
		colliding = false;
	}
}
