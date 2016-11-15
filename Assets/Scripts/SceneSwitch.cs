using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

	public string myScene;

	void OnMouseDown()
	{
		Application.LoadLevel (myScene);
	}
}
