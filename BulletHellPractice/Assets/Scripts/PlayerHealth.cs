using UnityEngine;
using System.Collections;

public class PlayerHealth : SimpleHealth 
{
	//The sound that plays when the player is hurt.
	public AudioSource ouchNoise;

	/**
	 * Overrides method to apply damage. Also, plays noise to indicate damage.
	 * */
	public override void Damage (int dmgAmount)
	{
		curHealth -= dmgAmount;
		if(ouchNoise != null)
		{
			ouchNoise.Play();
		}

	}
	protected override void Die ()
	{
		Destroy(gameObject);
	}
}
