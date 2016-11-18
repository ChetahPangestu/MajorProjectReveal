using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

	public string myScene;

	void OnMouseDown()
	{
		SceneManager.LoadScene (myScene);
	}
}
