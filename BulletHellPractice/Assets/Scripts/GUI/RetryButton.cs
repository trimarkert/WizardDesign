using UnityEngine;
using System.Collections;

public class RetryButton : MonoBehaviour {

	/**
	 * Loads main game demo scene.
	 * */
	public void LoadMainLevel()
	{
		Application.LoadLevel(1);
	}
}
