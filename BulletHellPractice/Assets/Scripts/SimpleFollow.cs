using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {
	public Transform followTarget;
	//how far away in the X Y Z directions your camera should hang
	public float offsetX = 0.0f;
	public float offsetY = 30.0f;
	public float offsetZ = -10.0f;
	
	// Update is called once per frame
	void Update () {
		if(followTarget == null && GameObject.FindGameObjectWithTag("Player"))
		{
				GameObject firstPlayer = GameObject.FindGameObjectWithTag("Player");
				followTarget = firstPlayer.transform;
		}
		if(followTarget != null)
		{
			transform.position = new Vector3 (followTarget.position.x + offsetX, 
		                                  followTarget.position.y + offsetY,
		                                  followTarget.position.z + offsetZ);
		}
	}

	public void SetFollowTarget(Transform nextTarget)
	{
		followTarget = nextTarget;
	}
}
