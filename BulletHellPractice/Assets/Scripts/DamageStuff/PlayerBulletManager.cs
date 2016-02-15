using UnityEngine;
using System.Collections;

public class PlayerBulletManager : MonoBehaviour {
	public float deathTime = 10.0f;
	public int dmgValue = 1;
	//keep the object alive just long enough to play animation and then delete.
	public float explodeTime = 0.5f;
	// Use this for initialization
	void Start () {
		Invoke("Die", deathTime);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		
		if(collision.gameObject.CompareTag("Enemy"))
		{
			//turn off everything but the particle system before destroying
			GetComponent<SimpleMoveForward>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			GetComponentInChildren<MeshRenderer>().enabled = false;
			GetComponentInChildren<ParticleSystem>().Play();

			//this is weak code, should be more general than that somehow
			if(collision.gameObject.GetComponent<SimpleHealth>())
			{
				collision.gameObject.GetComponent<SimpleHealth>().Damage(dmgValue);
			}
			Invoke("Die", explodeTime);
		}
		else
		{
			Die ();
		}
	}

	void OnBecameInvisible()
	{
		Die ();
	}
	// Update is called once per frame
	void Die()
	{
		Destroy(gameObject);
	}

}
