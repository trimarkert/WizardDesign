using UnityEngine;
using System.Collections;

public class EarthWeapon : Weapon {

	public int numPulse = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void FireEarth()
	{
		for(int a = 0; a < numPulse; a++)
		{
			Invoke("Fire", bulletDelay * a);
		}
	}

	public override void Fire()
	{

	}
}
