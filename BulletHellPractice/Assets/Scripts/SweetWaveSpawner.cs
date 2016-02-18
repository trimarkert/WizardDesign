using UnityEngine;
using System.Collections;

/**
 * Currently Spawns waves of enemies Randomly based on a set of spawn points.
 * Would like to expand to have differen types of waves (i.e. in sequence spawning or something along those lines.
 * */
public class SweetWaveSpawner : MonoBehaviour {

	public enum SpawnState {SPAWNING,WAITING,COUNTING};

	//Note: Serializable is needed so that you can make different versions of waves from within
	//the editor
	/**
	 * Class to describe an enemy spawn wave. Should be expanded to contain a list of spawn points
	 * for enemy objects.
	 * */
	[System.Serializable]
	public class Wave
	{
		//For developer reference and identification by other methods
		public string name;
		//The enemy object to be spawned (why does this work as a transform?
		public Transform enemy;
		//How many instances of the enemy to spawn
		public int count;
		//The delay between each individual spawn call
		public float indivSpawnDelay;

	}
	//All waves to be spawned
	public Wave[] waves;
	//Counter keeps track of next wave in list
	private int nextWave = 0;

	public Transform[] spawnPoints;

	//The time to wait between each spawn wave
	public float timeBetweenWaves = 2.0f;
	//Used to count down to zero and then start the spawning process.
	private float waveCountdown = 0f;

	//Represents the current state of spawner, defaults to doing a countdown.
	public SpawnState curState = SpawnState.COUNTING;
	//The fixed rate at which we check to see if enemies are still alive
	//relevant code in update() and enemyalive()
	private float searchCountdown = 1.0f;


	void Start()
	{
		if(spawnPoints.Length == 0)
		{
			Debug.LogError ("No Spawn points referenced.");
		}
		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{

		if(curState == SpawnState.WAITING)
		{
			//Check if there are enemies that the player has not killed yet.
			if(!EnemyIsAlive())
			{
				//begin a new wave
				WaveCompleted();

			}
			else
			{
				//This helps us skip the rest of the update function wich we now know is not relvant
				//because the state is waiting and there are still enemies to deal with.
				return;
			}

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
	/**
	 * Checks if any "Enemy" game objects are still alive.
	 * Used to know whether to move onto the next wave.
	 * */
	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(!GameObject.FindGameObjectWithTag("Enemy"))
			{
				return false;
			}
		}
		return true;
	}
	/**
	 * Executes when player finishes killing the whole round of enemies
	 * Resets appropriate states and starts the spawning over at the first wave
	 * */
	void WaveCompleted()
	{
		//reset appropriate variables to correct numbers
		curState = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if(nextWave + 1 > waves.Length -1)
		{
			nextWave = 0;
			//Possible to transition to win screen
			Debug.Log ("All Waves Complete!... looping");
		}
		else
		{
			nextWave ++;
		}
	}



	/**Spawns enemies and waits a short time in between spawning
	* should be expanded to use different spwan points so we can make
	* different shapes and what not with the spawn pattern.
	* @param _wave: The wave object that is used for spawning
	*/
	IEnumerator SpawnWave(Wave _wave)
	{
		//now we are actually spawning
		curState = SpawnState.SPAWNING;
		//spawn things based on number of enemies we want to spawn
		for(int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy (_wave.enemy);
			yield return new WaitForSeconds(_wave.indivSpawnDelay);

		}

		//Switch to wait for character to kill enemies
		curState = SpawnState.WAITING;
		yield break;
	}

	/**
	 * Spwans single enemy.
	 * 
	 * @param _enemy: The enemy object to be spawned.
	 * */
	void SpawnEnemy(Transform _enemy)
	{
		//Just a placeholder 
		//spawn enemy
		Debug.Log ("Spawning Enemy: " + _enemy.name);


		Transform spawnPnt = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate (_enemy, spawnPnt.position, spawnPnt.rotation);


	}
}
