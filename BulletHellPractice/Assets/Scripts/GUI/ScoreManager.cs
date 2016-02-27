using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	
	static ScoreManager _instance;
	public static ScoreManager managerInstance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.Find ("ScoreCounter").GetComponent<ScoreManager>();
			}
			return _instance;
		}
	}
	public int score = 0;
	public Text scoreText;
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score.ToString();
	}
}
