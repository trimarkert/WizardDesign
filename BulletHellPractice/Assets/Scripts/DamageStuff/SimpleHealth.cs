using UnityEngine;
using System.Collections;

public class SimpleHealth : MonoBehaviour {
	//The health that the spawned character starts with
	public int initialHealth = 3;
	//the current health of the character, only public for display reasons.
	public int curHealth;
	//The amount of score this character is worth (want to add more variety than just 1 point goblins
	public int scoreValue = 1;

	// initialization stuff
	void Start () {
	
		curHealth = initialHealth;
	}
	
	// Anyone can call this when they damage the character
	public void Damage (int dmgAmount) {

		curHealth -= dmgAmount;
		if(curHealth <=0)
		{
			Die();
		}
	
	}

	//Kills the instance and adds to the score
	void Die()
	{

		if(gameObject.CompareTag("Enemy"))
		{
			ScoreManager.managerInstance.score += scoreValue;
		}
		Destroy(gameObject);
	}
}
