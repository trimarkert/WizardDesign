using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public string leftStickHorizAxis = "Horizontal";
	public string leftStickVertAxis = "Vertical";
	public string rightStickHorizAxis = "Horizontal2";
	public string rightStickVertAxis = "Vertical2";
	public float speed = 10.0f;
	public string weapon = "";
	public float damageDelay = 2.0f;
	public ParticleSystem shield;
	public GameObject fireIcon;
	public GameObject iceIcon;

	private bool canBeHit = true;


	// Use this for initialization
	void Start () 
	{
		fireIcon = GameObject.Find("FireIcon");
		iceIcon = GameObject.Find("IceIcon");
	}
	
	// Update is called once per frame
	void Update () 
	{

		//logic to determine what weapon to use
		if(Input.GetButtonDown("Fire1") && !weapon.Equals("Fire"))
		{
			weapon = "Fire";
			//turn fire particles on
			GetComponent<FireWeapon>().enabled = true;
			GetComponent<FireWeapon>().TurnOnParticles();
			//make the fire icon bright
			fireIcon.GetComponent<WeaponIcon>().EnableWeapon();
			//turn ice off
			GetComponent<IceWeapon>().enabled = false;
			//make the ice icon dim
			iceIcon.GetComponent<WeaponIcon>().DisableWeapon();

		}
		else if(Input.GetButtonDown ("Fire2") && !weapon.Equals("Ice"))
		{
			weapon = "Ice";
			//turn Ice on
			GetComponent<IceWeapon>().enabled = true;
			//make the ice icon bright
			iceIcon.GetComponent<WeaponIcon>().EnableWeapon();

			//turn fire particles off
			GetComponent<FireWeapon>().enabled = false;
			GetComponent<FireWeapon>().TurnOffParticles();
			//make the fire icon dim
			fireIcon.GetComponent<WeaponIcon>().DisableWeapon();
		}


		//logic to aim and move character based on controller input
		transform.position += Vector3.right * Input.GetAxis(leftStickHorizAxis)* speed * Time.deltaTime;

		transform.position += Vector3.forward * Input.GetAxis(leftStickVertAxis) * speed * Time.deltaTime;
		Vector3 lookDirection = Vector3.right * Input.GetAxis(rightStickHorizAxis) + Vector3.forward * (- Input.GetAxis(rightStickVertAxis));
		if(!lookDirection.Equals(Vector3.zero))
		{
			transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
		}
	}
	//So far this is used to see if the enemy has hit the player.
	void OnCollisionEnter(Collision collision)
	{
		//Damage logic
		if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
		{
			if(canBeHit)
			{
				canBeHit = false;
				//failsafe against not having simpleHealth for some reason
				if(gameObject.GetComponent<SimpleHealth>())
				{
					//Damage character by 1, may be updated to be based on attack damage
					gameObject.GetComponent<SimpleHealth>().Damage(1);
					//play invilnerability animation
					invulnerability();
				}
			}
		}
	}

	//Makes the character invulnerable for a set amount of time damagedelay.
	void invulnerability()
	{
		shield.Play();
		//Makes it so the player can get hit again
		Invoke("ResetHit", damageDelay);

	}

	void ResetHit()
	{
		canBeHit = true;
	}
}
