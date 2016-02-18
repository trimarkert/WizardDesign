using UnityEngine;
using System.Collections;

public class SpawnPointGizmo : MonoBehaviour {
	/**
	 * Draws a red square at location of object
	 * */
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, new Vector3(2,2,2));
	}
}
