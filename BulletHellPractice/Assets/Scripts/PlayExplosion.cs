using UnityEngine;
using System.Collections;

public class PlayExplosion : MonoBehaviour {
	//The parent object of the geometry of this character
	public GameObject geometryParent;
	//Explision particles
	public ParticleSystem targetParticles;
	//How close the character has to be to the player for the bomb to charge up.
	public float minRange = 10.0f;
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
		GameObject collisionObj = other.gameObject;

		//If they are something that can be hurt, damage them by the damage value
		if(collisionObj.GetComponent<SimpleHealth>())
		{
			collisionObj.GetComponent<SimpleHealth>().Damage(dmgValue);
		}
		//We want it to push stuff back as the hitbox gets bigger as well
		if(collisionObj.GetComponent<Rigidbody>())
		{
			collisionObj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, GetComponent<SphereCollider>().radius);
		}

	}

	void Update()
	{


			if(GameObject.FindGameObjectWithTag("Player"))
			{
				GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
				if(Vector3.Distance(transform.position, tempPlayer.transform.position) <= minRange)
				{
					//Logic for changing the color of the model or whatever else will indicate getting closer to the explosion
					//For now just gonna be interal number going up
					
				}
			}
	}

	/**
	 * Plays the explosion animation which will increase the size of the sphere collider and plays the particle system.
	 * */
	void playExplosion()
	{
		//We will be increasing the size of the hitbox along with the explosion particles
		//This will be accomplished by making an animation and activating it here.
		if(targetParticles.isStopped)
		{
			//This line makes sure he doesn't move around from collisions after
			//explosion starts
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			targetParticles.Play();
			//This turns off the geometry of the character without deleting it so it can play the explosion.
			if(geometryParent != null)
			{
				geometryParent.SetActive(false);
			}
			if(myAnim != null)
			{
				//Note that the explode animation increases the size of the 
				myAnim.SetTrigger("explode");
			}
			
		}
	}
	/**
	 * Destroys the game object. Made public so that the animation for exploding can call the method after it has finished animating.
	 * */
	public void Die()
	{
		Destroy(gameObject);
	}

}
