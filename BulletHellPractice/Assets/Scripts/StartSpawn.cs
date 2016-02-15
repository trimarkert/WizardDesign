using UnityEngine;
using System.Collections;

public class StartSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GetComponent<WaveSpawner>())
		{
			GetComponent<WaveSpawner>().SpawnDefault();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
