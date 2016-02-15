using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleLivesMngr : MonoBehaviour {
	public GameObject playerObj;
	public Transform spawnPosition;
	public int initialLives = 3;
	public Text myScoreText;


	private int curLives;
	// Use this for initialization
	void Start () {
	
		curLives = initialLives;
		Instantiate(playerObj, spawnPosition.position, Quaternion.Euler(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		myScoreText.text = curLives.ToString();
		if(!GameObject.FindGameObjectWithTag("Player"))
		{
			curLives--;
			GameObject playerInstance = Instantiate(playerObj, spawnPosition.position, Quaternion.Euler(0,0,0)) as GameObject;
			gameObject.GetComponent<SimpleFollow>().SetFollowTarget(playerInstance.transform);
		}
		
	}
}
