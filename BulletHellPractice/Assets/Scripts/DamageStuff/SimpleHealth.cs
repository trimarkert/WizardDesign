using UnityEngine;
using System.Collections;

public abstract class SimpleHealth : MonoBehaviour {
	//The health that the spawned character starts with
	public int initialHealth = 3;
	//The amount of score this character is worth (want to add more variety than just 1 point goblins
	public int scoreValue = 1;
	//the current health of the character, only public for display reasons.
	protected int curHealth;

	protected abstract void Die();
	public abstract void Damage(int dmgAmount);


	/**
	 * Sets current health to initial health upon creation.
	 * */
	void Start () {
	
		curHealth = initialHealth;
	}

	void Update()
	{
		if(curHealth <= 0)
		{
			Die ();
		}
	}

	/**
	 * 
	 * */
	public int GetHealth(){
		return curHealth;
	}
}
