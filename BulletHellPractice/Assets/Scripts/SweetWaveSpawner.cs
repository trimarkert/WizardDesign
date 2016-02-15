using UnityEngine;
using System.Collections;

public class SweetWaveSpawner : MonoBehaviour {

	public enum SpawnState {SPAWNING,WAITING,COUNTING};

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;

	}

	public Wave[] waves;
	private int nextWave = 0;

	//The time to wait between each spawn wave
	public float timeBetweenWaves = 2.0f;
	//Used to count down to zero and then start the spawning process.
	public float waveCountdown = 0f;

	//Represents the current state of spawner, defaults to doing a countdown.
	public SpawnState curState = SpawnState.COUNTING;

	void Start()
	{
		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{

		if(curState == SpawnState.WAITING)
		{
			//check if enemies are still alive

		}


		if(waveCountdown <= 0)
		{
			if(curState != SpawnState.SPAWNING)
			{
				//start spawing here
				StartCoroutine(SpawnWave (waves[nextWave]));

			}

		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	bool EnemyIsAlive()
	{
		if(!GameObject.FindGameObjectWithTag("Enemy"))
		{
			return false;
		}

		return true;
	}



	//Method that spawns enemies and waits a short time in between spawning
	//should be expanded to use different spwan points so we can make
	//different shapes and what not with the spawn pattern
	IEnumerator SpawnWave(Wave _wave)
	{
		//now we are actually spawning
		curState = SpawnState.SPAWNING;
		//spawn things based on number of enemies we want to spawn
		for(int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy (_wave.enemy);
			yield return new WaitForSeconds(1f/ _wave.rate);

		}

		//Switch to wait for character to kill enemies
		curState = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		//Just a placeholder 
		//spawn enemy
		Debug.Log ("Spawning Enemy: " + _enemy.name);

	}
}
