using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

	public GameObject spawnObj;
	public int numWaves = 2;
	public int numPerWave = 4;
	//Default is the gameObject's position but you can set it yourself aswell
	public Transform spawnPosition;
	public float spawnDelay = 1.0f;

	public float particleStartTime = 0.0f;

	void Start()
	{
		spawnPosition = gameObject.transform;
		SpawnDefault();
	}

	public void SpawnDefault()
	{
		if(GetComponent<ParticleSystem>() != null)
		{
			GetComponent<ParticleSystem>().Play();
		}
		for(int a = 0; a < numPerWave; a++)
		{
			Invoke ("SingleSpawn", spawnDelay * a + spawnDelay);
		}
	}

	void SingleSpawn()
	{
		Vector3 spawnDirection = spawnPosition.forward;
		Quaternion spawnRotation = Quaternion.LookRotation(spawnDirection);
		Instantiate(spawnObj, spawnPosition.position, spawnRotation);
	}
}
