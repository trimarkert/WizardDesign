using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
				GameObject nextPlayer = GameObject.FindGameObjectWithTag("Player");
				//Found this little nugget to help prevent "ghost clones" from messing with your searches later on
				//One bad thing is that it only works in the unity editor, will have to test to see if builds 
				//still get the ghost clone problem
#if UNITY_EDITOR
				Selection.activeObject = nextPlayer;
#endif
				//Set the next follow target of course
				followTarget = nextPlayer.transform;
		}
		if(followTarget != null)
		{
			transform.position = new Vector3 (followTarget.position.x + offsetX, 
		                                  followTarget.position.y + offsetY,
		                                  followTarget.position.z + offsetZ);
		}
	}
	/**
	 * Switches the follow target manually. could be used to do some rudementary cut scenes.
	 * */
	public void SetFollowTarget(Transform nextTarget)
	{
		followTarget = nextTarget;
	}
}
