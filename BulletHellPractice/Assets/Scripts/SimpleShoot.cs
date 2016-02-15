using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleShoot : MonoBehaviour {
	public GameObject bullet;
	public List<Transform> bulletSpawns;
	//how fast the bullets travel after being shot
	public float bulletDelay = 4.0f;

	private bool canShoot = true;
	// Update is called once per frame
	void Update () {
		if(canShoot)
		{
			Vector3 shootDirection;
			Quaternion shootRotation;
			foreach( Transform curSpawn in bulletSpawns)
			{
				shootDirection = curSpawn.forward;
				shootRotation = Quaternion.LookRotation(shootDirection);
				Instantiate(bullet, curSpawn.position, shootRotation);
			}
			canShoot = false;
			Invoke ("ResetShot", bulletDelay);
		}
	}

	void ResetShot()
	{
		canShoot = true;
	}
}
