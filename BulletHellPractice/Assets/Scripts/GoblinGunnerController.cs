﻿using UnityEngine;
using System.Collections;

public class GoblinGunnerController : MonoBehaviour {
	//rate at which character rotates toward the player
	public float rotSpeed = 1.0f;
	//How close to the player the character must be to start shooting.
	public float activationDist = 10.0f;
	//How long the character should keep shooting for once in range
	public float gunnerTime = 10.0f;

	private bool isShooting = false;
	//How long the character should shoot for before going back to moving.
	private float timeToShoot = 5.0f;
	//How fast the character should move forward
	public float movSpeed = 10.0f;
	/**
	 * Follows the player at a lazy rotation speed. Once within a certain distance, character stops moving and starts
	 * shooting for a certain amount of time.
	 * */
	void Update()
	{
		//Check if there is a player object
		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			//Rotate toward the player.
			transform.rotation = Quaternion.Slerp(transform.rotation, 
			                                      Quaternion.LookRotation(player.transform.position - transform.position), 
			                                      rotSpeed * Time.deltaTime);
			if(!isShooting && Vector3.Distance(transform.position, player.transform.position) <= activationDist)
			{
				StartCoroutine(Shooting());
			}
			else if(!isShooting)
			{
				transform.position += transform.forward * movSpeed * Time.deltaTime;
			}
		}
	}
	/**
	 * Executes when the character starts shooting at the player instead of moving toward them.
	 * Turns Basic Shoot on so it will shoot at a constant rate.
	 * */
	IEnumerator Shooting()
	{
		isShooting = true;
		SimpleShoot mySimpleShoot = GetComponent<SimpleShoot>();
		if(mySimpleShoot != null && mySimpleShoot.enabled == false)
		{
			mySimpleShoot.enabled = true;
		}

		yield return new WaitForSeconds(timeToShoot);
		mySimpleShoot.enabled = false;
		isShooting = false;
		yield break;
	}
}
