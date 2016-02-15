using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceWeapon : Weapon {
	public GameObject bullet;
	public List<Transform> bulletSpawns;

	public override void Fire(){
		Vector3 shootDirection;
		Quaternion shootRotation;
		foreach( Transform curSpawn in bulletSpawns)
		{
			shootDirection = curSpawn.forward;
			shootRotation = Quaternion.LookRotation(shootDirection);
			Instantiate(bullet, curSpawn.position, shootRotation);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ShotControl();
	}
}
