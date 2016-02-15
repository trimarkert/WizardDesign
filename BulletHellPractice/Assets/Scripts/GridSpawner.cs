using UnityEngine;
using System.Collections;

public class GridSpawner : MonoBehaviour {
	public GameObject spawnTarget;
	public Transform gridOrigin;

	private Vector3 tempSpawnPoint;

	/* Function spawns the target in an "X" formation on the game board, waiting in between spawns
	 * rightFirst used to decide what side of the "X" to do first
	 * halfTime how long each half of the X will take to finish spawning.
	 * numSpawns denotes how many targets should be spawned in each part of the "X"
	*/
	public void SpawnXFormation(bool rightFirst, float halfTime, int numSpawns)
	{
		float timeInterval = halfTime / (float)numSpawns;
		float horizInterval = 100.0f/ (float)numSpawns;
		float vertInterval = 100.0f/ (float)numSpawns;

		for(int curIndex = 0; curIndex > numSpawns; curIndex++)
		{
			float curX = horizInterval * curIndex;
		}
	}

	public void SpawnAtOffset(float x, float y, float z, float time)
	{
		Vector3 offsetVector = new Vector3(x,y,z);
		Vector3 gridOriginPos = gridOrigin.position;
		tempSpawnPoint = offsetVector + gridOriginPos;
		Invoke ("Spawn", time);
	}

	void Spawn()
	{
		Instantiate(spawnTarget, tempSpawnPoint, Quaternion.identity);
	}

}
