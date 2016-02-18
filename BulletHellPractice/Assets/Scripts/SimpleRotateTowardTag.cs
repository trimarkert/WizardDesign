using UnityEngine;
using System.Collections;

public class SimpleRotateTowardTag : MonoBehaviour {
	//Rate at which character rotates toward the player
	public float homeingSpeed = 1.0f;
	//The Tag of the object to be followed
	public string targetTag = "Player";
	// Update is called once per frame
	void Update () {
		//Check if there is a player object
		if(GameObject.FindGameObjectWithTag(targetTag) != null)
		{
			GameObject player = GameObject.FindGameObjectWithTag(targetTag);
			//Rotate toward the player.
			transform.rotation = Quaternion.Slerp(transform.rotation, 
			                                      Quaternion.LookRotation(player.transform.position - transform.position), 
			                                      homeingSpeed * Time.deltaTime);
		}
	}
}
