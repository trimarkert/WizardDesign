using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon {
	public GameObject fireSpout;
	public Rigidbody dmgBox;

	//Left Blank Intentionaly
	public override void Fire()
	{
			//this is empty because Enemies detect being hit by fire themselves
	}
	
	void Update()
	{
		ShotControl();
	}


	public void TurnOnParticles()
	{
		fireSpout.GetComponent<ParticleSystem>().Play();
		fireSpout.GetComponent<BoxCollider>().enabled = true;
	}
	public void TurnOffParticles()
	{
		fireSpout.GetComponent<ParticleSystem>().Stop();
		fireSpout.GetComponent<ParticleSystem>().Clear();
		fireSpout.GetComponent<BoxCollider>().enabled = false;
	}
}
