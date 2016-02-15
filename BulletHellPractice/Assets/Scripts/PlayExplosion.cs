using UnityEngine;
using System.Collections;

public class PlayExplosion : MonoBehaviour {
	//Explision particles
	public ParticleSystem targetParticles;
	//How hard the explosion hits
	public float explosionForce = 10.0f;
	//Not sure how this one works yet
	public float explosionRadius = 10.0f;
	//The animator that will increase the size of the hitbox
	public Animator myAnim;
	//how much damage explosions deal to things hit
	//will most likely stay 1 unless we want it high to insta death.
	public int dmgValue = 1;

	void OnCollisionEnter(Collision other)
	{
		//We will be increasing the size of the hitbox along with the explosion particles
		//This will be accomplished by making an animation and activating it here.
		if(targetParticles.isStopped)
		{
			//This line makes sure he doesn't move around from collisions after
			//explosion starts
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			targetParticles.Play();
			if(myAnim != null)
			{
				myAnim.SetTrigger("explode");
			}

		}
		//If they are something that can be hurt, damage them by the damage value
		if(other.gameObject.GetComponent<SimpleHealth>())
		{
			other.gameObject.GetComponent<SimpleHealth>().Damage(dmgValue);
		}
		//We want it to push stuff back as the hitbox gets bigger as well


	}
	public void Die()
	{
		Destroy(gameObject);
	}

}
