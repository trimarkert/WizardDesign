using UnityEngine;
using System.Collections;

public class SimpleHealth : MonoBehaviour {
	//The health that the spawned character starts with
	public int initialHealth = 3;
	//The amount of score this character is worth (want to add more variety than just 1 point goblins
	public int scoreValue = 1;
	//the current health of the character, only public for display reasons.
	private int curHealth;


	/**
	 * Sets current health to initial health upon creation.
	 * */
	void Start () {
	
		curHealth = initialHealth;
	}
	
	/**
	 * Executes when something damages the character.
	 * */
	public void Damage (int dmgAmount) {

		curHealth -= dmgAmount;
		if(curHealth <=0)
		{
			Die();
		}
	
	}

	/**
	 * Updates appropriate values before removing character.
	 * */
	void Die()
	{

		if(gameObject.CompareTag("Enemy"))
		{
			ScoreManager.managerInstance.score += scoreValue;
		}
		Destroy(gameObject);
	}
	/**
	 * 
	 * */
	public int GetHealth(){
		return curHealth;
	}
}
