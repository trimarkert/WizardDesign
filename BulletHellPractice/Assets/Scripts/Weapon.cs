using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Weapon : MonoBehaviour {
	//Represents the amount of time between fires.
	public float bulletDelay = 4.0f;
	
	private bool canShoot = true;

	//should shoot whatever bullets need shootin'
	public abstract void Fire();

	public void ShotControl()
	{
		if(canShoot)
		{
			Fire();
			canShoot = false;
			Invoke("ResetShot", bulletDelay);
		}
	}
	public void ResetShot(){
		canShoot = true;
	}
}
