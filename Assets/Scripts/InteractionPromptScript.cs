using UnityEngine;
using System.Collections;

public class InteractionPromptScript : MonoBehaviour {

	public Transform player;

	void Update () {
		gameObject.transform.LookAt (player);
	}
}
