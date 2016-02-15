using UnityEngine;
using System.Collections;

public class PlayParticleOnCollision : MonoBehaviour {

	public ParticleSystem targetParticles;

	void OnCollisionEnter(Collision other)
	{
		targetParticles.Play();
	}
}
