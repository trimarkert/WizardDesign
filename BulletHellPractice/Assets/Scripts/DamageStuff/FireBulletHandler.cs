using UnityEngine;
using System.Collections;

public class FireBulletHandler : MonoBehaviour {
	public int dmgValue = 1;
	
	void OnTriggerEnter(Collider collision)
	{
		
		if(collision.gameObject.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<SimpleHealth>().Damage(dmgValue);
		}
	}
	// Update is called once per frame
	void Die()
	{
		GetComponent<BoxCollider>().enabled = false;

	}
	
}
