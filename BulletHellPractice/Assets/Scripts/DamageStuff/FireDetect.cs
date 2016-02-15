using UnityEngine;
using System.Collections;

public class FireDetect : MonoBehaviour {
	public bool canBeHit = true;
	public ParticleSystem myFireParticle;

	void OnTriggerStay(Collider theCollider)
	{
		if(theCollider.gameObject.CompareTag("Fire") && canBeHit)
		{
			canBeHit = false;
			myFireParticle.Play();
			GameObject tempFire = theCollider.gameObject;
			FireWeapon tempWpn = tempFire.GetComponentInParent<FireWeapon>();
			GetComponent<SimpleHealth>().Damage(1);
			Invoke("ResetHit", Time.deltaTime + tempWpn.bulletDelay);

		}
	}

	void ResetHit()
	{
		canBeHit = true;
		myFireParticle.Stop();
	}
}
