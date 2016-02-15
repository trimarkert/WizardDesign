using UnityEngine;
using System.Collections;

public class SimpleMoveForward : MonoBehaviour {
	public float speed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
