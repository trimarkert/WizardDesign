using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleLivesMngr : MonoBehaviour {
	public GameObject playerObj;
	public Transform spawnPosition;
	public int initialLives = 3;
	public Text myScoreText;
	public int gameOverIndex = 2;


	private int curLives;
	// Use this for initialization
	void Start () {
	
		curLives = initialLives;
		Instantiate(playerObj, spawnPosition.position, Quaternion.Euler(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		myScoreText.text =  "Lives: " + curLives.ToString();
		if(!GameObject.FindGameObjectWithTag("Player"))
		{

			GameObject playerInstance = Instantiate(playerObj, spawnPosition.position, Quaternion.Euler(0,0,0)) as GameObject;
#if UNITY_EDITOR
			Selection.activeObject = playerInstance;
#endif

			curLives--;
			if(curLives <= 0)
			{
				Application.LoadLevel(gameOverIndex);
				return;
			}
			gameObject.GetComponent<SimpleFollow>().SetFollowTarget(playerInstance.transform);
		}
		
	}
}
