using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour 
{
	public GameObject playerRef;

	/*s
	 * Simple script designed to manage the health information of the player
	 * Should display health someway on the GUI
	 */
	void LateUpdate () 
	{
		//As long as a player is in the scene you should find it and set the health
		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			playerRef = GameObject.FindGameObjectWithTag("Player");
			//also have to make sure simple health is enabled
			if(playerRef.GetComponent<SimpleHealth>() != null)
			{
				GetComponent<Text>().text = "Cur Health: " 
					+ playerRef.GetComponent<SimpleHealth>().curHealth.ToString();
			}
		}
			else
			{
				//Just a nice visualization if it can't find a player
				GetComponent<Text>().text = "Cur Health: 666";
			}
	}
}