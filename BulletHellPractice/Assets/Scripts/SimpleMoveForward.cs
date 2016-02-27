using UnityEngine;
using System.Collections;

public class SimpleMoveForward : MonoBehaviour {
	public float speed = 15.0f;

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
